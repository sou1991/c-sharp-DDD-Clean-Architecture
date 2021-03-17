using Application.Member.Model;
using Infrastructure;
using System.Linq;

namespace Application.Member.DomainService
{
    public class MemberDomainService
    {
        private IDataBaseService _dataBaseService;

        public MemberDomainService(IDataBaseService dataBaseService)
        {
            _dataBaseService = dataBaseService;
        }
        public bool HasRegistMember(IMemberDTO memberModel)
        {
            var member = _dataBaseService.Member
            .Where(p => p.memberValueObject.userName == memberModel.userName);

            return member.Any() ? true : false;
        }
    }
}
