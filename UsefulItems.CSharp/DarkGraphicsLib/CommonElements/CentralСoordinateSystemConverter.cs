namespace UsefulItems.CSharp.DarkGraphicsLib.CommonElements
{
    public class CentralСoordinateSystemConverter
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public CentralСoordinateSystemConverter(int width, int height) => (Width, Height) = (width, height);

        public Vector2 ConvertToScreen(Vector2 vector) => ConvertToScreen(vector, Width, Height);

        public Vector2 ConvertToCentral(Vector2 vector) => ConvertToCentral(vector, Width, Height);

        public static Vector2 ConvertToScreen(Vector2 vector, int width, int height)
        {
            var x = width / 2.0 + vector.X;
            var y = height / 2.0 - vector.Y;
            return new Vector2(x, y);
        }

        public static Vector2 ConvertToCentral(Vector2 vector, int width, int height)
        {
            var x = vector.X - width / 2.0;
            var y = height / 2.0 - vector.Y;
            return new Vector2(x, y);
        }
    }
}
