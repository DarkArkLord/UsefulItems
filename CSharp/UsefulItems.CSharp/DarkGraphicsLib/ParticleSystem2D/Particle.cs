using System.Collections.Generic;
using UsefulItems.CSharp.DarkGraphicsLib.CommonElements;

namespace UsefulItems.CSharp.DarkGraphicsLib.ParticleSystem2D
{
    public class Particle
    {
        public Vector2 Position { get; set; }
        public Vector2 Speed { get; set; }
        public Vector2 Acceleration { get; set; }

        public Particle(Vector2 position, Vector2 speed, Vector2 acceleration) =>
            (Position, Speed, Acceleration) = (position, speed, acceleration);

        public void Move()
        {
            Speed.Add(Acceleration);
            Position.Add(Speed);
        }

        public void AffectByField(MassField field)
        {
            var delta = new Vector2(field.Position.X - Position.X, field.Position.Y - Position.Y);
            var distance = delta.Length;
            var distance3 = distance * distance * distance;
            var force = field.Mass / distance3;
            delta.Mult(force);
            Acceleration.Add(delta);
        }

        public void AffectByFields(IEnumerable<MassField> fields, bool cleanAcceleration = true)
        {
            if (cleanAcceleration) Acceleration.Clear();

            foreach (var field in fields)
            {
                AffectByField(field);
            }
        }
    }
}
