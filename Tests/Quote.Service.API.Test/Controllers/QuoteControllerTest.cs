namespace Quote.Service.API.Test.Controllers
{
    using AutoMapper;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using NUnit.Framework;
    using Quote.Service.API.Domain.Entities;
    using Quote.Service.API.Models.Enum;
    using Quote.Service.API.Models.ResponseModels;
    using Quote.Service.API.Models.ResquestModels;
    using Quote.Service.API.Service.RequestHandlers.CommandHandlers;
    using Quote.Service.API.Service.RequestHandlers.QueryHandlers;
    using Quote.Servive.API.Controllers;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class QuoteControllerTest
    {
        private Mock<IMediator> mediatR;

        private QuoteController controller;

        private Mapper mapper;

        [SetUp]
        public void Setup()
        {
            // Arrange
            mediatR = new Mock<IMediator>();
            var config = new MapperConfiguration(m =>
            {
                m.CreateMap<CreateQuoteModel, Quote>();
                m.CreateMap<UpdateQuoteModel, Quote>();
                m.CreateMap<Quote, ResponseQuoteModel>();
            });
            mapper = new Mapper(config);
            var quote = new Quote
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
            var quoteList = new List<Quote> { quote };
            mediatR.Setup(m => m.Send(It.IsAny<CreateQuoteRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(quote));
            mediatR.Setup(m => m.Send(It.IsAny<UpdateQuoteRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(quote));
            mediatR.Setup(m => m.Send(It.IsAny<GetQuoteByIdRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(quote));
            mediatR.Setup(m => m.Send(It.IsAny<GetQuoteRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(quoteList));
            controller = new QuoteController(mapper, mediatR.Object);
        }

        [Test]
        public async Task PostAsync()
        {
            // Act
            var createQuoteModel = new CreateQuoteModel
            {
                ClientName = "ClientName 1",
                DateofBirth = new DateTime(1992, 09, 09),
                ClientSex = ClientSex.Male,
                Email = "Test@test.com",
                MobileNumber = "1234567880",
                RetirementAge = 62,
                InvestmentAmount = 100000,
                PensionPlan = PensionPlan.PensionSilver
            };
            var result = await controller.Post(createQuoteModel) as CreatedResult;

            // Assert      
            Assert.NotNull(result);
            Assert.AreEqual(201, result.StatusCode);
            var responseQuoteModel = result.Value as ResponseQuoteModel;
            Assert.NotNull(responseQuoteModel);
            Assert.AreEqual("ClientName 1", responseQuoteModel.ClientName);
        }

        [Test]
        public async Task PutAsync()
        {
            // Act
            var updateQuoteModel = new UpdateQuoteModel
            {
                Id = new Guid("6877D1A3-4BC7-4D0B-B802-08D844D525AC"),
                ClientName = "ClientName 1",
                DateofBirth = new DateTime(1992, 09, 09),
                ClientSex = ClientSex.Male,
                Email = "Test@test.com",
                MobileNumber = "1234567880",
                RetirementAge = 62,
                InvestmentAmount = 100000,
                PensionPlan = PensionPlan.PensionSilver
            };
            var result = await controller.Put(updateQuoteModel) as OkObjectResult;

            // Assert      
            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            var responseQuoteModel = result.Value as ResponseQuoteModel;
            Assert.NotNull(responseQuoteModel);
            Assert.AreEqual("ClientName 1", responseQuoteModel.ClientName);
        }

        [Test]
        public async Task DeleteAsync()
        {
            // Act
            var quoteId = new Guid("6877D1A3-4BC7-4D0B-B802-08D844D525AC");
            var result = await controller.Delete(quoteId) as OkObjectResult;

            // Assert      
            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            var responseQuoteModel = result.Value as ResponseQuoteModel;
            Assert.NotNull(responseQuoteModel);
            Assert.AreEqual("ClientName 1", responseQuoteModel.ClientName);
        }

        [Test]
        public async Task GetAsync()
        {
            // Act
            var result = await controller.Get() as OkObjectResult;

            // Assert      
            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            var responseQuoteModel = result.Value as List<ResponseQuoteModel>;
            Assert.NotNull(responseQuoteModel);
            Assert.AreEqual(1, responseQuoteModel.Count);
        }

        [Test]
        public async Task PostNullMediatorResponseAsync()
        {
            // Act
            var createQuoteModel = new CreateQuoteModel
            {
                ClientName = "ClientName 1",
                DateofBirth = new DateTime(1992, 09, 09),
                ClientSex = ClientSex.Male,
                Email = "Test@test.com",
                MobileNumber = "1234567880",
                RetirementAge = 62,
                InvestmentAmount = 100000,
                PensionPlan = PensionPlan.PensionSilver
            };
            mediatR.Setup(m => m.Send(It.IsAny<CreateQuoteRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<Quote>(null));
            controller = new QuoteController(mapper, mediatR.Object);
            var result = await controller.Post(createQuoteModel) as ObjectResult;

            // Assert      
            Assert.NotNull(result);
            Assert.AreEqual(409, result.StatusCode);
            Assert.AreEqual("Error Occurred While Creating a Quote", result.Value);
        }

        [Test]
        public async Task GetNullMediatorResponseAsync()
        {
            // Act
            mediatR.Setup(m => m.Send(It.IsAny<GetQuoteRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<List<Quote>>(null));
            controller = new QuoteController(mapper, mediatR.Object);
            var result = await controller.Get() as ObjectResult;

            // Assert      
            Assert.NotNull(result);
            Assert.AreEqual(404, result.StatusCode);
            Assert.AreEqual("No Quote found", result.Value);
        }

        [Test]
        public async Task DeleteNullMediatorResponseAsync()
        {
            // Act
            var quoteId = new Guid("6877d1a3-4bc7-4d0b-b802-08d844d525ac");
            mediatR.Setup(m => m.Send(It.IsAny<GetQuoteByIdRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<Quote>(null));
            controller = new QuoteController(mapper, mediatR.Object);
            var result = await controller.Delete(quoteId) as BadRequestObjectResult;

            // Assert      
            Assert.NotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.AreEqual("No quote found with the id 6877d1a3-4bc7-4d0b-b802-08d844d525ac", result.Value);
        }

        [Test]
        public async Task PutNullMediatorResponseAsync()
        {
            // Act
            var updateQuoteModel = new UpdateQuoteModel
            {
                Id = new Guid("6877D1A3-4BC7-4D0B-B802-08D844D525AC"),
                ClientName = "ClientName 1",
                DateofBirth = new DateTime(1992, 09, 09),
                ClientSex = ClientSex.Male,
                Email = "Test@test.com",
                MobileNumber = "1234567880",
                RetirementAge = 62,
                InvestmentAmount = 100000,
                PensionPlan = PensionPlan.PensionSilver
            };
            mediatR.Setup(m => m.Send(It.IsAny<GetQuoteByIdRequest>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<Quote>(null));
            controller = new QuoteController(mapper, mediatR.Object);
            var result = await controller.Put(updateQuoteModel) as BadRequestObjectResult;

            // Assert      
            Assert.NotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.AreEqual("No quote found with the id 6877d1a3-4bc7-4d0b-b802-08d844d525ac", result.Value);
        }

        [Test]
        public async Task GetThrowsExceptionAsync()
        {
            // Act
            mediatR.Setup(m => m.Send(It.IsAny<GetQuoteRequest>(), It.IsAny<CancellationToken>())).Throws(new InvalidOperationException());
            controller = new QuoteController(mapper, mediatR.Object);
            var result = await controller.Get() as ObjectResult;

            // Assert      
            Assert.NotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.AreEqual("Operation is not valid due to the current state of the object.", result.Value);
        }

        [Test]
        public async Task PostThrowsExceptionAsync()
        {
            // Act
            var createQuoteModel = new CreateQuoteModel
            {
                ClientName = "ClientName 1",
                DateofBirth = new DateTime(1992, 09, 09),
                ClientSex = ClientSex.Male,
                Email = "Test@test.com",
                MobileNumber = "1234567880",
                RetirementAge = 62,
                InvestmentAmount = 100000,
                PensionPlan = PensionPlan.PensionSilver
            };
            mediatR.Setup(m => m.Send(It.IsAny<CreateQuoteRequest>(), It.IsAny<CancellationToken>())).Throws(new InvalidOperationException());
            controller = new QuoteController(mapper, mediatR.Object);
            var result = await controller.Post(createQuoteModel) as ObjectResult;

            // Assert      
            Assert.NotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.AreEqual("Operation is not valid due to the current state of the object.", result.Value);
        }

        [Test]
        public async Task PutThrowsExceptionAsync()
        {
            // Act
            var updateQuoteModel = new UpdateQuoteModel
            {
                Id = new Guid("6877D1A3-4BC7-4D0B-B802-08D844D525AC"),
                ClientName = "ClientName 1",
                DateofBirth = new DateTime(1992, 09, 09),
                ClientSex = ClientSex.Male,
                Email = "Test@test.com",
                MobileNumber = "1234567880",
                RetirementAge = 62,
                InvestmentAmount = 100000,
                PensionPlan = PensionPlan.PensionSilver
            };
            mediatR.Setup(m => m.Send(It.IsAny<GetQuoteByIdRequest>(), It.IsAny<CancellationToken>())).Throws(new InvalidOperationException());
            controller = new QuoteController(mapper, mediatR.Object);
            var result = await controller.Put(updateQuoteModel) as ObjectResult;

            // Assert      
            Assert.NotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.AreEqual("Operation is not valid due to the current state of the object.", result.Value);
        }

        [Test]
        public async Task DeleteThrowsExceptionAsync()
        {
            // Act
            var quoteId = new Guid("6877d1a3-4bc7-4d0b-b802-08d844d525ac");
            mediatR.Setup(m => m.Send(It.IsAny<GetQuoteByIdRequest>(), It.IsAny<CancellationToken>())).Throws(new InvalidOperationException());
            controller = new QuoteController(mapper, mediatR.Object);
            var result = await controller.Delete(quoteId) as ObjectResult;

            // Assert      
            Assert.NotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.AreEqual("Operation is not valid due to the current state of the object.", result.Value);
        }
    }
}