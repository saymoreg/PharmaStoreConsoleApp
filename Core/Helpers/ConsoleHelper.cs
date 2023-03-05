using System;
namespace Core.Helpers
{
    public static class ConsoleHelper
    {
        public static void WriteWithColor(string text, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}