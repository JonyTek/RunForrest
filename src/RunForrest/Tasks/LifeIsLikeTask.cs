﻿using System;
using RunForrest.Core.Attributes;

namespace RunForrest.Tasks
{
    public class LifeIsLikeTask
    {
        private readonly string what;

        private const string Taskname = "like";

        public LifeIsLikeTask(string what)
        {
            this.what = what;
        }

        [Task(Taskname)]
        public void ABoxOfChocolates()
        {
            Console.WriteLine("Life is like {0}", what);
        }
    }
}