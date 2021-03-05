using Application.Member.Model;
using Infrastructure;
using System;
using System.Linq;
using Common.Member;
using System.Text.Json;
using Common;
using Npgsql;
using Application.Member.DomainService;
using Infrastructure.Member;

namespace Application.Member.Query
{
    public class SearchMemberQuary : SecureService, ISearchMemberQuary
    {
        private IMemberRepository _memberRepository;

        public SearchMemberQuary(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public IMemberDTO Execute(IMemberDTO memberModel)
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
        public IMemberDTO AbleToLogin(IMemberDTO memberModel)
        {
            var securePassword = _memberRepository.GetSecurePassword(memberModel.userName);

            if (securePassword == null) return null;

            //To Do 呼び出し元のコメント参照
            var salt =  JsonSerializer.Deserialize<byte[]>(securePassword.memberValueObject.saltPassword);

            if (VerifyPassword(securePassword.memberValueObject.password, memberModel.password, salt))
            {
                var memberEntity = _memberRepository.Find(memberModel.userName, securePassword.memberValueObject.password);

                var domainModel = new MemberModel(){m_no = memberEntity.m_no};

                return domainModel;
            }

            return null;

        }
       
        public IMemberDTO GetOneMember(IMemberDTO memberModel)
        {
            if (string.IsNullOrEmpty(memberModel.m_no)) throw new ArgumentNullException(null,"セッションが切れました。再度ログインしてください。");

            var memberEntity = _memberRepository.FindSingle(memberModel.m_no);

            var domainModel = new MemberModel
            {
                m_no = memberEntity.m_no,
                userName = memberEntity.memberValueObject.userName,
                monthlyIncome = memberEntity.amountValueObject.monthlyIncome,
                savings = memberEntity.amountValueObject.savings,
                fixedCost = memberEntity.amountValueObject.fixedCost,
                currencyTypeAmountLimit = CurrencyType.CastIntegerToCurrencyType(memberEntity.amountLimitValueObject._amountLimit)
            };

            return domainModel;
        }
    }
}
