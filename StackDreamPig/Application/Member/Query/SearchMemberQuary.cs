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
            var member = _dataBaseService.Member
            .Where(p => p.password == memberModel.password && p.userName == memberModel.userName)
            .Select(p => new MemberModel
            {
                m_no = p.m_no,
                password = p.password,
                userName = p.userName
            })
            .SingleOrDefault();

            return member;
            
        }

        public int GetMembersBooks(int m_no)
        {
            var amountLimit = _dataBaseService.Member
            .Where(p => p.m_no == m_no)
            .Select(p => new BooksModel
            {
                amountLimit = p.amountLimit._amountLimit
            })
            .SingleOrDefault();

            return amountLimit.amountLimit;
        }
    }
}
