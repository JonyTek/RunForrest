using System;

namespace RunForrest.Core.Util
{
    public static class Printer
    {
        public static void Info(string format, params object[] args)
        {
            Info(string.Format(format, args));
        }

        public static void Info(string output)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(output);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void Error(Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(ex);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void Error(string format, params object[] args)
        {
            Info(string.Format(format, args));
        }

        public static void Print(ConsoleColor color, string output)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(output);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void Print(ConsoleColor color, string format, params object[] args)
        {
            Info(string.Format(format, args));
        }

        public static void Error(string output)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(output);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        internal static void PrintHelp()
        {
            Info("Usage: <appname> <taskalias> <commands> ");
            Console.WriteLine();
            Info("-l -list\t\tlist available tasks\t\t<appname> -l");
            Info("-t -timed\t\ttime task execution\t\t<appname> -t");
            Info("-v -verbose\t\tprint exceptions to console\t<appname> -v");
            Info("-m -method\t\tpass method params\t\t<appname> -m arg1 arg2");
            Info("-c -constructor\t\tpass constructor args\t\t<appname> -c arg1 arg2");

            Console.WriteLine();
            Info("Command v{0}", typeof(Printer).Assembly.GetName().Version.ToString());
        }
    }
}