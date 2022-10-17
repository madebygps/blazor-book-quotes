# Data modeling

Operations 

[C1] Create/edit a quote
[Q1] Retrieve a quote
[C2] Create/edit a author
[Q2] Retrieve a author
[Q3] List an authors quotes
[C3] Create a book
[Q4] List a book's quotes

``` json
{
    "id": "<quote-id>",
    "type": "quote",
    "quoteId": "<quote-id>",
    "authorId": "<quote-author-id>",
    "authorName": "<quote-author-username>",
    "content": "<quote-content>",
    "bookId": <quote-book-id>,
    "bookName": <quote-book-name>
}
{
    "id": "<book-id>",
    "type": "book",
    "authorId": "<book-author-id>",
    "authorName": "<book-author-username>",
    "title": "<book-title>"
}

// author collection

{
    "id": "<author-id>",
    "name": "<author name>"
}

```