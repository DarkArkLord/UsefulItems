using System;

namespace ConsoleGameCore
{
    public enum EConsoleColor
    {
        Black = 0,
        Red = 1,
        Green = 2,
        Yellow = 3,
        Blue = 4,
        Cyan = 6,
        Magenta = 5,
        White = 7,

        LightBlack = 60,
        LightRed = 61,
        LightGreen = 62,
        LightYellow = 63,
        LightBlue = 64,
        LightMagenta = 65,
        LightCyan = 66,
        LightWhite = 67,
    }

    public static class ConsoleColorExt
    {
        public static void CheckColors()
        {
            (ConsoleColor, EConsoleColor)[] colors = new (ConsoleColor, EConsoleColor)[]
            {
                (ConsoleColor.Black,       EConsoleColor.Black),
                (ConsoleColor.Red,         EConsoleColor.LightRed),
                (ConsoleColor.Green,       EConsoleColor.LightGreen),
                (ConsoleColor.Yellow,      EConsoleColor.LightYellow),
                (ConsoleColor.Blue,        EConsoleColor.LightBlue),
                (ConsoleColor.Magenta,     EConsoleColor.LightMagenta),
                (ConsoleColor.Cyan,        EConsoleColor.LightCyan),
                (ConsoleColor.White,       EConsoleColor.LightWhite),
                (ConsoleColor.DarkGray,    EConsoleColor.LightBlack),
                (ConsoleColor.DarkRed,     EConsoleColor.Red),
                (ConsoleColor.DarkGreen,   EConsoleColor.Green),
                (ConsoleColor.DarkYellow,  EConsoleColor.Yellow),
                (ConsoleColor.DarkBlue,    EConsoleColor.Blue),
                (ConsoleColor.DarkMagenta, EConsoleColor.Magenta),
                (ConsoleColor.DarkCyan,    EConsoleColor.Cyan),
                (ConsoleColor.Gray,        EConsoleColor.White),
            };

            var baseColor = Console.ForegroundColor;
            var text = "MY TEXT";
            foreach (var color in colors)
            {
                Console.ForegroundColor = baseColor;
                Console.Write($"{color.Item1} -> ");
                Console.ForegroundColor = color.Item1;
                Console.Write(text);
                Console.ForegroundColor = baseColor;
                Console.Write($" | {color.Item2} -> ");
                Console.WriteLine($"\u001b[{color.Item2.GetForegroundColor()}m{text}\u001b[0m");
            }
        }

        public static string GetForegroundColor(this EConsoleColor color)
        {
            return (30 + (int)color).ToString();
        }

        public static string GetForegroundColor(this EConsoleColor? color, EConsoleColor defaultColor)
        {
            if (color != null)
            {
                return color.Value.GetForegroundColor();
            }

            return defaultColor.GetForegroundColor();
        }

        public static string GetBackgroundColor(this EConsoleColor color)
        {
            return (40 + (int)color).ToString();
        }

        public static string GetBackgroundColor(this EConsoleColor? color, EConsoleColor defaultColor)
        {
            if (color != null)
            {
                return color.Value.GetBackgroundColor();
            }

            return defaultColor.GetBackgroundColor();
        }
    }
}
