using System.Threading.Tasks;

namespace FabricGroup.TestTask.ConsoleRunner.Ledger
{
    public record BalanceOutput(BankBorrower BankBorrower, int AmountPaid, int EmisLeft);
    public interface IOutput
    {
        Task Send(BalanceOutput output);
    }

}