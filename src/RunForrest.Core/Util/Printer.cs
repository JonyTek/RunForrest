using System;
using RunForrest.Core.Model;

namespace RunForrest.Core.Util
{
    public static class Printer
    {
        private static RunForrestConfiguration config;

        internal static void Configure(RunForrestConfiguration configuration)
        {
            config = configuration;
        }

        public static void Info(string format, params object[] args)
        {
            Info(string.Format(format, args));
        }

        public static void Info(string output)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(output);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void Print(ConsoleColor color, string output)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(output);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void Print(ConsoleColor color, string format, params object[] args)
        {
            Print(color, string.Format(format, args));
        }

        public static void Error(string format, params object[] args)
        {
            Error(string.Format(format, args));
        }

        public static void Error(string output)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(output);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void Error(Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(ex);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        internal static void Error(Exception ex, bool verbodeMode)
        {
            var displayFullError = verbodeMode || config.IsVerbodeMode;
            if (displayFullError)
            {
                Error(ex);
            }
            else
            {
                Error(ex.InnerException?.Message ?? ex.Message);
            }
        }

        internal static void PrintHelp()
        {
            Info("Usage: <appname> <taskalias> <commands> ");
            Console.WriteLine();
            Info("-l -list\t\tlist available tasks\t\t<appname> -l");
            Info("-t -timed\t\ttime task execution\t\t<appname> -t");
            Info("-v -verbose\t\tprint exceptions to console\t<appname> -v");
            Info("-m -method\t\tpass method params\t\t<appname> <taskalias> -m arg1 arg2");
            Info("-c -constructor\t\tpass constructor args\t\t<appname> <taskalias> -c arg1 arg2");
            Info("-g -group\t\trun a group of tasks\t\t<appname> <groupalias> -g");
            Info("-p -parra\t\trun a group in parralell\t<appname> <groupalias> -g -p");

            Console.WriteLine();
            Info("Command v{0}", typeof(Printer).Assembly.GetName().Version.ToString());
        }
    }
}