using System;
using UsefulItems.CSharp.GameElements.DarkGraphicsLib.Common;

namespace UsefulItems.CSharp.GameElements.DarkGraphicsLib.ParticlesSystem2D
{
    public class Emitter
    {
        public Random Random { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public Vector2 StartSpeed { get; set; }
        public double SpeedDelta { get; set; }
        public double AngleDelta { get; set; }

        public Emitter(Vector2 position, Vector2 size, Vector2 startSpeed, double speedDelta, double angleDelta, Random random) =>
            (Position, Size, StartSpeed, SpeedDelta, AngleDelta, Random) = (position, size, startSpeed, speedDelta, angleDelta, random);

        public double GetSpeedRandomOffset() => SpeedDelta - Random.NextDouble() * SpeedDelta * 2;

        public double GetAngleRandomOffset() => AngleDelta - Random.NextDouble() * AngleDelta * 2;

        public Vector2 GetRandomInnerPosition()
        {
            var x = Position.X + Math.Floor(Random.NextDouble() * Size.X);
            var y = Position.Y - Math.Floor(Random.NextDouble() * Size.Y);
            return new Vector2(x, y);
        }

        public Particle EmitParticle()
        {
            var position = GetRandomInnerPosition();
            var angle = StartSpeed.Angle + GetAngleRandomOffset();
            var length = StartSpeed.Length + GetSpeedRandomOffset();
            var speed = Vector2.FromAngle(angle, length);
            return new Particle(position, speed, Vector2.Zero);
        }
    }
}
