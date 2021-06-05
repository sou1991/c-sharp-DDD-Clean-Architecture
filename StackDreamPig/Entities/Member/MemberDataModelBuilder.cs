using System;
using System.Collections.Generic;
using System.Text;
using Valueobject.Member;

namespace Entities.Member
{
    public class MemberDataModelBuilder
    {
        private string m_no;

        private MemberValueObject memberValueObject;

        private AmountValueObject amountValueObject;

        private AmountLimitValueObject amountLimitValueObject;

        public DateTime intime;

        public DateTime utime;

        public void SetMemberNo(string value)
        {
            m_no = value;
        }

        public void SetAmountValueObject(AmountValueObject obj)
        {
            amountValueObject = obj;
        }
        public void SetAmountLimitValueObject(AmountLimitValueObject obj)
        {
            amountLimitValueObject = obj;
        }
        public void SetMemberValueObject(MemberValueObject obj)
        {
            memberValueObject = obj;
        }

        public void SetIntime(DateTime time)
        {
            intime = time;
        }

        public void SetUtime(DateTime time)
        {
            utime = time;
        }
        public MemberData Build() 
        {
            if (string.IsNullOrEmpty(m_no))
            {
                return new MemberData(memberValueObject, amountValueObject, amountLimitValueObject, intime);
            }
            else 
            {
                return new MemberData(m_no,memberValueObject, amountValueObject, amountLimitValueObject, utime);
            }
        }
    }
}
