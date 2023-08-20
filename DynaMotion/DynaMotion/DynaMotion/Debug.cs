using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynaMotion.DynaMotion
{
    public class Debug
    {
        public static void Log(string logText)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"[Log] - {logText}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void LogWarning(string logText)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[Warning] - {logText}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void LogError(string logText)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[Exception] - {logText}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
