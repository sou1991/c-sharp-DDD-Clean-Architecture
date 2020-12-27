using System;
using System.Collections.Generic;
using System.Text;

namespace Valueobject.Member
{
    public class AmountLimitValueObject : ValueObject<AmountLimitValueObject>
    {
        //DIの関係でフィールドをpublicにし、setter側でprivateにする
        public int _amountLimit{ get; private set; }

        public AmountLimitValueObject(int amountLimit)
        {
            _amountLimit = amountLimit;
        }
        public  override bool ClientConditionEquals(AmountLimitValueObject that)
        {
            return this._amountLimit == that._amountLimit;
        }
    }
}
