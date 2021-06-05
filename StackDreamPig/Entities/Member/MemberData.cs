using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Valueobject.Member;

namespace Entities.Member
{
    public class MemberData
    {
        public MemberData()
        {

        }
        /// <summary>
        /// 会員情報更新Efオブジェクト
        /// </summary>
        /// <param name="m_no"></param>
        /// <param name="memberValueObject"></param>
        /// <param name="amountValueObject"></param>
        /// <param name="amountLimitValueObject"></param>
        /// <param name="utime"></param>
        public MemberData(
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
            this.userName = memberValueObject.userName;
            this.password = memberValueObject.password;
            this.saltPassword = memberValueObject.saltPassword;

            this.monthlyIncome = amountValueObject.monthlyIncome;
            this.savings = amountValueObject.savings;
            this.fixedCost = amountValueObject.fixedCost;
            this.amountLimit = amountLimitValueObject._amountLimit;
            this.utime = utime;
        }

        /// <summary>
        /// 会員情報登録オブジェクト
        /// </summary>
        /// <param name="memberValueObject"></param>
        /// <param name="amountValueObject"></param>
        /// <param name="amountLimitValueObject"></param>
        /// <param name="intime"></param>
        public MemberData(
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

            this.userName = memberValueObject.userName;
            this.password = memberValueObject.password;
            this.saltPassword = memberValueObject.saltPassword;
            this.monthlyIncome = amountValueObject.monthlyIncome;
            this.savings = amountValueObject.savings;
            this.fixedCost = amountValueObject.fixedCost;
            this.amountLimit = amountLimitValueObject._amountLimit;
            this.intime = intime;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string m_no { get; }

        public string userName { get; }

        public string password { get; }

        public string saltPassword { get; }

        public string monthlyIncome { get; }

        public string savings { get; }

        public string fixedCost { get; }

        public int amountLimit { get; }

        public DateTime intime { get; }

        public DateTime utime { get; set; }
    }
}
