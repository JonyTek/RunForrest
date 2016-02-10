using System.Collections.Generic;
using RunForrest.Core.Attributes;
using RunForrest.Core.Model;

namespace RunForrest.Specs
{
    public class Tasks
    {
        [Task("mytask")]
        public void MyTask(string input)
        {
        }
    }
}