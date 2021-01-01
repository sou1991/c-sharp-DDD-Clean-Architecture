using System;
using System.Collections.Generic;
using System.Text;

namespace Valueobject.Member
{
    public class AmountValueObject : ValueObject<AmountValueObject>
    {
        //DIの関係でフィールドをpublicにし、setter側でprivateにする
        public string monthlyIncome { get; private set; }

        public string savings { get; private set; }

        public string fixedCost { get; private set; }

        public AmountValueObject(string monthlyIncome, string savings, string fixedCost)
        {
            this.monthlyIncome = monthlyIncome;
            this.savings = savings;
            this.fixedCost = fixedCost;

        }
        public override bool ClientConditionEquals(AmountValueObject that)
        {
            return this.monthlyIncome == that.monthlyIncome &&
                   this.savings == that.savings &&
                   this.fixedCost == that.fixedCost;

        }
    }
}
