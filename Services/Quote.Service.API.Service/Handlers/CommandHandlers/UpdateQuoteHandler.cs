namespace Quote.Service.API.Service.Handlers.CommandHandlers
{
    using MediatR;
    using Quote.Service.API.Data.Repository;
    using Quote.Service.API.Domain.Entities;
    using Quote.Service.API.Service.RequestHandlers.CommandHandlers;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// UpdateQuoteHandler class
    /// </summary>
    public class UpdateQuoteHandler : IRequestHandler<UpdateQuoteRequest, Quote>
    {
        private readonly IRepository repository;

        /// <summary>
        /// UpdateQuoteHandler constructor
        /// </summary>
        /// <param name="repository">IRepository</param>
        public UpdateQuoteHandler(IRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Handle method to update quote
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Quote> Handle(UpdateQuoteRequest request, CancellationToken cancellationToken)
        {
            repository.Update(request.Quote);

            return await Task.FromResult(request.Quote);
        }
    }
}