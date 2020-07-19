using Application.Member.Model;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using stackDreamPig.Models.Book.Query;

namespace Application.Member.Query
{
    public class SearchMemberQuary : ISearchMemberQuary
    {
        private IDataBaseService _dataBaseService;

        public SearchMemberQuary(IDataBaseService dataBaseService)
        {
            _dataBaseService = dataBaseService;
        }

        public LoginModel Execute(LoginModel loginModel)
        {

            var member = _dataBaseService.Member
            .Where(p => p.password == loginModel.password && p.userName == loginModel.userName)
            .Select(p => new LoginModel
            {
                m_no = p.m_no,
                password = p.password,
                userName = p.userName
            })
            .SingleOrDefault();

            var result = member == null ? null : member.m_no;

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
