using System;
using System.Collections.Generic;
using System.Text;

namespace Valueobject.Books
{
    public class RegistDateValueObject : ValueObject <RegistDateValueObject>
    {
        //DIの関係でフィールドをpublicにし、プロパティをprivateにする
        public DateTime _registDate { get; private set; }

        public RegistDateValueObject(DateTime registDate)
        {
            _registDate = registDate;
        }

        public override bool ClientConditionEquals(RegistDateValueObject that)
        {
            return this._registDate == that._registDate;
        }
    }
}
