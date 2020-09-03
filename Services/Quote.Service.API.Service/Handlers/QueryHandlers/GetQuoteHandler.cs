namespace Quote.Service.API.Service.Handlers.QueryHandlers
{
    using MediatR;
    using Quote.Service.API.Data.Repository;
    using Quote.Service.API.Domain.Entities;
    using Quote.Service.API.Service.RequestHandlers.QueryHandlers;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// GetQuoteHandler class
    /// </summary>
    public class GetQuoteHandler : IRequestHandler<GetQuoteRequest, List<Quote>>
    {
        private readonly IRepository repository;

        /// <summary>
        /// GetQuoteHandler constructor
        /// </summary>
        /// <param name="repository">IRepository</param>
        public GetQuoteHandler(IRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Handle method to get all quote
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<Quote>> Handle(GetQuoteRequest request, CancellationToken cancellationToken)
        {
            var quote = repository.Query<Quote>()
                .OrderBy(x => x.QuoteDate)
                .ToList();

            return await Task.FromResult(quote);
        }
    }
}
