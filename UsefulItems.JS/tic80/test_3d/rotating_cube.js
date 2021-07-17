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

const DisplayConfig = {
    Width: WindowConfig.Width / 2,
    Height: WindowConfig.Height / 2
}

const WorkingSpaceConfig = {
    Width: DisplayConfig.Width,
    Height: DisplayConfig.Height,
    Depth: DisplayConfig.Width,
    DepthDivider: 3
}

function PointToScreen(point) {
    var depth_mult = (function() {
        const depth_mid = (1 + 1 / WorkingSpaceConfig.DepthDivider) / 2;
        const depth_delta = 1 - depth_mid;
        return depth_mid + depth_delta * point.z / WorkingSpaceConfig.Depth;
    })();

    var x = DisplayConfig.Width + (point.x / WorkingSpaceConfig.Width * DisplayConfig.Width) * depth_mult;
    var y = DisplayConfig.Height - (point.y / WorkingSpaceConfig.Height * DisplayConfig.Height) * depth_mult;

    return { x, y };
}

const qube_point = 50;

var base_cube = {
    Forward: {
        TopLeft: { x: -qube_point, y: qube_point, z: qube_point },
        TopRight: { x: qube_point, y: qube_point, z: qube_point },
        DownLeft: { x: -qube_point, y: -qube_point, z: qube_point },
        DownRight: { x: qube_point, y: -qube_point, z: qube_point }
    },
    Back: {
        TopLeft: { x: -qube_point, y: qube_point, z: -qube_point },
        TopRight: { x: qube_point, y: qube_point, z: -qube_point },
        DownLeft: { x: -qube_point, y: -qube_point, z: -qube_point },
        DownRight: { x: qube_point, y: -qube_point, z: -qube_point }
    }
}

function Counter(min, max, step) {
    var counter = min;

    this.Next = function() {
        if (counter >= max) {
            return false;
        }

        counter += step;
        return true;
    }

    this.Reset = function() { counter = min; }

    this.Get = function() { return counter; }
}

const current_angle = new Counter(0, 2 * Math.PI, Math.PI / (60 * 2));

const rotate_axe = new(function() {
    var axes = {
        x: new Counter(-1, 1, 1),
        y: new Counter(-1, 1, 1),
        z: new Counter(-1, 1, 1)
    };

    axes.x.Next();
    axes.x.Next();
    axes.y.Next();
    axes.z.Next();

    this.Get = function(angle) {
        return {
            x: axes.x.Get() * angle,
            y: axes.y.Get() * angle,
            z: axes.z.Get() * angle
        };
    }

    this.Next = function() {
        if (!axes.x.Next()) {
            axes.x.Reset();
            if (!axes.y.Next()) {
                axes.y.Reset();
                if (!axes.z.Next()) {
                    axes.z.Reset();
                }
            }
        }
    }
})();

const pause_timer = new Counter(0, 20, 1);

function TIC() {
    if (pause_timer.Next()) {
        return;
    }

    cls();

    const angle = current_angle.Get();
    const rotate = rotate_axe.Get(angle);

    var current_сube = RotateСube(base_cube, rotate);
    DrawСube(current_сube);

    if (!current_angle.Next()) {
        current_angle.Reset();
        rotate_axe.Next();
        pause_timer.Reset();
    }
}

function RotateСube(сube, angle3) {
    function RotatePoint3(point3) {
        function RotatePoint2(point2, angle) {
            var delta_sin = Math.sin(angle);
            var delta_cos = Math.cos(angle);
            return {
                x: point2.x * delta_cos + point2.y * delta_sin,
                y: point2.y * delta_cos - point2.x * delta_sin
            };
        }

        var res = {
            x: point3.x,
            y: point3.y,
            z: point3.z
        }

        var t = RotatePoint2({ x: res.y, y: res.z }, angle3.x);
        res.y = t.x;
        res.z = t.y;

        t = RotatePoint2({ x: res.x, y: res.z }, angle3.y);
        res.x = t.x;
        res.z = t.y;

        t = RotatePoint2({ x: res.x, y: res.y }, angle3.z);
        res.x = t.x;
        res.y = t.y;

        return res;
    }

    var rotated_cube = {
        Forward: {
            TopLeft: RotatePoint3(сube.Forward.TopLeft),
            TopRight: RotatePoint3(сube.Forward.TopRight),
            DownLeft: RotatePoint3(сube.Forward.DownLeft),
            DownRight: RotatePoint3(сube.Forward.DownRight),
        },
        Back: {
            TopLeft: RotatePoint3(сube.Back.TopLeft),
            TopRight: RotatePoint3(сube.Back.TopRight),
            DownLeft: RotatePoint3(сube.Back.DownLeft),
            DownRight: RotatePoint3(сube.Back.DownRight),
        }
    }

    return rotated_cube;
}

function DrawСube(сube) {
    function DrawSquare(square, color) {
        DrawLine(square.TopLeft, square.TopRight, color);
        DrawLine(square.DownLeft, square.DownRight, color);

        DrawLine(square.TopLeft, square.DownLeft, color);
        DrawLine(square.TopRight, square.DownRight, color);
    }

    function DrawSquareLinks(square1, square2) {
        DrawLine(square1.TopLeft, square2.TopLeft, Colors.Red);
        DrawLine(square1.TopRight, square2.TopRight, Colors.Yellow);
        DrawLine(square1.DownRight, square2.DownRight, Colors.Green);
        DrawLine(square1.DownLeft, square2.DownLeft, Colors.LightBlue);
    }

    DrawSquare(сube.Back, Colors.Violet);
    DrawSquareLinks(сube.Forward, сube.Back);
    DrawSquare(сube.Forward, Colors.White);
}

function DrawLine(point1, point2, color) {
    var start = PointToScreen(point1);
    var end = PointToScreen(point2);
    line(start.x, start.y, end.x, end.y, color);
}