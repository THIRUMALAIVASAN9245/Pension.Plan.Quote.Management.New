namespace Quote.Service.API.Service.Handlers.CommandHandlers
{
    using MediatR;
    using Quote.Service.API.Data.Repository;
    using Quote.Service.API.Domain.Entities;
    using Quote.Service.API.Service.RequestHandlers.CommandHandlers;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// CreateQuoteHandler class
    /// </summary>
    public class CreateQuoteHandler : IRequestHandler<CreateQuoteRequest, Quote>
    {
        private readonly IRepository repository;

        /// <summary>
        /// CreateQuoteHandler constructor
        /// </summary>
        /// <param name="repository">IRepository</param>
        public CreateQuoteHandler(IRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Handle method to create new quote
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Quote> Handle(CreateQuoteRequest request, CancellationToken cancellationToken)
        {
            repository.Save(request.Quote);

            return await Task.FromResult(request.Quote);
        }
    }
}