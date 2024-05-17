using System;
using System.Text;

namespace ConsoleGameCore
{
    public class ConsoleScreen
    {
        public class ScreenElement
        {
            public char Value { get; set; }
            public EConsoleColor? ForegroundColor { get; set; }
            public EConsoleColor? BackgroundColor { get; set; }
        }

        protected char _defaultCharacter = '.';
        protected EConsoleColor _defaultForegroundColor = EConsoleColor.White;
        protected EConsoleColor _defaultBackgroundColor = EConsoleColor.Black;

        protected ScreenElement[,] _screen;
        protected StringBuilder _text;

        public int Width => _screen.GetLength(0);
        public int Height => _screen.GetLength(1);

        public ScreenElement this[int x, int y] => _screen[x, y];

        public ConsoleScreen(int width, int height)
        {
            _screen = new ScreenElement[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    _screen[x, y] = new ScreenElement();
                }
            }

            _text = new StringBuilder(width * height);
        }

        public void SetDefaultCharacter(char character)
        {
            _defaultCharacter = character;
        }

        public void SetDefaultForegroundColor(EConsoleColor color)
        {
            _defaultForegroundColor = color;
        }

        public void SetDefaultBackgroundColor(EConsoleColor color)
        {
            _defaultBackgroundColor = color;
        }

        public void Clear(char? fillCharacter = null,
            EConsoleColor? foregroundColor = null,
            EConsoleColor? backgroundColor = null)
        {
            int width = Width;
            int height = Height;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    _screen[x, y].Value = fillCharacter ?? _defaultCharacter;
                    _screen[x, y].ForegroundColor = foregroundColor;
                    _screen[x, y].BackgroundColor = backgroundColor;
                }
            }
        }

        public void Draw()
        {
            var text = ToString();
            SetCursorPosition(0, 0);
            Console.Clear();
            Console.Write(text);
            SetCursorPosition(0, 0);
        }

        public void SetCursorPosition(int x, int y)
        {
            Console.SetCursorPosition(x, y);
        }

        public override string ToString()
        {
            _text.Clear();

            int width = Width;
            int height = Height;

            string resetStyle = "\x1b[0m";

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var fgdColor = _screen[x, y].ForegroundColor.GetForegroundColor(_defaultForegroundColor);
                    var bgdColor = _screen[x, y].BackgroundColor.GetBackgroundColor(_defaultBackgroundColor);
                    var style = $"\x1b[{fgdColor};{bgdColor}m";

                    _text.Append(style);
                    _text.Append(_screen[x, y].Value);
                    _text.Append(resetStyle);
                }

                if (y < height - 1)
                {
                    _text.Append("\n");
                }
            }

            return _text.ToString();
        }
    }


    public static class ConsoleScreenTest
    {
        public static void Test()
        {
            Console.BufferHeight = Console.WindowHeight;
            Console.BufferWidth = Console.WindowWidth;

            ConsoleScreen screen = new ConsoleScreen(Console.WindowWidth, Console.WindowHeight);

            int x1 = 0;
            int y1 = 0;

            int x2 = 0;
            int y2 = 0;

            DateTime? date = null;
            var frameTime = 100;

            while (true)
            {
                screen.Clear();

                screen[x1, y1].Value = 'X';
                screen[x1, y1].ForegroundColor = EConsoleColor.Red;

                screen[x2, y2].BackgroundColor = EConsoleColor.Yellow;

                screen.Draw();

                var key = Console.ReadKey(true);

                // X control
                if (key.Key == ConsoleKey.LeftArrow && x1 > 0)
                {
                    x1 -= 1;
                }
                if (key.Key == ConsoleKey.RightArrow && x1 < screen.Width - 1)
                {
                    x1 += 1;
                }

                if (key.Key == ConsoleKey.UpArrow && y1 > 0)
                {
                    y1 -= 1;
                }
                if (key.Key == ConsoleKey.DownArrow && y1 < screen.Height - 1)
                {
                    y1 += 1;
                }

                // Yellow contol
                if (key.Key == ConsoleKey.A && x2 > 0)
                {
                    x2 -= 1;
                }
                if (key.Key == ConsoleKey.D && x2 < screen.Width - 1)
                {
                    x2 += 1;
                }

                if (key.Key == ConsoleKey.W && y2 > 0)
                {
                    y2 -= 1;
                }
                if (key.Key == ConsoleKey.S && y2 < screen.Height - 1)
                {
                    y2 += 1;
                }

                // Exit
                if (key.Key == ConsoleKey.Escape)
                {
                    break;
                }

                // FOR FRAME TIME
                if (date == null)
                {
                    date = DateTime.Now;
                }
                else
                {
                    var tdate = DateTime.Now;
                    var deltaTime = (tdate - date).Value.Milliseconds;

                    while (deltaTime < frameTime)
                    {
                        tdate = DateTime.Now;
                        deltaTime = (tdate - date).Value.Milliseconds;
                    }

                    date = tdate;
                    //times.Add(deltaTime);
                }

                // Clear console buffer
                while (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                }
            }

            Console.Clear();
            Console.WriteLine("\nend");
        }
    }
}
