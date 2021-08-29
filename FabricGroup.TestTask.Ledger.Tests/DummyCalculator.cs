using FabricGroup.TestTask.ConsoleRunner.Ledger;

namespace FabricGroup.TestTask.Ledger.Tests
{
    public class DummyCalculator: IBalanceCalculator
    {
        private readonly BalanceResult _result;

        public DummyCalculator(BalanceResult result)
        {
            _result = result;
        }
        public BalanceResult Calculate(BorrowerData data, int emiNo)
        {
            return _result with { EmisLeft = emiNo };
        }
    }
}