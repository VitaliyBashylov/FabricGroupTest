using System.Threading;
using System.Threading.Tasks;
using FabricGroup.TestTask.ConsoleRunner.Ledger;
using FabricGroup.TestTask.ConsoleRunner.Ledger.Commands;
using NUnit.Framework;

namespace FabricGroup.TestTask.Ledger.Tests
{
    [TestFixture]
    public class LoanCommandTests
    {
        private LoanCommand _command;
        private BankBorrower _bb;
        private LedgerContext _context;
        private DummyOutput _output;

        [SetUp]
        public void Setup()
        {
            _command = new LoanCommand();
            _bb = new BankBorrower("b", "b");
            _context = new LedgerContext();
            _output = new DummyOutput();
        }

        [Test]
        public void TestLoad()
        {
            _command.Load("LOAN IDIDI Dale 10000 5 4");
            
            Assert.AreEqual(new BankBorrower("IDIDI", "Dale"), _command.CommandData.BankBorrower);
            Assert.AreEqual(0.04m, _command.CommandData.Interest);
            Assert.AreEqual(10000, _command.CommandData.Principal);
            Assert.AreEqual(5, _command.CommandData.NoOfYears);
        }

        [Test]
        public async Task TestExecute()
        {
            _command.CommandData = new LoanCommand.Data(_bb, 1000, 5, 0.3m);
            await _command.Execute(_context, _output);
            
            Assert.IsTrue(_context.HasBorrower(_bb));
            Assert.AreEqual(1000, _context[_bb].Principal);
            Assert.AreEqual(5, _context[_bb].LoanLengthYears);
            Assert.AreEqual(0.3m, _context[_bb].Interest);
            Assert.AreEqual(0, _output.Outputs.Count);
        }
        
    }
}