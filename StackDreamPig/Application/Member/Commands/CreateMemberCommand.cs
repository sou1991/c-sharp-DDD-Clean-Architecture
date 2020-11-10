using Infrastructure;
using Application.Member.Model;
using System.Linq;
using Entities;
using Valueobject.Member;
using System;
using Common.Member;

namespace Application.Member.Commands
{
    public class CreateMemberCommand : SecureService, ICreateMemberCommand
    {
        private IDataBaseService _dataBaseService;

        public CreateMemberCommand(IDataBaseService dataBaseService)
        {
            _dataBaseService = dataBaseService;
        }

        public void Execute(MemberModel memberModel)
        {
            var (securePassword, passwordSalt) = HashToValue(memberModel.password);
            var serializePasswordSalt = ToSerialize(passwordSalt);

            var _memberEntity = new MemberEntity {
                userName = memberModel.userName,
                password = securePassword,
                monthlyIncome = memberModel.monthlyIncome,
                savings = memberModel.savings,
                fixedCost = memberModel.fixedCost,
                amountLimit = new AmountLimitValueObject(memberModel.amountLimit),
                saltPassword = serializePasswordSalt,
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
            .Where(p => p.userName == memberModel.userName);

            if (member.Any())
            {
                return true;   
            }
            else
            {
                return false;
            }
        }
    }
}
