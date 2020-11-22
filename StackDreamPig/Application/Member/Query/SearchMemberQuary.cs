using Application.Member.Model;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using stackDreamPig.Models.Book.Query;
using Common.Member;
using System.Text.Json;
using Common;
using Npgsql;

namespace Application.Member.Query
{
    public class SearchMemberQuary : SecureService, ISearchMemberQuary
    {
        private IDataBaseService _dataBaseService;

        public SearchMemberQuary(IDataBaseService dataBaseService)
        {
            _dataBaseService = dataBaseService;
        }

        public MemberModel Execute(MemberModel memberModel)
        {
            try
            {
                if (memberModel.m_no == (int)EnumMember.NON_MEMBER)
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
        public MemberModel AbleToLogin(MemberModel memberModel)
        {
            var securePassword = _dataBaseService.Member
                               .Where(p => p.userName == memberModel.userName)
                               .SingleOrDefault();

            if (securePassword == null) return null;

            //To Do 呼び出し元のコメント参照
            var salt =  JsonSerializer.Deserialize<byte[]>(securePassword.saltPassword);

            if (VerifyPassword(securePassword.password,memberModel.password, salt))
            {
                var results = _dataBaseService.Member.Where(p => p.password == securePassword.password && p.userName == memberModel.userName)
                .Select(p => new MemberModel
                {
                    m_no = p.m_no
                });

                var result = results.SingleOrDefault();
                return result;
            }

            return null;

        }

        public MemberModel GetOneMember(MemberModel memberModel)
        {
            var results = _dataBaseService.Member.Where(p => p.m_no == memberModel.m_no)
            .Select(p => new MemberModel
            {
                m_no = p.m_no,
                password = p.password,
                userName = p.userName,
                monthlyIncome = p.monthlyIncome,
                savings = p.savings,
                fixedCost = p.fixedCost,
                currencyTypeAmountLimit = CurrencyType.CastIntegerToCurrencyType(p.amountLimit._amountLimit)
            });

            var result = results.SingleOrDefault();
            return result;
        }
    }
}
