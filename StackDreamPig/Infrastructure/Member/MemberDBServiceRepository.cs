using Entities;
using Entities.Member;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Valueobject.Books;
using Valueobject.Member;

namespace Infrastructure.Member
{
    public class MemberDBServiceRepository : IMemberRepository
    {
        private IDataBaseService _dataBaseService;

        public MemberDBServiceRepository(IDataBaseService dataBaseService)
        {
            _dataBaseService = dataBaseService;
        }

        public MemberEntity GetUserWithSession(string target)
        {
            var user = _dataBaseService.Member
                      .Where(p => p.m_no == target).AsNoTracking().First();

            return Transfer(user, target);
        }

        public MemberEntity GetUser(string targetName, string targetPass)
        {
            var user = _dataBaseService.Member
                          .Where(p => p.password == targetPass && p.userName == targetName)
                          .First();

            return Transfer(user, user.m_no);
        }

        public MemberEntity GetUserWithUserName(string target)
        {
            var user = _dataBaseService.Member
                                 .Where(p => p.userName == target).FirstOrDefault();

            if (user == null) return null;

            return Transfer(user);
        }

        public void Create(MemberEntity memberEntity) 
        {
            var dtoModel = ToModel(memberEntity);
            _dataBaseService.Member.Add(dtoModel);
        }

        public void Save() 
        {
            _dataBaseService.Save();
        }

        public void Update(MemberEntity entity, string m_no)
        {
            var dtoModel = ToModel(entity, m_no);

            _dataBaseService.Member.Update(dtoModel).State = EntityState.Modified;
        }

        public MemberEntity Transfer(MemberData dtoModel)
        {
           var amountLimit = new AmountLimitValueObject(dtoModel.amountLimit);

           var amountValue = new AmountValueObject(dtoModel.monthlyIncome, dtoModel.savings, dtoModel.fixedCost);

           var memberValueObject = new MemberValueObject(dtoModel.userName, dtoModel.password, dtoModel.saltPassword);

            return new MemberEntity(memberValueObject, amountValue, amountLimit, DateTime.Now);
        }

        public MemberEntity Transfer(MemberData dtoModel, string m_no)
        {
            var amountLimit = new AmountLimitValueObject(dtoModel.amountLimit);

            var amountValue = new AmountValueObject(dtoModel.monthlyIncome, dtoModel.savings, dtoModel.fixedCost);

            var memberValueObject = new MemberValueObject(dtoModel.userName, dtoModel.password, dtoModel.saltPassword);

            return new MemberEntity(m_no, memberValueObject, amountValue, amountLimit, DateTime.Now);
        }

        public MemberData ToModel(MemberEntity entity, string m_no)
        {
            var dtoModel = new MemberDataModelBuilder();

            entity.Notice(dtoModel);

            var datamodel = dtoModel.Build();

            var memberValueObj = new MemberValueObject(datamodel.userName, datamodel.password, datamodel.saltPassword);
            var amount = new AmountValueObject(datamodel.monthlyIncome, datamodel.savings, datamodel.fixedCost);
            var amountLimit = new AmountLimitValueObject(datamodel.amountLimit);

            return new MemberData(m_no, memberValueObj, amount, amountLimit, entity.utime);
        }

        public MemberData ToModel(MemberEntity entity)
        {
            var dtoModel = new MemberDataModelBuilder();

            entity.Notice(dtoModel);

            var datamodel = dtoModel.Build();

            var memberValueObj = new MemberValueObject(datamodel.userName, datamodel.password, datamodel.saltPassword);
            var amount = new AmountValueObject(datamodel.monthlyIncome, datamodel.savings, datamodel.fixedCost);
            var amountLimit = new AmountLimitValueObject(datamodel.amountLimit);

            return new MemberData(memberValueObj, amount, amountLimit, entity.intime);
        }
    }
}
