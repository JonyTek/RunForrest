using RunForrest.Core.Attributes;

namespace RunForrest.Specs.Tasks
{
    [TaskGroup("taskgroup", "task group description")]
    public class TaskGroup
    {
        private readonly string[] values = new string[2];

        private const string TaskOneName = "taskgrouptaskone";

        private const string TaskTwoName = "taskgrouptasktwo";

        [Task(TaskOneName, priority: 1)]
        public void DoSomethingOne()
        {
            values[0] = TaskOneName;

            OnAfterTask();
        }

        [Task(TaskTwoName, priority: 2)]
        public void DoSomethingTwo()
        {
            values[1] = TaskTwoName;

            OnAfterTask();
        }

        private void OnAfterTask()
        {
            if (!string.IsNullOrEmpty(values[0]) && !string.IsNullOrEmpty(values[1]))
            {
                TestHelper.Value = string.Join(" ", values).TrimEnd();
            }
        }
    }
}