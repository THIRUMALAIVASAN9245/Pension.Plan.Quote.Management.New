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

    public class GetQuoteByIdHandlerTest
    {
        private GetQuoteByIdRequest request;

        private GetQuoteByIdHandler underTest;

        private Mock<IRepository> repository;

        [SetUp]
        public void Setup()
        {
            // Arrange   
            var quote = new Quote
            {
                Id = new Guid("6877D1A3-4BC7-4D0B-B802-08D844D525AC"),
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
            };
            repository = new Mock<IRepository>();
            repository.Setup(m => m.Get<Quote>(new Guid("6877D1A3-4BC7-4D0B-B802-08D844D525AC"))).Returns(quote);

            underTest = new GetQuoteByIdHandler(repository.Object);
            request = new GetQuoteByIdRequest(new Guid("6877D1A3-4BC7-4D0B-B802-08D844D525AC"));
        }

        [Test]
        public async Task HandleSuccessAsync()
        {
            // Act
            CancellationToken cancellationToken;
            var result = await underTest.Handle(request, cancellationToken);

            // Assert  
            Assert.NotNull(result);
            Assert.AreEqual("ClientName 1", result.ClientName);
            Assert.AreEqual(new DateTime(1992, 09, 09), result.DateofBirth);
            Assert.AreEqual(1, result.ClientSex);
            Assert.AreEqual("Test@test.com", result.Email);
            Assert.AreEqual("1234567880", result.MobileNumber);
            Assert.AreEqual(new DateTime(2020, 08, 08), result.QuoteDate);
            Assert.AreEqual(62, result.RetirementAge);
            Assert.AreEqual(100000, result.InvestmentAmount);
            Assert.AreEqual(1500, result.MaturityAmount);
            Assert.AreEqual(1, result.PensionPlan);
        }

        [Test]
        public async Task HandleNullQuoteSuccessAsync()
        {
            // Act
            repository.Setup(m => m.Get<Quote>(new Guid("6877D1A3-4BC7-4D0B-B802-08D844D525AC"))).Returns(await Task.FromResult<Quote>(null));
            underTest = new GetQuoteByIdHandler(repository.Object);
            request = new GetQuoteByIdRequest(new Guid("6877D1A3-4BC7-4D0B-B802-08D844D525AC"));
            CancellationToken cancellationToken;
            var result = await underTest.Handle(request, cancellationToken);

            // Assert  
            Assert.Null(result);
        }
    }
}