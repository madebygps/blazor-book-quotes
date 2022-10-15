using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Extensions.Configuration;
using BlazorApp.Shared;

namespace ApiIsolated
{
    public class QuotesRepository
        {

        private readonly Container _quotesCollection;
        private readonly Container _booksCollection;
        private readonly Container _authorsCollection;

        public QuotesRepository(CosmosClient client, IConfiguration configuration)
        {
            var database = client.GetDatabase(configuration["AZURE_COSMOS_DATABASE_NAME"]);
            _quotesCollection = database.GetContainer("quote");
            _booksCollection = database.GetContainer("book");
            _authorsCollection = database.GetContainer("author");
        }

        public async Task<IEnumerable<Quote>> GetQuotesAsync()
        {
            return await ToListAsync(
                _quotesCollection.GetItemLinqQueryable<Quote>());
        }

        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            return await ToListAsync(
                _booksCollection.GetItemLinqQueryable<Book>());
        }

        public async Task<IEnumerable<Author>> GetAuthorsAsync()
        {
            return await ToListAsync(
                _authorsCollection.GetItemLinqQueryable<Author>());
        }

        public async Task<Quote?> GetQuoteAsync(string quoteId)
        {
            var response = await _quotesCollection.ReadItemAsync<Quote>(quoteId, new PartitionKey(quoteId));
            return response?.Resource;
        }
        public async Task<Book?> GetBookAsync(string bookId)
        {
            var response = await _booksCollection.ReadItemAsync<Book>(bookId, new PartitionKey(bookId));
            return response?.Resource;
        }
        public async Task<Author?> GetAuthorAsync(string authorId)
        {
            var response = await _authorsCollection.ReadItemAsync<Author>(authorId, new PartitionKey(authorId));
            return response?.Resource;
        }

        public async Task DeleteQuoteAsync(string quoteId)
        {
            await _quotesCollection.DeleteItemAsync<Quote>(quoteId, new PartitionKey(quoteId));
        }

        public async Task AddQuoteAsync(Quote quote)
        {
            quote.Id = Guid.NewGuid().ToString("N");
            await _quotesCollection.UpsertItemAsync(quote, new PartitionKey(quote.Id));
        }

        public async Task UpdateQuote(Quote existingQuote)
        {
            await _quotesCollection.ReplaceItemAsync(existingQuote, existingQuote.Id, new PartitionKey(existingQuote.Id));
        }

        private async Task<List<T>> ToListAsync<T>(IQueryable<T> queryable)
        {
            

            using FeedIterator<T> iterator = queryable.ToFeedIterator();
            var items = new List<T>();

            while (iterator.HasMoreResults)
            {
                foreach (var item in await iterator.ReadNextAsync())
                {
                    items.Add(item);
                }
            }

            return items;
        }



    }
}
