using RunForrest.Core.Attributes;

namespace RunForrest.Specs.Tasks
{
    public class BasicTask
    {
        private const string TaskName = "basictask";

        private const string TaskDescription = "basic task description";

        [Task(TaskName, TaskDescription, 10)]
        public void DoSomething()
        {
            TestHelper.Value = TaskName;
        }
    }
}