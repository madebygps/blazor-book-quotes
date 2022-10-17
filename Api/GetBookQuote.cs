using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Text.Json;
using BlazorApp.Shared;

namespace ApiIsolated
{
    public class GetBookQuote
    {
        private readonly ILogger _logger;
        private readonly QuotesRepository _repository;

        public GetBookQuote(ILoggerFactory loggerFactory, QuotesRepository repository)
        {
            _logger = loggerFactory.CreateLogger<GetBookQuote>();
            _repository = repository;
        }

        [Function("GetQuotes")]
        public async Task<HttpResponseData> GetQuotes(
            [HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
          {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            var quotes = await _repository.GetQuotesAsync();
            response.WriteString(JsonSerializer.Serialize(quotes));
            return response;

        }

        [Function("GetQuote")]
        public async Task<HttpResponseData> GetQuote(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "GetQuote/{quoteId}")] HttpRequestData req, string quoteId)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            var quote = await _repository.GetQuoteAsync(quoteId);
            response.WriteString(JsonSerializer.Serialize(quote));
            return response;

        }

        [Function("GetBooks")]
        public async Task<HttpResponseData> GetBooks(
            [HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            var books = await _repository.GetBooksAsync();
            
            response.WriteString(JsonSerializer.Serialize(books));
            return response;

        }
        
        [Function("GetAuthors")]
        public async Task<HttpResponseData> GetAuthors(
            [HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            var quotes = await _repository.GetAuthorsAsync();
            response.WriteString(JsonSerializer.Serialize(quotes));
            return response;

        }
    }
}
