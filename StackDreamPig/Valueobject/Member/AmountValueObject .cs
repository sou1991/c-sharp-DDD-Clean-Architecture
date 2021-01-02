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
            if (string.IsNullOrEmpty(monthlyIncome)) throw new ArgumentNullException(null, "月収の値が不正です。入力しなおしてください。");
            if (string.IsNullOrEmpty(savings)) throw new ArgumentNullException(null, "目標貯金額の値が不正です。入力しなおしてください。");
            if (string.IsNullOrEmpty(fixedCost)) throw new ArgumentNullException(null, "固定費の値が不正です。入力しなおしてください。");

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
