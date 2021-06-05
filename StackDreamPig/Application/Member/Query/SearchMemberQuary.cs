using Application.Member.Model;
using System;
using Common.Member;
using System.Text.Json;
using Common;
using Npgsql;
using Infrastructure.Member;
using Entities.Member;

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
            var member = _memberRepository.GetUserWithUserName(memberModel.userName);

            if (member == null) return null;

            var dataModel = new MemberDataModelBuilder();

            member.Notice(dataModel);

            var dtoModel = dataModel.Build();

            //To Do 呼び出し元のコメント参照
            var salt =  JsonSerializer.Deserialize<byte[]>(dtoModel.saltPassword);

            if (VerifyPassword(dtoModel.password, memberModel.password, salt))
            {
                var memberEntity = _memberRepository.GetUser(memberModel.userName, dtoModel.password);

                var model = new MemberModel(){m_no = memberEntity.m_no};

                return model;
            }

            return null;

        }
       
        public IMemberDTO GetOneMember(IMemberDTO memberModel)
        {
            if (string.IsNullOrEmpty(memberModel.m_no)) throw new ArgumentNullException(null,"セッションが切れました。再度ログインしてください。");

            var memberEntity = _memberRepository.GetUserWithSession(memberModel.m_no);

            var dataModel = new MemberDataModelBuilder();

            memberEntity.Notice(dataModel);

            var dtoModel = dataModel.Build();

            var viewModel = new MemberModel
            {
                m_no = dtoModel.m_no,
                userName = dtoModel.userName,
                monthlyIncome = dtoModel.monthlyIncome,
                savings = dtoModel.savings,
                fixedCost = dtoModel.fixedCost,
                currencyTypeAmountLimit = CurrencyType.CastIntegerToCurrencyType(dtoModel.amountLimit)
            };

            return viewModel;
        }
    }
}
