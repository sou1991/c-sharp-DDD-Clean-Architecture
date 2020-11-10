using Application.Member.Model;
using Common.Member;
using Entities;
using Infrastructure;
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

        public UpdateMemberCommnd(IDataBaseService dataBaseService)
        {
            _dataBaseService = dataBaseService;
        }

        public void Execute(MemberModel memberModel)
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

        public void UpdateMember(MemberModel memberModel)
        {
            var member = _dataBaseService.Member
            .Where(p => p.m_no == memberModel.m_no).First();

            member.userName = memberModel.userName;
            member.monthlyIncome = memberModel.monthlyIncome;
            member.savings = memberModel.savings;
            member.fixedCost = memberModel.fixedCost;
            member.amountLimit = new AmountLimitValueObject(memberModel.amountLimit);
            member.utime = DateTime.Now;

        }

        public bool CanUpdateMember(MemberModel memberModel)
        {
            var member = _dataBaseService.Member
            .Where(p => p.userName == memberModel.userName);

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
