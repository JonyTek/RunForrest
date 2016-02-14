using System;
using RunForrest.Core.Util;

namespace RunForrest.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TaskGroupAttribute : Attribute
    {
        public TaskGroupAttribute(string alias = null, string description = null)
        {            
            Alias = alias;
            Description = description ?? Constants.NoDescriptionText;
        }

        public string Alias { get; set; }

        public string Description { get; set; }

    }
}