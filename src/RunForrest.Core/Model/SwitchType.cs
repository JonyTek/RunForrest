namespace RunForrest.Core.Model
{
    internal enum InstructionType
    {
        None = 0,

        Constructor = 1,

        Method = 2,

        Group = 3,

        Verbose = 10,

        Timed = 11,

        Parallel = 12,

        DisplayList = 20,

        DisplayHelp = 21
    }
}