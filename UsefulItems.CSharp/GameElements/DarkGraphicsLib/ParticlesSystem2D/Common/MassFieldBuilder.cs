using UsefulItems.CSharp.GameElements.DarkGraphicsLib.Common;

namespace UsefulItems.CSharp.GameElements.DarkGraphicsLib.ParticlesSystem2D.Common
{
    public class MassFieldBuilder
    {
        public static MassFieldBuilder NewBuilder => new MassFieldBuilder();

        private Vector2 _position = Vector2.Zero;
        private double _mass = 0;

        public MassFieldBuilder Clear() => ClearPosition().ClearMass();

        public MassField Create() => new MassField(_position.Copy(), _mass);

        public MassFieldBuilder SetPosition(Vector2 position) =>
            (_position = position, this).Item2;

        public MassFieldBuilder SetPosition(double x, double y) =>
            (_position = new Vector2(x, y), this).Item2;

        public MassFieldBuilder ClearPosition() =>
            (_position = Vector2.Zero, this).Item2;

        public MassFieldBuilder SetMass(double mass) =>
            (_mass = mass, this).Item2;

        public MassFieldBuilder ClearMass() =>
            (_mass = 0, this).Item2;
    }
}
