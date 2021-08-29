using System;
using System.Threading.Tasks;
using FabricGroup.TestTask.ConsoleRunner.Ledger;
using FabricGroup.TestTask.ConsoleRunner.Ledger.Commands;
using NUnit.Framework;

namespace FabricGroup.TestTask.Ledger.Tests
{
    [TestFixture]
    public class PaymentCommandTests
    {
        private PaymentCommand _command;
        private BankBorrower _bb;
        private LedgerContext _context;
        private DummyOutput _output;

        [SetUp]
        public void Setup()
        {
            _command = new PaymentCommand();
            _bb = new BankBorrower("b", "b");
            _context = new LedgerContext();
            _output = new DummyOutput();
        }
        [Test]
        public void TestLoad()
        {
            _command.Load("PAYMENT IDIDI Dale 1000 5");

            Assert.AreEqual(5, _command.CommandData.EmiNo);
            Assert.AreEqual(1000, _command.CommandData.LumpAmount);
        }

        [Test]
        public async Task TestExecuteNoBorrower()
        {
            _command.CommandData = new PaymentCommand.Data(_bb, 100, 5);
            var context = new LedgerContext();
            var output = new DummyOutput();
            Assert.ThrowsAsync<ApplicationException>(() => _command.Execute(context, output));
        }


        [Test]
        public async Task TestExecute()
        {
            _command.CommandData = new PaymentCommand.Data(_bb, 100, 5);
            _context[_bb] = new BorrowerData(1, 10, 0.1m, 0m, 0);
            await _command.Execute(_context, _output);
            
            Assert.AreEqual(100m, _context[_bb].LumpAmount);
            Assert.AreEqual(5, _context[_bb].EmisPaid);
        }
    }
}