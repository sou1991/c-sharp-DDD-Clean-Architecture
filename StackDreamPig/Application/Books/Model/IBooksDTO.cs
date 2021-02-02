using stackDreamPig.Models.Book.Query;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Books.Model
{
    public interface IBooksDTO
    {
        public string m_no { get; }

        public int amountUsed { get; }

        public string currencyTypeAmountLimit { get; }

        public string year { get; }

        public string month { get; }

        public string day { get; set; }
        public IEnumerable<IBooksDTO> booksList { get;  }

        public DateTime DispRegistDate { get;  }

        public DateTime registrationDateSearch { get;  }

        public string monthlyTotalAmountUsed { get; }

        public string currencyTypeAmountUsed { get; }

    }
}
