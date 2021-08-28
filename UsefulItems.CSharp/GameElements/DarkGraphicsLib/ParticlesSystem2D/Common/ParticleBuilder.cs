using UsefulItems.CSharp.GameElements.DarkGraphicsLib.Common;

namespace UsefulItems.CSharp.GameElements.DarkGraphicsLib.ParticlesSystem2D.Common
{
    public class ParticleBuilder
    {
        public static ParticleBuilder NewBuilder => new ParticleBuilder();

        private Vector2 _position = Vector2.Zero;
        private Vector2 _speed = Vector2.Zero;
        private Vector2 _acceleration = Vector2.Zero;

        public ParticleBuilder Clear() => ClearPosition().ClearSpeed().ClearAcceleration();

        public Particle Create() => new Particle(_position.Copy(), _speed.Copy(), _acceleration.Copy());

        public ParticleBuilder SetPosition(Vector2 position) => 
            (_position = position, this).Item2;

        public ParticleBuilder SetPosition(double x, double y) =>
            (_position = new Vector2(x, y), this).Item2;

        public ParticleBuilder ClearPosition() =>
            (_position = Vector2.Zero, this).Item2;

        public ParticleBuilder SetSpeed(Vector2 speed) =>
            (_speed = speed, this).Item2;

        public ParticleBuilder SetSpeed(double angle, double speed) =>
            (_speed = Vector2.FromAngle(angle, speed), this).Item2;

        public ParticleBuilder ClearSpeed() =>
            (_speed = Vector2.Zero, this).Item2;

        public ParticleBuilder SetAcceleration(Vector2 acceleration) =>
            (_acceleration = acceleration, this).Item2;

        public ParticleBuilder SetAcceleration(double angle, double speed) =>
            (_acceleration = Vector2.FromAngle(angle, speed), this).Item2;

        public ParticleBuilder ClearAcceleration() =>
            (_acceleration = Vector2.Zero, this).Item2;
    }
}
