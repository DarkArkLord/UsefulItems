using System;

namespace UsefulItems.CSharp.GameElements.DarkGraphicsLib.Common
{
    public class Vector2
    {
        #region Base
        public double X { get; set; }
        public double Y { get; set; }

        public Vector2(double x, double y) => (X, Y) = (x, y);

        public override string ToString() => $"({X}, {Y})";

        public Vector2 Copy() => new Vector2(X, Y);

        public static Vector2 FromAngle(double angle, double length) => new Vector2(length * Math.Cos(angle), length * Math.Sin(angle));

        public static Vector2 Zero => new Vector2(0, 0);
        #endregion

        #region Properties
        public double LengthSquare => X * X + Y * Y;

        public double Length => Math.Sqrt(LengthSquare);

        public double Angle => Math.Atan2(Y, X);

        public Vector2 Normalize => new Vector2(X / Length, Y / Length);
        #endregion

        #region Math
        public Vector2 Negative() => ((X, Y) = (-X, -Y), this).Item2;

        public Vector2 Add(Vector2 other) => ((X, Y) = (X + other.X, Y + other.Y), this).Item2;

        public Vector2 Add(double other) => ((X, Y) = (X + other, Y + other), this).Item2;

        public Vector2 Sub(Vector2 other) => ((X, Y) = (X - other.X, Y - other.Y), this).Item2;

        public Vector2 Sub(double other) => ((X, Y) = (X - other, Y - other), this).Item2;

        public Vector2 Mult(double other) => ((X, Y) = (X * other, Y * other), this).Item2;

        public Vector2 Div(double other) => ((X, Y) = (X / other, Y / other), this).Item2;

        public double Dot(Vector2 other) => X * other.X + Y * other.Y;
        #endregion

        public Vector2 Rotate(double angle)
        {
            var deltaSin = Math.Sin(angle);
            var deltaCos = Math.Cos(angle);
            var old = (X, Y);
            X = old.X * deltaCos + old.Y * deltaSin;
            Y = old.Y * deltaCos - old.X * deltaSin;
            return this;
        }
    }
}
