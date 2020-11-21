using stackDreamPig.Models.Book;
using stackDreamPig.Models.Book.Query;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Books.Query
{
    public interface ISearchBooksQuery
    {
        IEnumerable<BooksModel> Execute(BooksModel booksModel);
    }
}
