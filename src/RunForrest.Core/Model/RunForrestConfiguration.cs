using System;
using System.Collections.Generic;
using System.Reflection;

namespace RunForrest.Core.Model
{
    public class RunForrestConfiguration
    {
        #region Singleton

        private static RunForrestConfiguration instance;

        private static readonly object Lock = new object();

        internal static RunForrestConfiguration Instance
        {
            get
            {
                lock (Lock)
                {
                    return instance ?? (instance = new RunForrestConfiguration());
                }
            }
        }

        #endregion

        private RunForrestConfiguration()
        {
            OnBeforeEachTask = task => { };
            OnAfterEachTask = (task, returnValue) => { };
        }

        public bool IsTimedMode { internal get; set; }

        public bool IsVerbodeMode { internal get; set; }

        public Action<Task> OnBeforeEachTask { internal get; set; }

        public Action<Task, object> OnAfterEachTask { internal get; set; }

        public IEnumerable<Assembly> AdditionalAssembliesToScanForTasks { internal get; set; } 
    }
}