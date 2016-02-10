using System;

namespace RunForrest.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TaskGroupAttribute : Attribute
    {
        public TaskGroupAttribute(string alias = null, string description = null)
        {            
            Alias = alias;
            Description = description ?? "<No Description>";
        }

        public string Alias { get; set; }

        public string Description { get; set; }

    }
}