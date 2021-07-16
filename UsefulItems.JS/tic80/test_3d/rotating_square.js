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

const WorkingSpaceConfig = {
    Width: WindowConfig.Width / 2,
    Height: WindowConfig.Height / 2,
    Depth: WindowConfig.Width / 2,
    DepthDivider: 2
}

function PointToScreen(point) {
    var depth_mult = (function() {
        const depth_mid = (1 + 1 / WorkingSpaceConfig.DepthDivider) / 2;
        const depth_delta = 1 - depth_mid;
        return depth_mid + depth_delta * point.z / WorkingSpaceConfig.Depth;
    })();

    const half_width = WindowConfig.Width / 2;
    const half_height = WindowConfig.Height / 2;

    var x = half_width + (point.x / WorkingSpaceConfig.Width * half_width) * depth_mult;
    var y = half_height - (point.y / WorkingSpaceConfig.Height * half_height) * depth_mult;

    return { x, y };
}

const t = 30;

var square = {
    TopLeft: { x: -t, y: t, z: 0 },
    TopRight: { x: t, y: t, z: 0 },
    DownLeft: { x: -t, y: -t, z: 0 },
    DownRight: { x: t, y: -t, z: 0 }
}

var angle = 0;
const angle_delta = Math.PI / 180;

function TIC() {
    cls();

    var temp = {
        TopLeft: PointToScreen(RotatePoint(square.TopLeft)),
        TopRight: PointToScreen(RotatePoint(square.TopRight)),
        DownLeft: PointToScreen(RotatePoint(square.DownLeft)),
        DownRight: PointToScreen(RotatePoint(square.DownRight)),
    }

    line(temp.TopLeft.x, temp.TopLeft.y, temp.TopRight.x, temp.TopRight.y, Colors.Lime);
    line(temp.TopRight.x, temp.TopRight.y, temp.DownRight.x, temp.DownRight.y, Colors.Lime);
    line(temp.DownRight.x, temp.DownRight.y, temp.DownLeft.x, temp.DownLeft.y, Colors.Lime);
    line(temp.DownLeft.x, temp.DownLeft.y, temp.TopLeft.x, temp.TopLeft.y, Colors.Lime);

    angle += angle_delta; // 0.03;
}

function RotatePoint(point) {
    return {
        x: Math.cos(angle) * point.x,
        y: point.y,
        z: Math.sin(angle) * point.x
    }
}