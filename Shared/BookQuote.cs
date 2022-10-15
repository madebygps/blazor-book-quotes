using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorApp.Shared
{
    public class BookQuote
    {

        public BookQuote(Author author, Quote quote, Book book)
        {
            this.Author = author;
            this.Quote = quote;
            this.Book = book;
        }
        public Quote Quote { get; set; }
        public Author Author { get; set; }
        public Book Book { get; set; }

    }
}
