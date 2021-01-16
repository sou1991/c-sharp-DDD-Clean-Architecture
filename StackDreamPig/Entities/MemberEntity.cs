using System;
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
            string m_no,
            MemberValueObject memberValueObject,
            AmountValueObject amountValueObject,
            AmountLimitValueObject amountLimitValueObject,
            DateTime utime
        )
        {
            if (string.IsNullOrEmpty(m_no)) throw new ArgumentNullException(null, "セッションが切れました。再度ログインしてください。");
            if (memberValueObject == null) throw new ArgumentNullException(null, "システムエラー。再度入力しなおしてください。");
            if (amountValueObject == null) throw new ArgumentNullException(null, "システムエラー。再度入力しなおしてください。");
            if (amountLimitValueObject == null) throw new ArgumentNullException(null, "システムエラー。再度入力しなおしてください。");
            if (utime == default(DateTime)) throw new ArgumentNullException(null, "更新日が不正です。入力しなおしてください。");

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
            if (memberValueObject == null) throw new ArgumentNullException(null, "システムエラー。再度入力しなおしてください。");
            if (amountValueObject == null) throw new ArgumentNullException(null, "システムエラー。再度入力しなおしてください。");
            if (amountLimitValueObject == null) throw new ArgumentNullException(null, "システムエラー。再度入力しなおしてください。");
            if (intime == default(DateTime)) throw new ArgumentNullException(null, "登録日が不正です。入力しなおしてください。");

            this.memberValueObject = memberValueObject;
            this.amountValueObject = amountValueObject;
            this.amountLimitValueObject = amountLimitValueObject;
            this.intime = intime;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string m_no { get; }

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
