using System;
using System.Collections.Generic;
using System.Text;

namespace Valueobject.Books
{
    public class RegistDateValueObject : ValueObject <RegistDateValueObject>
    {
        //DIの関係でフィールドをpublicにし、setter側でprivateにする
        public DateTime _registDate { get; private set; }

        public RegistDateValueObject(DateTime registDate)
        {
            if (registDate == default(DateTime)) throw new ArgumentNullException(null, "帳簿登録日が不正です。");
            _registDate = registDate;
        }

        public override bool ClientConditionEquals(RegistDateValueObject that)
        {
            return this._registDate == that._registDate;
        }
    }
}
