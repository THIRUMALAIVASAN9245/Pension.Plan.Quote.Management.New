namespace Quote.Service.API.Service.Handlers.QueryHandlers
{
    using MediatR;
    using Quote.Service.API.Data.Repository;
    using Quote.Service.API.Domain.Entities;
    using Quote.Service.API.Service.RequestHandlers.QueryHandlers;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// GetQuoteByIdHandler class
    /// </summary>
    public class GetQuoteByIdHandler : IRequestHandler<GetQuoteByIdRequest, Quote>
    {
        private readonly IRepository repository;

        /// <summary>
        /// GetQuoteByIdHandler constructor
        /// </summary>
        /// <param name="repository">IRepository</param>
        public GetQuoteByIdHandler(IRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Handle method to get quote
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Quote> Handle(GetQuoteByIdRequest request, CancellationToken cancellationToken)
        {
            var quote = repository.Get<Quote>(request.QuoteId);

            if (quote == null)
            {
                return await Task.FromResult<Quote>(null);
            }

            return await Task.FromResult(quote);
        }
    }
}
