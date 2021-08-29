using System.Linq;
using System.Threading.Tasks;
using FabricGroup.TestTask.ConsoleRunner.Ledger;
using FabricGroup.TestTask.ConsoleRunner.Ledger.Commands;
using NuGet.Frameworks;
using NUnit.Framework;

namespace FabricGroup.TestTask.Ledger.Tests
{
    [TestFixture]
    public class CommandProcessorTests
    {
        private CommandProcessor _processor;
        private DummyOutput _output;

        [SetUp]
        public void Setup()
        {
            _output = new DummyOutput();
            _processor = new CommandProcessor(_output);
            _processor.RegisterCommand("loan", () => new LoanCommand());
            _processor.RegisterCommand("payment", () => new PaymentCommand());
            _processor.RegisterCommand("balance", () => new BalanceCommand(new BalanceCalculator()));
        }
        [Test]
        public void TestLoad()
        {
            var input = new[]
            {
                "LOAN IDIDI Dale 5000 1 6",
                "PAYMENT IDIDI Dale 1000 5",
                "BALANCE IDIDI Dale 6",
            };
            var commands = _processor.Load(input).ToList();
            Assert.AreEqual(3, commands.Count);
            Assert.IsInstanceOf<LoanCommand>(commands[0]);
            Assert.IsInstanceOf<PaymentCommand>(commands[1]);
            Assert.IsInstanceOf<BalanceCommand>(commands[2]);
        }
        [Test]
        public async Task TestExecute()
        {
            var input = new[]
            {
                "LOAN IDIDI Dale 5000 1 6",
                "PAYMENT IDIDI Dale 1000 5",
                "BALANCE IDIDI Dale 6",
            };
            var commands = _processor.Load(input);
            await _processor.Execute(commands);
            Assert.AreEqual(1, _output.Outputs.Count);
            Assert.AreEqual(3652, _output.Outputs[0].AmountPaid);
            Assert.AreEqual(4, _output.Outputs[0].EmisLeft);
        }
    }
}