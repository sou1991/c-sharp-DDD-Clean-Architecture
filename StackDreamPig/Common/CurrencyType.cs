﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public static class CurrencyType
    {
        public static string CastIntegerToCurrencyType(int value)
        {
            return string.Format("{0:c}", value);
        }
    }
}
