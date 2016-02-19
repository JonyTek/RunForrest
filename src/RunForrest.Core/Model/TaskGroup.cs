using System.Collections.Generic;
using System.Linq;
using RunForrest.Core.Util;

namespace RunForrest.Core.Model
{
    internal class TaskGroup
    {
        private IEnumerable<AbstractTask> tasks;

        internal string Alias { get; set; }

        internal string Description { get; set; }

        internal IEnumerable<AbstractTask> Tasks
        {
            get { return tasks.OrderBy(x => x.Priority); }
            set { tasks = value; }
        }

        internal object ExecuteOnInstance(ApplicationInstructions instructions)
        {
            return tasks.ToInstanceToExecuteOn(instructions);
        }
    }
}