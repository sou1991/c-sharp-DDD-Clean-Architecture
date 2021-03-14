using System;
using Valueobject.Books;
using Valueobject.Member;

namespace Factory
{
    public class ValueObjectFactory
    {
        public AmountLimitValueObject CreateAmountLimitValueObject(int amountLimit) 
            => new AmountLimitValueObject(amountLimit);

        public AmountValueObject CreateAmountValueObject(string monthlyIncome, string savings, string fixedCost)
            => new AmountValueObject(monthlyIncome, savings, fixedCost);

        public MemberValueObject CreateMemberValueObject(string userName, string password, string saltPassword)
            => new MemberValueObject(userName, password, saltPassword);

        public RegistDateValueObject CreateRegistDateValueObject(DateTime registDate)
            => new RegistDateValueObject(registDate);
    }
}
