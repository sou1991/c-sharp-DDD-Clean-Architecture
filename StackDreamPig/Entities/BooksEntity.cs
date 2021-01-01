using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
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
        public BooksEntity(int m_no, int amountUsed, DateTime intime, RegistDateValueObject registDate)
        {
            this.m_no = m_no;
            this.amountUsed = amountUsed;
            this.intime = intime;
            this.registDate = registDate;
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; }
    
        public int m_no { get; }

        public RegistDateValueObject registDate { get; set; }

        public int amountUsed { get; set; }

        public DateTime intime { get; set; }

        public DateTime utime { get; set; }


    }
}
