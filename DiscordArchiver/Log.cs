using System;

namespace DiscordArchiver
{
    public class Log
    {
        public static void Info(string asMessage)
        {
            Console.WriteLine($"[INFO] {asMessage}");
        }

        public static void Warn(string asMessage)
        {
            if (Program.Debug)
            {
                Console.WriteLine($"[WARN] {asMessage}");
            }
        }

        public static void Error(string asMessage)
        {
            if (Program.Debug)
            {
                Console.WriteLine($"[ERRO] {asMessage}");
            }
        }
    }

}
