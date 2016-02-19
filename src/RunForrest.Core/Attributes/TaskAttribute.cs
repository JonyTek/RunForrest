using System;
using System.Security.AccessControl;
using RunForrest.Core.Util;

namespace RunForrest.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class TaskAttribute : Attribute
    {
        public string Alias { get; set; }

        public string Description { get; set; }

        public int Priority { get; set; }

        public TaskAttribute(string alias = null, string description = null, int priority = 0)
        {
            Alias = alias;
            Description = description ?? Constants.NoDescriptionText;
            Priority = priority;
        }
    }
}