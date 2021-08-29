using System;

namespace FabricGroup.TestTask.ConsoleRunner.Ledger
{
    public record BalanceResult (int AmountPaid, int EmisLeft);

    public interface IBalanceCalculator
    {
        BalanceResult Calculate(BorrowerData data, int emiNo);
    }

    public class BalanceCalculator : IBalanceCalculator
    {
        public BalanceResult Calculate(BorrowerData data, int emiNo)
        {
            var totalInterest = data.Principal * data.LoanLengthYears * data.Interest;
            var totalPayment = data.Principal + totalInterest;
            var emi = (int)Math.Ceiling(totalPayment / data.LoanLengthYears / 12);

            var paidSoFar = emiNo * emi + (data.EmisPaid <= emiNo ? data.LumpAmount : 0);
            //managing last and all next emis
            //in case we supply emi number far away from the last emi we don't throw we just return that everything is paid. 
            if (paidSoFar > totalPayment)
                paidSoFar = totalPayment;
            
            var paymentLeft = totalPayment - paidSoFar;
            var emisLeft = (int)Math.Ceiling(paymentLeft / emi);
            
            return new BalanceResult((int)Math.Ceiling(paidSoFar), emisLeft);
        }
    }
}