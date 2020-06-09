using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace stackDreamPig.SeedWork
{
    public static class SdpCommon
    {
        public static int castIntoInteger(string value)
        {
            var success = int.TryParse(value, out var intger);
            if (success)
                return intger;
            else
                return 0;
        }
    }
}
