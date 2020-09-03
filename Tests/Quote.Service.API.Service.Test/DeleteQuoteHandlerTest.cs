namespace Quote.Service.API.Service.Test
{
    using Moq;
    using NUnit.Framework;
    using Quote.Service.API.Data.Repository;
    using Quote.Service.API.Domain.Entities;
    using Quote.Service.API.Service.Handlers.CommandHandlers;
    using Quote.Service.API.Service.RequestHandlers.CommandHandlers;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class DeleteQuoteHandlerTest
    {
        private DeleteQuoteRequest request;

        private DeleteQuoteHandler underTest;

        private Mock<IRepository> repository;

        [SetUp]
        public void Setup()
        {
            // Arrange 
            var quoteModel = new Quote
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
            };
            repository = new Mock<IRepository>();
            repository.Setup(m => m.Save<Quote>(It.IsAny<Quote>())).Returns(quoteModel);
            underTest = new DeleteQuoteHandler(repository.Object);
            request = new DeleteQuoteRequest(quoteModel);
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
    }
}