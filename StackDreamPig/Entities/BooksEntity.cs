using System;
using System.ComponentModel.DataAnnotations.Schema;
using Valueobject.Books;

namespace Entities
{
    public class BooksEntity : IEntity
    {
        public BooksEntity()
        {

        }
        /// <summary>
        /// 帳簿登録オブジェクト
        /// </summary>
        /// <param name="m_no"></param>
        /// <param name="amountUsed"></param>
        /// <param name="intime"></param>
        /// <param name="registDate"></param>
        public BooksEntity(string m_no, int amountUsed, DateTime intime, RegistDateValueObject registDate)
        {
            if (string.IsNullOrEmpty(m_no)) throw new ArgumentNullException(null,"セッションが切れました。再度ログインしてください。");
            if (intime == default(DateTime)) throw new ArgumentNullException(null, "登録日が不正です。入力しなおしてください。");
            if (registDate == null) throw new ArgumentNullException(null, "システムエラー。再度入力しなおしてください。");

            this.m_no = m_no;
            this.amountUsed = amountUsed;
            this.intime = intime;
            this.registDate = registDate;
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; }
    
        public string m_no { get; }

        public RegistDateValueObject registDate { get; set; }

        public int amountUsed { get; set; }

        public DateTime intime { get; set; }

        public DateTime utime { get; set; }


    }
}
