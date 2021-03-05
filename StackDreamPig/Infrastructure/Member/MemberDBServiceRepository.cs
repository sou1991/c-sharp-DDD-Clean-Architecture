using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Member
{
    public class MemberDBServiceRepository : IMemberRepository
    {
        private IDataBaseService _dataBaseService;

        public MemberDBServiceRepository(IDataBaseService dataBaseService)
        {
            _dataBaseService = dataBaseService;
        }
        /****************************
        * TO DO : 検索条件をポリモーフィズム的に解決したい
        *****************************/
        public MemberEntity FindSingle(string target)
        {
            var user = _dataBaseService.Member
                      .Where(p => p.m_no == target);
                      
            return user.First();
        }

        public MemberEntity Find(string targetName, string targetPass)
        {
            var results = _dataBaseService.Member
                          .Where(p => p.memberValueObject.password == targetPass && p.memberValueObject.userName == targetName)
                          .First();

            return results;
        }

        public MemberEntity GetSecurePassword(string target)
        {
            var securePassword = _dataBaseService.Member
                                 .Where(p => p.memberValueObject.userName == target);

            return securePassword.FirstOrDefault();
        }

        public void Create(MemberEntity memberEntity) 
        {
            _dataBaseService.Member.Add(memberEntity);
        }

        public void Save() 
        {
            _dataBaseService.Save();
        }
    }
}
