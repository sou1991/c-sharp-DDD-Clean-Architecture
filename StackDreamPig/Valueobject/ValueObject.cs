using System;
using System.Collections.Generic;
using System.Text;

namespace Valueobject
{
        public abstract class ValueObject<T> where T : ValueObject<T>
        {
            public override bool Equals(object obj)
            {
                var vo = obj as T;
  
                if (vo == null)
                {
                    return false;
                }

                return ClientConditionEquals(vo);
            }

            public static bool operator == (ValueObject<T> vo1, ValueObject<T> vo2)
            {
                return Equals(vo1, vo2);
             }

            public static bool operator !=(ValueObject<T> vo1, ValueObject<T> vo2)
            {
                return !Equals(vo1, vo2);
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }

            public abstract bool ClientConditionEquals(T other);
        }
}
