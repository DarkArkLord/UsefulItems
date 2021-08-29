using System;
using UsefulItems.CSharp.DarkGraphicsLib.CommonElements;

namespace UsefulItems.CSharp.DarkGraphicsLib.ParticleSystem2D.Common
{
    public class EmitterBuilder
    {
        public static EmitterBuilder NewBuilder => new EmitterBuilder();

        private Random _random = new Random();
        private Vector2 _position = Vector2.Zero;
        private Vector2 _size = Vector2.Zero;
        private Vector2 _startSpeed = Vector2.Zero;
        private double _speedDelta = 0;
        private double _angleDelta = 0;

        public EmitterBuilder Clear() => ClearRandom()
            .ClearPosition().ClearSize().ClearStartSpeed()
            .ClearSpeedDelta().ClearAngleDelta();

        public Emitter Create() => new Emitter(_position.Copy(), _size.Copy(), _startSpeed.Copy(), _speedDelta, _angleDelta, _random);

        public EmitterBuilder SetRandom(Random random) =>
            (_random = random, this).Item2;

        public EmitterBuilder SetRandom(int seed) =>
            (_random = new Random(seed), this).Item2;

        public EmitterBuilder ClearRandom() =>
            (_random = new Random(), this).Item2;

        public EmitterBuilder SetPosition(Vector2 position) =>
            (_position = position, this).Item2;

        public EmitterBuilder SetPosition(double x, double y) =>
            (_position = new Vector2(x, y), this).Item2;

        public EmitterBuilder ClearPosition() =>
            (_position = Vector2.Zero, this).Item2;

        public EmitterBuilder SetSize(Vector2 size) =>
            (_size = size, this).Item2;

        public EmitterBuilder SetSize(double width, double height) =>
            (_size = new Vector2(width, height), this).Item2;

        public EmitterBuilder ClearSize() =>
            (_size = Vector2.Zero, this).Item2;

        public EmitterBuilder SetStartSpeed(Vector2 acceleration) =>
            (_startSpeed = acceleration, this).Item2;

        public EmitterBuilder SetStartSpeed(double angle, double speed) =>
            (_startSpeed = Vector2.FromAngle(angle, speed), this).Item2;

        public EmitterBuilder ClearStartSpeed() =>
            (_startSpeed = Vector2.Zero, this).Item2;

        public EmitterBuilder SetSpeedDelta(double speedDelta) =>
            (_speedDelta = speedDelta, this).Item2;

        public EmitterBuilder ClearSpeedDelta() =>
            (_speedDelta = 0, this).Item2;

        public EmitterBuilder SetAngleDelta(double angleDelta) =>
            (_angleDelta = angleDelta, this).Item2;

        public EmitterBuilder ClearAngleDelta() =>
            (_angleDelta = 0, this).Item2;
    }
}
