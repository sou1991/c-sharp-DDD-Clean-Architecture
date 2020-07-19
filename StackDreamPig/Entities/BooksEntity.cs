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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        
        public int m_no { get; set; }

        public RegistDateValueObject registDate { get; set; }

        public int amountUsed { get; set; }

        public DateTime intime { get; set; }

        public DateTime utime { get; set; }


    }
}
