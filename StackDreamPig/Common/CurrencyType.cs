
namespace Common
{
    public static class CurrencyType
    {
        public static string CastIntegerToCurrencyType(int value)
        {
            return string.Format("{0:N0}", value) + "円";
        }
    }
}
