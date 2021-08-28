using UsefulItems.CSharp.GameElements.DarkGraphicsLib.Common;

namespace UsefulItems.CSharp.GameElements.DarkGraphicsLib.ParticlesSystem2D
{
    public class MassField
    {
        public Vector2 Position { get; set; }
        public double Mass { get; set; }

        public MassField(Vector2 position, double mass) =>
            (Position, Mass) = (position, mass);
    }
}
