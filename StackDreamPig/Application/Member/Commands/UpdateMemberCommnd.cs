using Application.Member.Model;
using Common.Member;
using Entities.Member;
using Factory;
using Infrastructure.Member;
using Npgsql;
using System;

namespace Application.Member.Commands
{
    public class UpdateMemberCommnd : SecureService, IUpdateMemberCommnd
    {
        private IMemberRepository _memberRepository;
        private readonly string DUMMY_USER = "1";

        public UpdateMemberCommnd(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public void Execute(MemberModel memberModel)
        {
            if(memberModel.m_no == DUMMY_USER)
            {
                memberModel.isError = true;
                memberModel.errorMessege = "テストユーザーの為、変更できません。";
            }
            else
            {
                try
                {
                    if (CanUpdateMember(memberModel))
                    {
                        UpdateMember(memberModel);

                        _memberRepository.Save();
                    }
                    else
                    {
                        memberModel.isError = true;
                        memberModel.errorMessege = "既に登録されたユーザーです。";

                    }
                }
                catch (NpgsqlException)
                {
                    throw new Exception("データベース接続に失敗しました。");
                }
            }
        }

        public void UpdateMember(MemberModel memberModel)
        {
            var memberEntity = _memberRepository.GetUserWithSession(memberModel.m_no);

            var dataModel = new MemberDataModelBuilder();

            memberEntity.Notice(dataModel);

            var dtoModel = dataModel.Build();

            var memberValueObject = SdpFactory.ValueObjectFactory().CreateMemberValueObject(memberModel.userName, dtoModel.password, dtoModel.saltPassword);
            var amount = SdpFactory.ValueObjectFactory().CreateAmountValueObject(memberModel.monthlyIncome, memberModel.savings, memberModel.fixedCost);
            var amountLimit = SdpFactory.ValueObjectFactory().CreateAmountLimitValueObject(memberModel.amountLimit);

            var member = SdpFactory.EntityFactory().CreateMemberEntity(memberModel.m_no, memberValueObject, amount, amountLimit, DateTime.Now);

            _memberRepository.Update(member, memberModel.m_no);
        }

        public bool CanUpdateMember(MemberModel memberModel)
        {
            var memberEntity = _memberRepository.GetUserWithUserName(memberModel.userName);

            if (memberEntity != null)
            {
                if (memberEntity.m_no == memberModel.m_no)
                {
                    return true;
                }
                return false;
            }
            return true;
        }
    }
}
