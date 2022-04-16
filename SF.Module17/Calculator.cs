using System;
using System.Collections.Generic;

namespace SF.Module17
{
    class Calculator
    {
        public static void CalculateInterest(List<IInterest> accounts)
        {
            accounts.ForEach(acc => acc.CalculateInterest());
        }
    }
}