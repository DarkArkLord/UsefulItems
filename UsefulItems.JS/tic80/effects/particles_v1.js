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

(function() {
    // Сложить два вектора
    Vector.prototype.add = function(vector) {
        this.x += vector.x;
        this.y += vector.y;
    }

    // Получить длину вектора
    Vector.prototype.getMagnitude = function() {
        return Math.sqrt(this.x * this.x + this.y * this.y);
    };

    // Получить угол вектора, учитывая квадрант
    Vector.prototype.getAngle = function() {
        return Math.atan2(this.y, this.x);
    };

    // Получить новый вектор, исходя из угла и размеров
    Vector.fromAngle = function(angle, magnitude) {
        return new Vector(magnitude * Math.cos(angle), magnitude * Math.sin(angle));
    };
})();

function Particle(point, velocity, acceleration) {
    this.position = point || new Vector(0, 0);
    this.velocity = velocity || new Vector(0, 0);
    this.acceleration = acceleration || new Vector(0, 0);
}

(function() {
    Particle.prototype.move = function() {
        // Добавить ускорение к скорости
        this.velocity.add(this.acceleration);

        // Добавить скорость к координатам
        this.position.add(this.velocity);
    };

    Particle.prototype.submitToFields = function(fields) {
        // стартовое ускорение в кадре
        var totalAccelerationX = 0;
        var totalAccelerationY = 0;

        // запускаем цикл по гравитационным полям
        for (var i = 0; i < fields.length; i++) {
            var field = fields[i];

            // вычисляем расстояние между частицей и полем
            var vectorX = field.position.x - this.position.x;
            var vectorY = field.position.y - this.position.y;

            // вычисляем силу с помощью МАГИИ и НАУКИ!
            var force = field.mass / Math.pow(vectorX * vectorX + vectorY * vectorY, 1.5);

            // аккумулируем ускорение в кадре произведением силы на расстояние
            totalAccelerationX += vectorX * force;
            totalAccelerationY += vectorY * force;
        }

        // обновляем ускорение частицы
        this.acceleration = new Vector(totalAccelerationX, totalAccelerationY);
    };
})();

function Emitter(point, velocity, spread) {
    this.position = point || new Vector(0, 0);
    this.velocity = velocity || new Vector(0, 0);
    this.spread = spread || Math.PI / 32; // Возможный угол = скорость +/- разброс.
    this.drawColor = Colors.Green;
}

(function() {
    Emitter.prototype.emitParticle = function() {
        // Использование случайного угла для формирования потока частиц позволит нам получить своего рода «спрей»
        var angle = this.velocity.getAngle() + this.spread - (Math.random() * this.spread * 2);

        // Магнитуда скорости излучателя
        var magnitude = this.velocity.getMagnitude();

        // Координаты излучателя
        var position = new Vector(this.position.x, this.position.y);

        // Обновлённая скорость, полученная из вычисленного угла и магнитуды
        var velocity = Vector.fromAngle(angle, magnitude);

        // Возвращает нашу Частицу!
        return new Particle(position, velocity);
    };
})();

function Field(point, mass) {
    this.position = point;
    this.setMass(mass);
}

Field.prototype.setMass = function(mass) {
    this.mass = mass || 100;
    this.drawColor = mass < 0 ? Colors.Red : Colors.Blue;
}

var particles = [];

// Добавим один излучатель с координатами `100, 230` от начала координат (верхний левый угол)
// Начнём излучать на скорости `2` в правую сторону (угол `0`)
var emitters = [new Emitter(new Vector(WindowConfig.Width / 9 * 4, WindowConfig.Height / 2), Vector.fromAngle(0, 2))];

// Добавляем поле с координатами `400, 230` (правее излучателя),
// установив отрицательную массу `-140`
var fields = [new Field(new Vector(WindowConfig.Width / 9 * 8, WindowConfig.Height / 2), -50), new Field(new Vector(WindowConfig.Width / 9 * 3, WindowConfig.Height / 2), 50)];

var maxParticles = 200; // Эксперимент! 20 000 обеспечит прекрасную вселенную
var emissionRate = 4; // количество частиц, излучаемых за кадр

function addNewParticles() {
    // прекращаем, если достигнут предел
    if (particles.length > maxParticles) return;

    // запускаем цикл по каждому излучателю
    for (var i = 0; i < emitters.length; i++) {

        // согласно emissionRate, генерируем частицы
        for (var j = 0; j < emissionRate; j++) {
            particles.push(emitters[i].emitParticle());
        }

    }
}

function plotParticles(boundsX, boundsY) {
    var currentParticles = [];
    for (var i = 0; i < particles.length; i++) {
        var particle = particles[i];
        var pos = particle.position;
        if (pos.x < 0 || pos.x > boundsX || pos.y < 0 || pos.y > boundsY) continue;

        // Обновление скорости и ускорения, в соответствии с гравитацией полей
        particle.submitToFields(fields);

        particle.move();
        currentParticles.push(particle);
    }
    particles = currentParticles;
}

function drawParticles() {

    // Запускаем цикл, который отображает частицы
    for (var i = 0; i < particles.length; i++) {
        var position = particles[i].position;

        // Рисуем квадрат определенных размеров с заданными координатами
        pix(position.x, position.y, Colors.Blue);
    }
}

function drawCircle(object) {
    circ(object.position.x, object.position.y, 2, object.drawColor);
}

function TIC() {
    cls();
    addNewParticles();
    plotParticles(WindowConfig.Width, WindowConfig.Height);
    drawParticles();
    fields.forEach(drawCircle);
    emitters.forEach(drawCircle);
}