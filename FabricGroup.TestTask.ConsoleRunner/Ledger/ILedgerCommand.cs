using System.Threading.Tasks;

namespace FabricGroup.TestTask.ConsoleRunner.Ledger
{
    public interface ILedgerCommand
    {
        void Load(string input);
        Task Execute(LedgerContext context, IOutput output);
    }
}