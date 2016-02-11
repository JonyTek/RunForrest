//using System;
//using NUnit.Framework;
//using RunForrest.Core.Model;
//using RunForrest.Core.Runners;
//using RunForrest.Core.Util;
//using Assert = NUnit.Framework.Assert;

//namespace RunForrest.Specs
//{
//    [TestFixture]
//    public class InstructionParserSpecs
//    {
//        [SetUp]
//        public void OnBeforeEachTest()
//        {
//        }

//        [Test]
//        public void ShouldSetStateToDisplayHelpIfNoArgsAndNoConfig()
//        {
//            var input = new string[0];
//            var instructions = InstructionParser.ParseInstructions(input, new RunForrestConfiguration());

//            Assert.That(instructions.Runner is ExecuteHelpInstructions, Is.True);
//            Assert.That(instructions.ApplicationMode, Is.EqualTo(ApplicationMode.Help));
//        }

//        [Test]
//        public void ShouldSetStateToGroupIfNoArgsAndGroupAndAliasSetInConfig()
//        {
//            var input = new string[0];
//            var config = new RunForrestConfiguration { ExecuteAlias = "alias", IsInGroupMode = true };
//            var instructions = InstructionParser.ParseInstructions(input, config);

//            Assert.That(instructions.ExecuteAlias, Is.EqualTo("alias"));
//            Assert.That(instructions.Runner is ExecuteGroupTaskInstructions, Is.True);
//            Assert.That(instructions.ApplicationMode, Is.EqualTo(ApplicationMode.Group));
//        }

//        [Test]
//        public void ShouldSetStateToSingleIfNoArgsAndAliasSetInConfig()
//        {
//            var input = new string[0];
//            var config = new RunForrestConfiguration { ExecuteAlias = "alias" };
//            var instructions = InstructionParser.ParseInstructions(input, config);

//            Assert.That(instructions.ExecuteAlias, Is.EqualTo("alias"));
//            Assert.That(instructions.Runner is ExecuteSingleTaskInstructions, Is.True);
//            Assert.That(instructions.ApplicationMode, Is.EqualTo(ApplicationMode.Single));
//        }


//        [Test]
//        public void ShouldSetListInstructions()
//        {
//            var input = new[] { "-l" };
//            var instructions = InstructionParser.ParseInstructions(input, new RunForrestConfiguration());

//            Assert.That(instructions.Runner is ExecuteListInstructions, Is.True);
//            Assert.That(instructions.ApplicationMode, Is.EqualTo(ApplicationMode.List));
//        }

//        [Test]
//        public void ShouldSetListInstructionsAndThrowWhenCalledIfSpecifiedTaskAlias()
//        {
//            var input = new[] { "alias", "-l" };
//            var config = new RunForrestConfiguration();
//            var instructions = InstructionParser.ParseInstructions(input, config);

//            Assert.That(instructions.ApplicationMode, Is.EqualTo(ApplicationMode.List));
//            Assert.Throws<ArgumentException>(() => instructions.Runner.Execute(instructions, config));
//        }

//        [Test]
//        public void ShouldSetDisplayWhenCalledIfSpecifiedTaskAlias()
//        {
//            var input = new[] { "alias", "-h" };
//            var instructions = InstructionParser.ParseInstructions(input, new RunForrestConfiguration());

//            Assert.That(instructions.Runner is ExecuteHelpInstructions, Is.True);
//            Assert.That(instructions.ApplicationMode, Is.EqualTo(ApplicationMode.Help));
//        }

//        [Test]
//        public void ShouldSetListInstructionsAndThrowWhenCalledIfSpecifiedAdditionalSwitches()
//        {
//            var input = new[] { "-l", "-v" };
//            var config = new RunForrestConfiguration();
//            var instructions = InstructionParser.ParseInstructions(input, new RunForrestConfiguration());

//            Assert.That(instructions.ApplicationMode, Is.EqualTo(ApplicationMode.List));
//            Assert.Throws<ArgumentException>(() => instructions.Runner.Execute(instructions, config));
//        }

//        [Test]
//        public void ShouldSetHelpInstructions()
//        {
//            var input = new[] { "-h" };
//            var instructions = InstructionParser.ParseInstructions(input, new RunForrestConfiguration());

//            Assert.That(instructions.Runner is ExecuteHelpInstructions, Is.True);
//            Assert.That(instructions.ApplicationMode, Is.EqualTo(ApplicationMode.Help));
//        }

//        [Test]
//        public void ShouldSetHelpInstructionsIfNoArgumentsPassed()
//        {
//            var input = new string[0];
//            var instructions = InstructionParser.ParseInstructions(input, new RunForrestConfiguration());

//            Assert.That(instructions.Runner is ExecuteHelpInstructions, Is.True);
//            Assert.That(instructions.ApplicationMode, Is.EqualTo(ApplicationMode.Help));
//        }

//        [Test]
//        public void ShouldSetHelpInstructionsAndThrowWhenCalledIfSpecifiedAdditionalSwitches()
//        {
//            var input = new[] { "-h", "-v" };
//            var config = new RunForrestConfiguration();
//            var instructions = InstructionParser.ParseInstructions(input, config);

//            Assert.That(instructions.ApplicationMode, Is.EqualTo(ApplicationMode.Help));
//            Assert.Throws<ArgumentException>(() => instructions.Runner.Execute(instructions, config));            
//        }

//        [Test]
//        public void ShouldSetTaskInstructions()
//        {
//            var input = new[] {"alias", "-m", "method1", "method2", "-c", "input1", "input2", "-v", "-t"};
//            var instructions = InstructionParser.ParseInstructions(input, new RunForrestConfiguration());

//            Assert.That(instructions.TimedMode, Is.True);
//            Assert.That(instructions.VerbodeMode, Is.True);
//            Assert.That(instructions.Runner is ExecuteSingleTaskInstructions, Is.True);
//            Assert.That(instructions.ApplicationMode, Is.EqualTo(ApplicationMode.Single));
//            Assert.That(instructions.Instructions[SwitchType.Method], Is.EquivalentTo(new[] { "method1", "method2" }));
//            Assert.That(instructions.Instructions[SwitchType.Constructor], Is.EquivalentTo(new[] { "input1", "input2" }));
//        }

//        [Test]
//        public void ShouldSetTaskGroupInstructions()
//        {
//            var input = new[] { "alias", "-g" };
//            var instructions = InstructionParser.ParseInstructions(input, new RunForrestConfiguration());

//            Assert.That(instructions.Runner is ExecuteGroupTaskInstructions, Is.True);
//            Assert.That(instructions.ApplicationMode, Is.EqualTo(ApplicationMode.Group));
//        }

//        [Test]
//        public void ShouldSetTaskGroupInstructionsFromConfig()
//        {
//            var input = new string[0];
//            var config = new RunForrestConfiguration { ExecuteAlias = "alias", IsInGroupMode = true };
//            var instructions = InstructionParser.ParseInstructions(input, config);

//            Assert.That(instructions.Runner is ExecuteGroupTaskInstructions, Is.True);
//            Assert.That(instructions.ApplicationMode, Is.EqualTo(ApplicationMode.Group));
//        }

//        [Test]
//        public void ShouldSetTaskGroupInstructionsFromConfigAndInput()
//        {
//            var input = new[] { "alias" };
//            var config = new RunForrestConfiguration { IsInGroupMode = true };
//            var instructions = InstructionParser.ParseInstructions(input, config);

//            Assert.That(instructions.ExecuteAlias, Is.EqualTo("alias"));
//            Assert.That(instructions.Runner is ExecuteGroupTaskInstructions, Is.True);
//            Assert.That(instructions.ApplicationMode, Is.EqualTo(ApplicationMode.Group));
//        }

//        [Test]
//        public void ShouldSetTaskGroupInstructionsFromConfigAndInput1()
//        {
//            var input = new[] { "-g" };
//            var config = new RunForrestConfiguration { ExecuteAlias = "alias"};
//            var instructions = InstructionParser.ParseInstructions(input, config);

//            Assert.That(instructions.ExecuteAlias, Is.EqualTo("alias"));
//            Assert.That(instructions.Runner is ExecuteGroupTaskInstructions, Is.True);
//            Assert.That(instructions.ApplicationMode, Is.EqualTo(ApplicationMode.Group));
//        }
//    }
//}