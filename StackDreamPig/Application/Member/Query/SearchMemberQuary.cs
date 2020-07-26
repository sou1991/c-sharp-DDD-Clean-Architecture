using Application.Member.Model;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using stackDreamPig.Models.Book.Query;
using Common.Member;

namespace Application.Member.Query
{
    public class SearchMemberQuary : ISearchMemberQuary
    {
        private IDataBaseService _dataBaseService;

        public SearchMemberQuary(IDataBaseService dataBaseService)
        {
            _dataBaseService = dataBaseService;
        }

        public MemberModel Execute(MemberModel memberModel)
        {
            
            if (memberModel.m_no == (int)EnumMember.NON_MEMBER)
            {
                return CheckLogin(memberModel);
            }
            else
            {
                return GetOneMember(memberModel);
            }
            
        }
        public MemberModel CheckLogin(MemberModel memberModel)
        {
            var results = _dataBaseService.Member.Where(p => p.password == memberModel.password && p.userName == memberModel.userName)
            .Select(p => new MemberModel
            {
               m_no = p.m_no,
               password = p.password,
               userName = p.userName
            });
            var result = results.SingleOrDefault();
            return result;

        }

        public MemberModel GetOneMember(MemberModel memberModel)
        {
            var results = _dataBaseService.Member.Where(p => p.m_no == memberModel.m_no)
            .Select(p => new MemberModel
            {
                m_no = p.m_no,
                userName = p.userName,
                monthlyIncome = p.monthlyIncome,
                savings = p.savings,
                fixedCost = p.fixedCost,
                dispAmountLimit = p.amountLimit._amountLimit
            });
            
            var result = results.SingleOrDefault();
            return result;
        }
    }
}
