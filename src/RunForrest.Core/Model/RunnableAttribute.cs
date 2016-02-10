using System;

namespace RunForrest.Core.Model
{
    [AttributeUsage(AttributeTargets.Method)]
    public class RunnableAttribute : Attribute
    {
        public RunnableAttribute(string alias = null, string description = null)
        {
            Alias = alias;
            Description = description ?? "<No Description>";
        }

        public string Alias { get; set; }

        public string Description { get; set; }

    }
}