using Infrastructure;
using Application.Member.Model;
using System.Linq;
using Entities;
using Valueobject.Member;
using System;

namespace Application.Member.Commands
{
    public class CreateMemberCommand : ICreateMemberCommand
    {
        private IDataBaseService _dataBaseService;

        public CreateMemberCommand(IDataBaseService dataBaseService)
        {
            _dataBaseService = dataBaseService;
        }

        public void Execute(MemberModel memberModel)
        {
            var _memberEntity = new MemberEntity {
                userName = memberModel.userName,
                password = memberModel.password,
                monthlyIncome = memberModel.monthlyIncome,
                savings = memberModel.savings,
                fixedCost = memberModel.fixedCost,
                amountLimit = new AmountLimitValueObject(memberModel.amountLimit),
                intime = DateTime.Now
            };

            
            if (HasRegistMember(memberModel))
            {
                memberModel.isError = true;
                memberModel.errorMessege = "既に登録されたユーザーです。";
            }
            else 
            {
                _dataBaseService.Member.Add(_memberEntity);
                _dataBaseService.Save();
            }

     
        }

        public bool HasRegistMember(MemberModel memberModel)
        {
            var hasMember = _dataBaseService.Member
            .Where(p => p.userName == memberModel.userName && p.password == memberModel.password);

            var memberDataInTheDataBaseCnt = hasMember.Count();

            if (memberDataInTheDataBaseCnt == 0) return false;
            
            if (hasMember.Single().m_no != 0) return true; else return false;
        }

    }
}
