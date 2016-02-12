using System.Collections.Generic;
using System.Linq;

namespace RunForrest.Core.Model
{
    internal class TaskGroup
    {
        private IEnumerable<BasicTask> tasks;

        internal string Alias { get; set; }

        internal string Description { get; set; }

        internal IEnumerable<BasicTask> Tasks
        {
            get { return tasks.OrderBy(x => x.Priority); }
            set { tasks = value; }
        }
    }
}