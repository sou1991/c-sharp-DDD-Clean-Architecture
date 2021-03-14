using Entities;
using System;
using Valueobject.Books;
using Valueobject.Member;

namespace Factory
{
    public class EntityFactory
    {
        /// <summary>
        /// 会員情報更新オブジェクト
        /// </summary>
        /// <param name="m_no"></param>
        /// <param name="memberValueObject"></param>
        /// <param name="amountValueObject"></param>
        /// <param name="amountLimitValueObject"></param>
        /// <param name="utime"></param>
        /// <returns></returns>
        public MemberEntity CreateMemberEntity(
            string m_no,
            MemberValueObject memberValueObject,
            AmountValueObject amountValueObject,
            AmountLimitValueObject amountLimitValueObject,
            DateTime utime
        )
            => new MemberEntity(m_no, memberValueObject, amountValueObject, amountLimitValueObject, utime);

        /// <summary>
        /// 会員情報登録オブジェクト
        /// </summary>
        /// <param name="memberValueObject"></param>
        /// <param name="amountValueObject"></param>
        /// <param name="amountLimitValueObject"></param>
        /// <param name="intime"></param>
        /// <returns></returns>
        public MemberEntity CreateMemberEntity(
            MemberValueObject memberValueObject,
            AmountValueObject amountValueObject,
            AmountLimitValueObject amountLimitValueObject,
            DateTime intime
        )
            => new MemberEntity(memberValueObject, amountValueObject, amountLimitValueObject, intime);

        /// <summary>
        /// 帳簿登録オブジェクト
        /// </summary>
        /// <param name="m_no"></param>
        /// <param name="amountUsed"></param>
        /// <param name="intime"></param>
        /// <param name="registDate"></param>
        /// <returns></returns>
        public BooksEntity CreateBooksEntity(string m_no, int amountUsed, DateTime intime, RegistDateValueObject registDate)
            => new BooksEntity(m_no, amountUsed, intime, registDate);
    }
}
