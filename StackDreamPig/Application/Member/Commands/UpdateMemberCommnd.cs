using Application.Member.Model;
using Common.Member;
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

            //To Do 不変性にしたいが更新処理O/Rマッパーがsetterを強要する。
            memberEntity.memberValueObject = SdpFactory.ValueObjectFactory().CreateMemberValueObject(memberModel.userName, memberEntity.memberValueObject.password, memberEntity.memberValueObject.saltPassword);
            memberEntity.amountValueObject = SdpFactory.ValueObjectFactory().CreateAmountValueObject(memberModel.monthlyIncome, memberModel.savings, memberModel.fixedCost);
            memberEntity.amountLimitValueObject = SdpFactory.ValueObjectFactory().CreateAmountLimitValueObject(memberModel.amountLimit);
            memberEntity.utime = DateTime.Now;
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
