using System;
using System.Threading.Tasks;
using FabricGroup.TestTask.ConsoleRunner.Ledger;
using FabricGroup.TestTask.ConsoleRunner.Ledger.Commands;
using NUnit.Framework;

namespace FabricGroup.TestTask.Ledger.Tests
{
    [TestFixture]
    public class BalanceCommandTests
    {
        private BalanceCommand _command;
        private BankBorrower _bb;
        private LedgerContext _context;
        private DummyOutput _output;

        [SetUp]
        public void Setup()
        {
            _command = new BalanceCommand(new DummyCalculator(new BalanceResult(1, 1)));
            _bb = new BankBorrower("b", "b");
            _context = new LedgerContext();
            _output = new DummyOutput();
        }

        [Test]
        public void TestLoad()
        {
            _command.Load("BALANCE IDIDI Dale 3");

            Assert.AreEqual(3, _command.CommandData.EmiNo);
        }
        [Test]
        public async Task TestNoBorrower()
        {
            _command.CommandData = new BalanceCommand.Data(_bb, 1);
            Assert.ThrowsAsync<ApplicationException>(() => _command.Execute(_context, _output));
        }
        
        [Test]
        public async Task TestExecute()
        {
            _command.CommandData = new BalanceCommand.Data(_bb, 5);
            _context[_bb] = new BorrowerData(1, 10, 0.1m, 1m, 3);
            await _command.Execute(_context, _output);

            Assert.AreEqual(1, _output.Outputs.Count);
            Assert.AreEqual(1, _output.Outputs[0].AmountPaid);
            Assert.AreEqual(5, _output.Outputs[0].EmisLeft);
        }

    }
}