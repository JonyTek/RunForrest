﻿using System;
using RunForrest.Core.Attributes;
using RunnForrest = RunForrest.Core.RunForrest;

namespace RunForrest
{
    class Program
    {
        static void Main(string[] args)
        {
            RunnForrest.Run<Program>(args);

            Console.ReadKey();
        }
    }
}
