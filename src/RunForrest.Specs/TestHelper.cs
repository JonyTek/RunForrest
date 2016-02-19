using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using NUnit.Framework;
using RunForrest.Core.Model;
using RunForrest.Core.Util;
using RunForrest.Specs.Model;

namespace RunForrest.Specs
{
    public class TestHelper
    {
        public static string Value { get; set; }

        public static string Value1 { get; set; }

        public static string Value2 { get; set; }

        public static List<object> Values { get; set; }

        public static void ResetValueCollection()
        {
            Values = new List<object>();
        }

        private static bool isSetup;

        public static ApplicationConfiguration Config;

        public static void Bootstrap(bool ignoreConfig = false)
        {
            if (!isSetup)
            {
                Config = ApplicationConfiguration.Bootstrap<ApplicationInstructionsSpecs>(ignoreConfig);

                TaskCollection.Initialise(Config);

                isSetup = true;
            }
        }

        internal static ApplicationInstructions GenerateInstructions(string[] args, bool ignoreConfig = false)
        {
            Printer.Configure(Config);
            return new InstructionBuilder(args, Config).Build();
        }
    }

    [SetUpFixture]
    public class SetupFixture
    {
        [SetUp]
        public void Enter()
        {
            TestHelper.Bootstrap();
        }
    }
}