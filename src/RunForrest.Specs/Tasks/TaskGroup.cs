using System.Threading;
using RunForrest.Core.Attributes;
using RunForrest.Specs.Services;

namespace RunForrest.Specs.Tasks
{
    [TaskGroup("taskgroup1", "task group 1 description")]
    public class TaskGroup1
    {
        private readonly string value;

        private const string TaskOneName = "taskgrouponetaskone";

        private const string TaskTwoName = "taskgrouponetasktwo";

        public TaskGroup1(string value)
        {
            this.value = value;
        }

        [Task(TaskOneName, priority: 1)]
        public void DoSomethingOne(string s)
        {
            TestHelper.Value = value;
        }

        [Task(TaskTwoName, priority: 2)]
        public void DoSomethingTwo(string s)
        {
            TestHelper.Value1 = s;
        }
    }

    [TaskGroup("taskgroup", "task group description")]
    public class TaskGroup
    {
        private readonly string[] values = new string[2];

        private const string TaskOneName = "taskgrouptaskone";

        private const string TaskTwoName = "taskgrouptasktwo";

        private const string TaskThreeName = "taskgrouptaskthree";

        private const string TaskFourName = "taskgrouptaskfour";

        private readonly IService service;

        public TaskGroup(IService service)
        {
            this.service = service;
            TestHelper.ResetValueCollection();
        }

        [Task(TaskOneName, priority: 1)]
        public void DoSomethingOne()
        {
            values[0] = TaskOneName;
            TestHelper.Values.Add(1);

            OnAfterTask();
        }

        [Task(TaskTwoName, priority: 2)]
        public void DoSomethingTwo()
        {
            values[1] = TaskTwoName;
            TestHelper.Values.Add(2);

            OnAfterTask();
        }

        [Task(TaskThreeName, priority: 0)]
        public void DoSomethingThree()
        {
            Thread.Sleep(100);

            values[1] = TaskTwoName;
            TestHelper.Values.Add(0);
        }

        [Task(TaskFourName, priority: 0)]
        public void DoSomethingFour()
        {
            TestHelper.Value1 = service.GetString();
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