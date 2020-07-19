using stackDreamPig.Models.Book.Query;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Books.Commands
{
    public interface IBooksRegistCommand
    {
        void Execute(BooksModel booksModel);
    }
}
