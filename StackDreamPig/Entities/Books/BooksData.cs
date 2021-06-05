using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Valueobject.Books;

namespace Entities.Books
{
    public class BooksData
    {
        public BooksData()
        {

        }
        /// <summary>
        /// 帳簿登録EFオブジェクト
        /// </summary>
        /// <param name="m_no"></param>
        /// <param name="amountUsed"></param>
        /// <param name="intime"></param>
        /// <param name="registDate"></param>

        public BooksData(int id, string m_no, int amountUsed, DateTime intime, DateTime registDate)
        {
            if (string.IsNullOrEmpty(m_no)) throw new ArgumentNullException(null, "セッションが切れました。再度ログインしてください。");
            if (intime == default(DateTime)) throw new ArgumentNullException(null, "登録日が不正です。入力しなおしてください。");
            if (registDate == null) throw new ArgumentNullException(null, "システムエラー。再度入力しなおしてください。");

            this.id = id;
            this.m_no = m_no;
            this.amountUsed = amountUsed;
            this.intime = intime;
            this.registDate = registDate;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; }

        public string m_no { get; }

        public DateTime registDate { get; }

        public int amountUsed { get; }

        public DateTime intime { get; }

        public DateTime utime { get; }
    }
}
