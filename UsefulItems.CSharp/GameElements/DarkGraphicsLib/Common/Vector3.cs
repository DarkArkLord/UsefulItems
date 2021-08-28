using System;

namespace UsefulItems.CSharp.GameElements.DarkGraphicsLib.Common
{
    public class Vector3
    {
        #region Base
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Vector3(double x, double y, double z) => (X, Y, Z) = (x, y, z);

        public override string ToString() => $"({X}, {Y}, {Z})";

        public Vector3 Copy() => new Vector3(X, Y, Z);

        public static Vector3 Zero => new Vector3(0, 0, 0);

        public Vector2 ToVector2() => new Vector2(X, Y);
        #endregion

        #region Properties
        public double LengthSquare => X * X + Y * Y + Z * Z;

        public double Length => Math.Sqrt(LengthSquare);

        public Vector3 Normalize => new Vector3(X / Length, Y / Length, Z / Length);
        #endregion

        #region Math
        public Vector3 Negative() => ((X, Y, Z) = (-X, -Y, -Z), this).Item2;

        public Vector3 Add(Vector3 other) => ((X, Y, Z) = (X + other.X, Y + other.Y, Z + other.Z), this).Item2;

        public Vector3 Add(double other) => ((X, Y, Z) = (X + other, Y + other, Z + other), this).Item2;

        public Vector3 Sub(Vector3 other) => ((X, Y, Z) = (X - other.X, Y - other.Y, Z - other.Z), this).Item2;

        public Vector3 Sub(double other) => ((X, Y, Z) = (X - other, Y - other, Z - other), this).Item2;

        public Vector3 Mult(double other) => ((X, Y, Z) = (X * other, Y * other, Z * other), this).Item2;

        public Vector3 Div(double other) => ((X, Y, Z) = (X / other, Y / other, Z / other), this).Item2;

        public double Dot(Vector3 other) => X * other.X + Y * other.Y + Z * other.Z;
        #endregion

        public Vector3 Rotate(Vector3 angles)
        {
            var temp = new Vector2(X, Y).Rotate(angles.Z);
            (X, Y) = (temp.X, temp.Y);

            temp = new Vector2(Y, Z).Rotate(angles.X);
            (Y, Z) = (temp.X, temp.Y);

            temp = new Vector2(Z, X).Rotate(angles.Y);
            (Z, X) = (temp.X, temp.Y);

            return this;
        }
    }
}
