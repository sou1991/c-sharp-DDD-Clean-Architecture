using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Valueobject.Member;


namespace Entities
{
    public class MemberEntity : IEntity
    {
        public MemberEntity()
        {

        }
        /// <summary>
        /// 会員情報更新オブジェクト
        /// </summary>
        /// <param name="m_no"></param>
        /// <param name="memberValueObject"></param>
        /// <param name="amountValueObject"></param>
        /// <param name="amountLimitValueObject"></param>
        /// <param name="utime"></param>
        public MemberEntity(
            int m_no,
            MemberValueObject memberValueObject,
            AmountValueObject amountValueObject,
            AmountLimitValueObject amountLimitValueObject,
            DateTime utime
        )
        {
            this.m_no = m_no;
            this.memberValueObject = memberValueObject;
            this.amountValueObject = amountValueObject;
            this.amountLimitValueObject = amountLimitValueObject;
            this.utime = utime;
        }

        /// <summary>
        /// 会員情報登録オブジェクト
        /// </summary>
        /// <param name="memberValueObject"></param>
        /// <param name="amountValueObject"></param>
        /// <param name="amountLimitValueObject"></param>
        /// <param name="intime"></param>
        public MemberEntity(
            MemberValueObject memberValueObject, 
            AmountValueObject amountValueObject,
            AmountLimitValueObject amountLimitValueObject,
            DateTime intime
        )
        {
            this.memberValueObject = memberValueObject;
            this.amountValueObject = amountValueObject;
            this.amountLimitValueObject = amountLimitValueObject;
            this.intime = intime;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int m_no { get; }

        //To Do　値オブジェクトを不変化にしたい。O/Rマッパーにsetterを強要される
        public MemberValueObject memberValueObject { get; set; }
        //To Do　値オブジェクトを不変化にしたい。O/Rマッパーにsetterを強要される
        public AmountValueObject amountValueObject { get; set; }
        //To Do　値オブジェクトを不変化にしたい。O/Rマッパーにsetterを強要される
        public AmountLimitValueObject amountLimitValueObject { get; set; }

        public DateTime intime { get; }
        //To Do　値オブジェクトを不変化にしたい。O/Rマッパーにsetterを強要される
        public DateTime utime { get; set; }
    }
}
