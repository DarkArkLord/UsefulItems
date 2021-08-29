using UsefulItems.CSharp.DarkGraphicsLib.CommonElements;

namespace UsefulItems.CSharp.DarkGraphicsLib.ParticleSystem2D
{
    public class MassField
    {
        public Vector2 Position { get; set; }
        public double Mass { get; set; }

        public MassField(Vector2 position, double mass) =>
            (Position, Mass) = (position, mass);
    }
}
