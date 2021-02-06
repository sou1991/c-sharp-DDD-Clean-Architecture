using Infrastructure;
using Application.Member.Model;
using System.Linq;
using Entities;
using Valueobject.Member;
using System;
using Common.Member;
using Factory;

namespace Application.Member.Commands
{
    public class CreateMemberCommand : SecureService, ICreateMemberCommand
    {
        private IDataBaseService _dataBaseService;

        public CreateMemberCommand(IDataBaseService dataBaseService)
        {
            _dataBaseService = dataBaseService;
        }

        public void Execute(IMemberDTO memberModel)
        {
            
            var (securePassword, passwordSalt) = HashToValue(memberModel.password);
            var serializePasswordSalt = ToSerialize(passwordSalt);

            //To Do　値オブジェクトを不変化にしたい。O/Rマッパーにsetterを強要される
            var memberValueObject = SdpFactory.ValueObjectFactory().CreateMemberValueObject(memberModel.userName, securePassword, serializePasswordSalt);

            var amountValueObject = SdpFactory.ValueObjectFactory().CreateAmountValueObject(memberModel.monthlyIncome, memberModel.savings, memberModel.fixedCost);
            var amountLimitValueObject = SdpFactory.ValueObjectFactory().CreateAmountLimitValueObject(memberModel.amountLimit);

            var memberEntity = SdpFactory.EntityFactory().CreateMemberEntity(memberValueObject, amountValueObject, amountLimitValueObject, DateTime.Now);

            _dataBaseService.Member.Add(memberEntity);

            _dataBaseService.Save();
            
        }
    }
}
