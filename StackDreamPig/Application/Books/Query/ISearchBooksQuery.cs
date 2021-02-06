using Application.Books.Model;
using stackDreamPig.Models.Book;
using stackDreamPig.Models.Book.Query;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Books.Query
{
    public interface ISearchBooksQuery
    {
        IEnumerable<IBooksDTO> Execute(IBooksDTO booksModel);
    }
}
