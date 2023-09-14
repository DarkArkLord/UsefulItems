namespace ConsoleUtilsCore
{
    public static class ConsoleOutput
    {
        public static void WriteLineWithColor(string text, ConsoleColor color = ConsoleColor.Gray)
        {
            var oldColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = oldColor;
        }
    }
}
