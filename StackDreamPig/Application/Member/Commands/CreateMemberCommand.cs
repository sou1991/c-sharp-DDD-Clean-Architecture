﻿using Infrastructure;
using Application.Member.Model;
using System.Linq;
using Entities;
using Valueobject.Member;
using System;
using Common.Member;

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
            var member = _dataBaseService.Member
            .Where(p => p.userName == memberModel.userName && p.password == memberModel.password);

            var memberDataInTheDataBaseCnt = member.Count();

            if (memberDataInTheDataBaseCnt == (int) EnumMember.NON_MEMBER) return false;
            
            if (member.Single().m_no != (int)EnumMember.NON_MEMBER) return true; else return false;
        }

    }
}
