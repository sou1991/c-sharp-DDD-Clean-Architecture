using Application.Member.Model;
using Common.Member;
using Entities;
using Factory;
using Infrastructure;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Valueobject.Member;

namespace Application.Member.Commands
{
    public class UpdateMemberCommnd : SecureService, IUpdateMemberCommnd
    {
        private IDataBaseService _dataBaseService;
        private readonly int DUMMY_USER = 1;

        public UpdateMemberCommnd(IDataBaseService dataBaseService)
        {
            _dataBaseService = dataBaseService;
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

                        _dataBaseService.Save();
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
            var member = _dataBaseService.Member
            .Where(p => p.m_no == memberModel.m_no).First();

            //To Do 不変性にしたいが更新処理O/Rマッパーがsetterを強要する。
            member.memberValueObject = SdpFactory.ValueObjectFactory().CreateMemberValueObject(memberModel.userName, member.memberValueObject.password, member.memberValueObject.saltPassword);
            member.amountValueObject = SdpFactory.ValueObjectFactory().CreateAmountValueObject(memberModel.monthlyIncome, memberModel.savings, memberModel.fixedCost);
            member.amountLimitValueObject = SdpFactory.ValueObjectFactory().CreateAmountLimitValueObject(memberModel.amountLimit);
            member.utime = DateTime.Now;
        }

        public bool CanUpdateMember(MemberModel memberModel)
        {
            var member = _dataBaseService.Member
            .Where(p => p.memberValueObject.userName == memberModel.userName);

            if (member.Any())
            {
                if (member.First().m_no == memberModel.m_no)
                {
                    return true;
                }
                return false;
            }
            return true;
        }
    }
}
