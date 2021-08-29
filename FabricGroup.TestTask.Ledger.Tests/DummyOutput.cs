using System.Collections.Generic;
using System.Threading.Tasks;
using FabricGroup.TestTask.ConsoleRunner.Ledger;

namespace FabricGroup.TestTask.Ledger.Tests
{
    public class DummyOutput: IOutput
    {
        public List<BalanceOutput> Outputs { get; } = new();
        public Task Send(BalanceOutput output)
        {
            Outputs.Add(output);
            return Task.CompletedTask;
        }
    }
}