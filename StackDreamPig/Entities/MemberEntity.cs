using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Valueobject.Member;


namespace Entities
{
    public class MemberEntity : IEntity
    {


        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int m_no { get; set; }

        public string userName { get; set; }

        public string password { get; set; }

        public string monthlyIncome { get; set; }

        public string savings { get; set; }

        public string fixedCost { get; set; }

        public DateTime intime { get; set; }

        public DateTime utime { get; set; }


        public AmountLimitValueObject amountLimit { get; set; }

        public string saltUserName { get; set; }

        public string saltPassword { get; set; }

    }
}
