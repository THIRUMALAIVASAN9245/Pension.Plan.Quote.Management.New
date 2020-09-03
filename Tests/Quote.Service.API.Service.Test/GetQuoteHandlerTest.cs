namespace Quote.Service.API.Service.Test
{
    using Moq;
    using NUnit.Framework;
    using Quote.Service.API.Data.Repository;
    using Quote.Service.API.Domain.Entities;
    using Quote.Service.API.Service.Handlers.QueryHandlers;
    using Quote.Service.API.Service.RequestHandlers.QueryHandlers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetQuoteHandlerTest
    {
        private GetQuoteRequest request;

        private GetQuoteHandler underTest;

        private Mock<IRepository> repository;

        [SetUp]
        public void Setup()
        {
            // Arrange   
            var quoteList = GetMockQuoteList().AsQueryable();
            repository = new Mock<IRepository>();
            repository.Setup(m => m.Query<Quote>()).Returns(quoteList);

            underTest = new GetQuoteHandler(repository.Object);
            request = new GetQuoteRequest();
        }

        [Test]
        public async Task HandleSuccessAsync()
        {
            // Act
            CancellationToken cancellationToken;
            var result = await underTest.Handle(request, cancellationToken);

            // Assert  
            Assert.NotNull(result);
            Assert.AreEqual(2, result.Count);
        }

        private static List<Quote> GetMockQuoteList()
        {
            var quoteList = new List<Quote>
            {
               new Quote
               {
                    Id = Guid.NewGuid(),
                    ClientName = "ClientName 1",
                    DateofBirth = new DateTime(1992, 09, 09),
                    ClientSex = 1,
                    Email = "Test@test.com",
                    MobileNumber = "1234567880",
                    QuoteDate = new DateTime(2020, 08, 08),
                    RetirementAge = 62,
                    InvestmentAmount = 100000,
                    MaturityAmount = 1500,
                    PensionPlan = 1
               },
               new Quote
               {
                    Id = Guid.NewGuid(),
                    ClientName = "ClientName 2",
                    DateofBirth = new DateTime(1992, 09, 09),
                    ClientSex = 1,
                    Email = "Test@test.com",
                    MobileNumber = "1234567880",
                    QuoteDate = new DateTime(2020, 08, 08),
                    RetirementAge = 62,
                    InvestmentAmount = 100000,
                    MaturityAmount = 1500,
                    PensionPlan = 1
               }
            };

            return quoteList;
        }
    }
}