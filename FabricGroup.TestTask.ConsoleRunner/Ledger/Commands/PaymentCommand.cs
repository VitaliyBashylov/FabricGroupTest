using System.Threading.Tasks;

namespace FabricGroup.TestTask.ConsoleRunner.Ledger.Commands
{
    public class PaymentCommand: ILedgerCommand
    {
        public record Data(BankBorrower BankBorrower, decimal LumpAmount, int EmiNo);

        private Data _data;
        public void Load(string input)
        {
            var split = input.Split(" ");
            _data = new Data(
                new BankBorrower(split[1], split[2]), 
                decimal.Parse(split[3]), 
                int.Parse(split[4])
                );
        }
        public void SetData(Data data) => _data = data;

        public Task Execute(LedgerContext context, IOutput output)
        {
            var bd = context[_data.BankBorrower];

            context[_data.BankBorrower] = bd with
            {
                LumpAmount = _data.LumpAmount,
                EmisPaid = _data.EmiNo
            };

            return Task.CompletedTask;
        }
    }
}