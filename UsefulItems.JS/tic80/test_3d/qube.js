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

var current_angle = 0;
const angle_delta = Math.PI / 180;

function TIC() {
    cls();

    var current_сube = RotateСube(base_cube, { x: current_angle, y: 0, z: 0 });
    DrawСube(current_сube, Colors.Lime);

    current_angle += angle_delta;
}

function RotateСube(сube, angle3) {
    function RotatePoint(point) {
        // nope...

        var point_len = Math.sqrt(point.x * point.x + point.y * point.y + point.z * point.z);

        var x_angle = Math.acos(point.x / point_len) + angle3.x;
        var y_angle = Math.acos(point.y / point_len) + angle3.y;
        var z_angle = Math.acos(point.z / point_len) + angle3.z;

        return {
            x: point_len * Math.cos(x_angle),
            y: point_len * Math.cos(y_angle),
            z: point_len * Math.cos(z_angle)
        }
    }

    var rotated_cube = {
        Forward: {
            TopLeft: RotatePoint(сube.Forward.TopLeft),
            TopRight: RotatePoint(сube.Forward.TopRight),
            DownLeft: RotatePoint(сube.Forward.DownLeft),
            DownRight: RotatePoint(сube.Forward.DownRight),
        },
        Back: {
            TopLeft: RotatePoint(сube.Back.TopLeft),
            TopRight: RotatePoint(сube.Back.TopRight),
            DownLeft: RotatePoint(сube.Back.DownLeft),
            DownRight: RotatePoint(сube.Back.DownRight),
        }
    }

    return rotated_cube;
}

function DrawСube(сube, color) {
    function DrawSquare(square, color) {
        DrawLine(square.TopLeft, square.TopRight, color);
        DrawLine(square.DownLeft, square.DownRight, color);

        DrawLine(square.TopLeft, square.DownLeft, color);
        DrawLine(square.TopRight, square.DownRight, color);
    }

    function DrawSquareLinks(square1, square2, color) {
        DrawLine(square1.TopLeft, square2.TopLeft, color);
        DrawLine(square1.TopRight, square2.TopRight, color);
        DrawLine(square1.DownRight, square2.DownRight, color);
        DrawLine(square1.DownLeft, square2.DownLeft, color);
    }

    DrawSquare(сube.Forward, color);
    DrawSquare(сube.Back, color);
    DrawSquareLinks(сube.Forward, сube.Back, color)
}

function DrawLine(point1, point2, color) {
    var start = PointToScreen(point1);
    var end = PointToScreen(point2);
    line(start.x, start.y, end.x, end.y, color);
}