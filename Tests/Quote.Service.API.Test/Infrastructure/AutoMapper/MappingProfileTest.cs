namespace Quote.Service.API.Test.Infrastructure.AutoMapper
{
    using FluentAssertions;
    using global::AutoMapper;
    using NUnit.Framework;
    using Quote.Service.API.Domain.Entities;
    using Quote.Service.API.Infrastructure.AutoMapper;
    using Quote.Service.API.Models.Enum;
    using Quote.Service.API.Models.ResponseModels;
    using Quote.Service.API.Models.ResquestModels;
    using System;

    public class MappingProfileTests
    {
        private CreateQuoteModel createQuoteModel;
        private UpdateQuoteModel updateQuoteModel;
        private Quote quoteEntity;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            _mapper = mockMapper.CreateMapper();

            createQuoteModel = new CreateQuoteModel
            {
                ClientName = "ClientName 1",
                DateofBirth = new DateTime(1992, 09, 09),
                ClientSex = ClientSex.Male,
                Email = "Test@test.com",
                MobileNumber = "1234567880",
                RetirementAge = 66,
                InvestmentAmount = 300000,
                PensionPlan = PensionPlan.PensionPlatinum
            };
            updateQuoteModel = new UpdateQuoteModel
            {
                Id = Guid.Parse("9f35b48d-cb87-4783-bfdb-21e36012930a"),
                ClientName = "ClientName 1",
                DateofBirth = new DateTime(1992, 09, 09),
                ClientSex = ClientSex.Male,
                Email = "Test@test.com",
                MobileNumber = "1234567880",
                RetirementAge = 66,
                InvestmentAmount = 100000,
                PensionPlan = PensionPlan.PensionSilver
            };

            quoteEntity = new Quote
            {
                Id = Guid.NewGuid(),
                ClientName = "ClientName 2",
                DateofBirth = new DateTime(1992, 09, 09),
                ClientSex = 1,
                Email = "Test@test.com",
                MobileNumber = "1234567880",
                QuoteDate = new DateTime(2020, 08, 08),
                RetirementAge = 66,
                InvestmentAmount = 100000,
                MaturityAmount = 1500,
                PensionPlan = 1
            };
        }

        [Test]
        public void Map_Quote_CreateQuoteModel_ShouldHaveValidConfig()
        {
            var configuration = new MapperConfiguration(cfg =>
               cfg.CreateMap<Quote, CreateQuoteModel>());

            configuration.AssertConfigurationIsValid();
        }

        [Test]
        public void Map_Quote_UpdateQuoteModel_ShouldHaveValidConfig()
        {
            var configuration = new MapperConfiguration(cfg =>
                cfg.CreateMap<Quote, UpdateQuoteModel>());

            configuration.AssertConfigurationIsValid();
        }

        [Test]
        public void Map_ResponseQuoteModel_Quote_ShouldHaveValidConfig()
        {
            var configuration = new MapperConfiguration(cfg =>
                cfg.CreateMap<Quote, ResponseQuoteModel>());

            configuration.AssertConfigurationIsValid();
        }

        [Test]
        public void Map_Quote_Quote_ShouldHaveValidConfig()
        {
            var configuration = new MapperConfiguration(cfg =>
                cfg.CreateMap<Quote, Quote>());

            configuration.AssertConfigurationIsValid();
        }

        [Test]
        public void Map_CreateQuoteModel_Quote()
        {
            var quote = _mapper.Map<Quote>(createQuoteModel);

            quote.ClientName.Should().Be(createQuoteModel.ClientName);
            quote.ClientSex.Should().Be((int)createQuoteModel.ClientSex);
            quote.DateofBirth.Should().Be(createQuoteModel.DateofBirth);
            quote.Email.Should().Be(createQuoteModel.Email);
            quote.MobileNumber.Should().Be(createQuoteModel.MobileNumber);
            quote.InvestmentAmount.Should().Be(createQuoteModel.InvestmentAmount);
            quote.PensionPlan.Should().Be((int)createQuoteModel.PensionPlan);
            quote.RetirementAge.Should().Be(createQuoteModel.RetirementAge);
        }

        [Test]
        public void Map_UpdateQuoteModel_Quote()
        {
            var quote = _mapper.Map<Quote>(updateQuoteModel);

            quote.Id.Should().Be(updateQuoteModel.Id);
            quote.ClientName.Should().Be(updateQuoteModel.ClientName);
            quote.ClientSex.Should().Be((int)updateQuoteModel.ClientSex);
            quote.DateofBirth.Should().Be(updateQuoteModel.DateofBirth);
            quote.Email.Should().Be(updateQuoteModel.Email);
            quote.MobileNumber.Should().Be(updateQuoteModel.MobileNumber);
            quote.InvestmentAmount.Should().Be(updateQuoteModel.InvestmentAmount);
            quote.PensionPlan.Should().Be((int)updateQuoteModel.PensionPlan);
            quote.RetirementAge.Should().Be(updateQuoteModel.RetirementAge);
        }

        [Test]
        public void Map_Quote_ResponseQuoteModel()
        {
            var responseQuoteModel = _mapper.Map<ResponseQuoteModel>(quoteEntity);

            responseQuoteModel.Id.Should().Be(quoteEntity.Id);
            responseQuoteModel.ClientName.Should().Be(quoteEntity.ClientName);
            responseQuoteModel.ClientSex.Should().Be(quoteEntity.ClientSex);
            responseQuoteModel.DateofBirth.Should().Be(quoteEntity.DateofBirth);
            responseQuoteModel.Email.Should().Be(quoteEntity.Email);
            responseQuoteModel.QuoteDate.Should().Be(quoteEntity.QuoteDate);
            responseQuoteModel.MobileNumber.Should().Be(quoteEntity.MobileNumber);
            responseQuoteModel.InvestmentAmount.Should().Be(quoteEntity.InvestmentAmount);
            responseQuoteModel.PensionPlan.Should().Be(quoteEntity.PensionPlan);
            responseQuoteModel.RetirementAge.Should().Be(quoteEntity.RetirementAge);
            responseQuoteModel.MaturityAmount.Should().Be(quoteEntity.MaturityAmount);
        }

        [Test]
        public void Map_CreateQuoteModel_Quote_PensionSilver()
        {
            createQuoteModel.PensionPlan = PensionPlan.PensionSilver;
            var quote = _mapper.Map<Quote>(createQuoteModel);

            quote.PensionPlan.Should().Be((int)createQuoteModel.PensionPlan);
        }

        [Test]
        public void Map_CreateQuoteModel_Quote_PensionGold()
        {
            createQuoteModel.PensionPlan = PensionPlan.PensionGold;
            var quote = _mapper.Map<Quote>(createQuoteModel);

            quote.PensionPlan.Should().Be((int)createQuoteModel.PensionPlan);
        }
    }
}