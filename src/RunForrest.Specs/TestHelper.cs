using RunForrest.Core.Model;
using RunForrest.Core.Util;
using RunForrest.Specs.Model;

namespace RunForrest.Specs
{
    public class TestHelper
    {
        public static string Value { get; set; }

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
            Bootstrap(ignoreConfig);
            Printer.Configure(Config);
            return new InstructionBuilder(args, Config).Build();
        }
    }
}