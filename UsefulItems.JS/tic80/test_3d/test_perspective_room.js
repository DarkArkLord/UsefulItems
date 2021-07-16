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
        // (1 + point.z / WorkingSpaceConfig.Depth) / 2
        return depth_mid + depth_delta * point.z / WorkingSpaceConfig.Depth;
    })();

    const half_width = WindowConfig.Width / 2;
    const half_height = WindowConfig.Height / 2;

    var x = half_width + (point.x / WorkingSpaceConfig.Width * half_width) * depth_mult;
    var y = half_height - (point.y / WorkingSpaceConfig.Height * half_height) * depth_mult;

    return { x, y };
}

const qube_point = 20;

var qube = {
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

var angle = 0;
const angle_delta = Math.PI / 180;

function TIC() {
    cls();

    DrawSquare(WorkingSpaceConfig.Depth);
    DrawSquare(0);
    DrawSquare(-WorkingSpaceConfig.Depth);

    LRLines(WorkingSpaceConfig.Width);
    LRLines(0);
    LRLines(-WorkingSpaceConfig.Width);

    angle += angle_delta;
}

function DrawSquare(z) {
    DrawLine({ x: -WorkingSpaceConfig.Width, y: WorkingSpaceConfig.Height, z: z }, { x: WorkingSpaceConfig.Width, y: WorkingSpaceConfig.Height, z: z });
    DrawLine({ x: -WorkingSpaceConfig.Width, y: -WorkingSpaceConfig.Height, z: z }, { x: WorkingSpaceConfig.Width, y: -WorkingSpaceConfig.Height, z: z });

    DrawLine({ x: WorkingSpaceConfig.Width, y: -WorkingSpaceConfig.Height, z: z }, { x: WorkingSpaceConfig.Width, y: WorkingSpaceConfig.Height, z: z });
    DrawLine({ x: -WorkingSpaceConfig.Width, y: -WorkingSpaceConfig.Height, z: z }, { x: -WorkingSpaceConfig.Width, y: WorkingSpaceConfig.Height, z: z });
}

function LRLines(x) {
    DrawLine({ x: x, y: WorkingSpaceConfig.Height, z: -WorkingSpaceConfig.Depth }, { x: x, y: WorkingSpaceConfig.Height, z: WorkingSpaceConfig.Depth });
    DrawLine({ x: x, y: 0, z: -WorkingSpaceConfig.Depth }, { x: x, y: 0, z: WorkingSpaceConfig.Depth });
    DrawLine({ x: x, y: -WorkingSpaceConfig.Height, z: -WorkingSpaceConfig.Depth }, { x: x, y: -WorkingSpaceConfig.Height, z: WorkingSpaceConfig.Depth });
}

function DrawLine(point1, point2) {
    var start = PointToScreen(point1);
    var end = PointToScreen(point2);
    line(start.x, start.y, end.x, end.y, Colors.Lime);
}