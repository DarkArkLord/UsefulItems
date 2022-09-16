// title: game title
// author: game developer
// desc: short description
// script: js

const Colors = {
    Black: 0,
    Violet: 1,
    Red: 2,
    Orange: 3,
    Yellow: 4,
    Lime: 5,
    Green: 6,
    Sea: 7,
    DarkBlue: 8,
    Blue: 9,
    LightBlue: 10,
    Sky: 11,
    White: 12,
    LightGray: 13,
    Gray: 14,
    DarkGray: 15
};

const WindowConfig = {
    Width: 240 - 1,
    Height: 136 - 1
}

// Вектор
function Vector(x, y) {
    this.x = x || 0;
    this.y = y || 0;
}

Vector.prototype = {
    copy() { return new Vector(this.x, this.y); },
    add(other) {
        this.x += other.x;
        this.y += other.y;
    },
    getLength() { return Math.sqrt(this.x * this.x + this.y * this.y); },
    getAngle() { return Math.atan2(this.y, this.x); }
};

Vector.fromAngle = function(angle, length) {
    var x = length * Math.cos(angle);
    var y = length * Math.sin(angle);
    return new Vector(x, y);
};

Vector.zero = function() { return new Vector(0, 0); };

function Particle(position, speed, acceleration, color) {
    this.position = position || Vector.zero();
    this.speed = speed || Vector.zero();
    this.acceleration = acceleration || Vector.zero();
    this.color = color || Colors.Blue;
}

Particle.prototype = {
    move() {
        this.speed.add(this.acceleration);
        this.position.add(this.speed);
    },
    submitToFields(fields) {
        // стартовое ускорение в кадре
        var totalAccelerationX = 0;
        var totalAccelerationY = 0;

        // запускаем цикл по гравитационным полям
        for (var i = 0; i < fields.length; i++) {
            var field = fields[i];

            // вычисляем расстояние между частицей и полем
            var distanceX = field.position.x - this.position.x;
            var distanceY = field.position.y - this.position.y;
            var distance2 = distanceX * distanceX + distanceY * distanceY;
            var distance = distance2 ** 1.5;

            // вычисляем силу с помощью МАГИИ и НАУКИ!
            var force = field.mass / distance;

            // аккумулируем ускорение в кадре произведением силы на расстояние
            totalAccelerationX += distanceX * force;
            totalAccelerationY += distanceY * force;
        }

        // обновляем ускорение частицы
        this.acceleration = new Vector(totalAccelerationX, totalAccelerationY);
    }
};

function Rectangle(x, y, width, height) {
    this.position = new Vector(x, y);
    this.size = new Vector(width, height);
}

function Emitter(position, startSpeed, angleDelta, speedDelta, color) {
    this.position = position || new Rectangle(0, 0, 0, 0);
    this.startSpeed = startSpeed || Vector.zero();
    this.angleDelta = angleDelta || Math.PI / 32;
    this.speedDelta = speedDelta || 0;
    this.color = color || Colors.Green;
}

Emitter.prototype = {
    getAngleDelta() { return this.angleDelta - (Math.random() * this.angleDelta * 2); },
    getSpeedDelta() { return this.speedDelta - (Math.random() * this.speedDelta * 2); },
    getInnerPosition() {
        var x = this.position.position.x + (this.position.size.x * Math.random() | 0);
        var y = this.position.position.y + (this.position.size.y * Math.random() | 0);
        return new Vector(x, y);
    },
    emitParticle() {
        var angle = this.startSpeed.getAngle() + this.getAngleDelta();
        var length = this.startSpeed.getLength() + this.getSpeedDelta();
        var position = this.getInnerPosition();
        var speed = Vector.fromAngle(angle, length);
        return new Particle(position, speed);
    }
}

Emitter.forValues = function(x, y, width, height, targetAngle, moveSpeed, angleDelta, speedDelta, color) {
    var position = new Rectangle(x, y, width, height);
    var speed = Vector.fromAngle(targetAngle, moveSpeed);
    return new Emitter(position, speed, angleDelta, speedDelta, color);
}

function Field(position, mass) {
    this.position = position;
    this.setMass(mass);
}

Field.prototype = {
    massColors: {
        "-1": Colors.Red,
        "-0": Colors.Gray,
        "0": Colors.Gray,
        "1": Colors.Blue,
    },
    setMass(mass) {
        this.mass = mass || 0;
        this.color = this.massColors[Math.sign(mass)];
    }
}

Field.forValues = function(x, y, mass) {
    var position = new Vector(x, y);
    return new Field(position, mass);
}

var particlesSystem = {
    elements: {
        particles: [],
        emitters: [],
        fields: [],
    },
    config: {
        borders: {
            width: {
                start: 0,
                end: WindowConfig.Width
            },
            height: {
                start: 0,
                end: WindowConfig.Height
            }
        },
        maxParticles: 200,
        emissionRate: 4
    }
};

var halfHeight = WindowConfig.Height / 2;
var partWidth = WindowConfig.Width / 9;

particlesSystem.elements.emitters.push(Emitter.forValues(partWidth * 5 - 1, halfHeight - 1, 3, 3, 0, 2, Math.PI / 32, 1));

particlesSystem.elements.fields.push(Field.forValues(partWidth * 8, halfHeight, -50));
particlesSystem.elements.fields.push(Field.forValues(partWidth * 3, halfHeight, 50));

function addNewParticles(particles, emitters, config) {
    for (var emission = 0; emission < config.emissionRate; emission++) {
        if (particles.length > config.maxParticles) return;
        for (var i in emitters) {
            particles.push(emitters[i].emitParticle());
        }
    }
}

function plotParticles(particles, borders) {
    for (var i in particles) {
        var pos = particles[i].position;
        if (pos.x >= borders.width.start && pos.x <= borders.width.end && pos.y >= borders.height.start && pos.y <= borders.height.end) continue;
        particles.splice(i, 1);
        i--;
    }
}

function moveParticles(particles, fields) {
    for (var i in particles) {
        var particle = particles[i];
        particle.submitToFields(fields);
        particle.move();
    }
}

function drawParticles(elements, isDrawing) {
    isDrawing.fields && elements.fields.forEach(function drawField(field) {
        circ(field.position.x, field.position.y, 1, field.color);
    });
    isDrawing.emitters && elements.emitters.forEach(function drawEmitter(emitter) {
        rect(emitter.position.position.x, emitter.position.position.y, emitter.position.size.x, emitter.position.size.y, emitter.color);
    });
    isDrawing.particles && elements.particles.forEach(function drawParticle(particle) {
        pix(particle.position.x, particle.position.y, particle.color);
    });
}

function TIC() {
    cls();
    plotParticles(particlesSystem.elements.particles, particlesSystem.config.borders);
    addNewParticles(particlesSystem.elements.particles, particlesSystem.elements.emitters, particlesSystem.config);
    moveParticles(particlesSystem.elements.particles, particlesSystem.elements.fields);
    drawParticles(particlesSystem.elements, { particles: true, emitters: true, fields: true });
}