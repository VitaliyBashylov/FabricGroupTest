using System.Collections.Generic;

namespace FabricGroup.TestTask.ConsoleRunner.Ledger
{
    public record BorrowerData (
        int LoanLengthYears, decimal Principal, decimal Interest, decimal LumpAmount, int EmisPaid
        );
    public class LedgerContext
    {
        private readonly Dictionary<BankBorrower, BorrowerData> _borrowers = new();
        public bool HasBorrower(BankBorrower bb) => _borrowers.ContainsKey(bb);
        public BorrowerData this[BankBorrower bankBorrower]
        {
            get => _borrowers[bankBorrower]; 
            set => _borrowers[bankBorrower] = value;
        }
    }
}