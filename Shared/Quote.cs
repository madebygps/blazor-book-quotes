using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BlazorApp.Shared
{
    public class Quote
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("authorId")]
        public string AuthorId { get; set; }

        [JsonPropertyName("body")]
        public string Body { get; set; }

        [JsonPropertyName("bookId")]
        public string BookId { get; set; }


    }
}
