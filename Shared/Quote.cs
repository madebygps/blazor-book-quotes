using System.Collections.Generic;

namespace BlazorApp.Shared
{
    public class Quote
    {
        public string Id { get; set; }
        public Author Author { get; set; }
        public string Content { get; set; }
        public Book Book { get; set; }
        public List<string>Tags { get; set; }

    }
}