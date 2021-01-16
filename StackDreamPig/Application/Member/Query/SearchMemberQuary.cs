using Application.Member.Model;
using Infrastructure;
using System;
using System.Linq;
using Common.Member;
using System.Text.Json;
using Common;
using Npgsql;

namespace Application.Member.Query
{
    public class SearchMemberQuary : SecureService, ISearchMemberQuary
    {
        private IDataBaseService _dataBaseService;

        public SearchMemberQuary(IDataBaseService dataBaseService)
        {
            _dataBaseService = dataBaseService;
        }

        public MemberModel Execute(MemberModel memberModel)
        {
            try
            {
                if (string.IsNullOrEmpty(memberModel.m_no))
                {
                    return AbleToLogin(memberModel);
                }
                else
                {
                    return GetOneMember(memberModel);
                }
            }
            catch (NpgsqlException)
            {
                throw new Exception("データベース接続に失敗しました。");
            }

        }
        public MemberModel AbleToLogin(MemberModel memberModel)
        {
            var securePassword = _dataBaseService.Member
                               .Where(p => p.memberValueObject.userName == memberModel.userName)
                               .SingleOrDefault();

            if (securePassword == null) return null;

            //To Do 呼び出し元のコメント参照
            var salt =  JsonSerializer.Deserialize<byte[]>(securePassword.memberValueObject.saltPassword);

            if (VerifyPassword(securePassword.memberValueObject.password, memberModel.password, salt))
            {
                var results = _dataBaseService.Member
                    .Where(p => p.memberValueObject.password == securePassword.memberValueObject.password && p.memberValueObject.userName == memberModel.userName)
                    .Select(p => new MemberModel
                    {
                        m_no = p.m_no
                    });

                var result = results.SingleOrDefault();
                return result;
            }

            return null;

        }
       
        public MemberModel GetOneMember(MemberModel memberModel)
        {
            if (string.IsNullOrEmpty(memberModel.m_no)) throw new ArgumentNullException(null,"セッションが切れました。再度ログインしてください。");

            var results = _dataBaseService.Member.Where(p => p.m_no == memberModel.m_no)
            .Select(p => new MemberModel
            {
                m_no = p.m_no,
                userName = p.memberValueObject.userName,
                monthlyIncome = p.amountValueObject.monthlyIncome,
                savings = p.amountValueObject.savings,
                fixedCost = p.amountValueObject.fixedCost,
                currencyTypeAmountLimit = CurrencyType.CastIntegerToCurrencyType(p.amountLimitValueObject._amountLimit)
            });

            var result = results.SingleOrDefault();
            return result;
        }
    }
}
