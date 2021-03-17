
namespace Application.Member.Model
{
    public interface IMemberDTO
    {
        string m_no { get; }

        string userName { get; }

        string password { get; }

        string monthlyIncome { get;}

        string savings { get; }

        string fixedCost { get; }

        int amountLimit { get; }

        string currencyTypeAmountLimit { get; }
    }
}
