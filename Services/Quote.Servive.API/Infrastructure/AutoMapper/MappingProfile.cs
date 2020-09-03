namespace Quote.Service.API.Infrastructure.AutoMapper
{
    using global::AutoMapper;
    using Quote.Service.API.Domain.Entities;
    using Quote.Service.API.Models.Enum;
    using Quote.Service.API.Models.ResponseModels;
    using Quote.Service.API.Models.ResquestModels;
    using System;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateQuoteModel, Quote>()
                .ForMember(dest => dest.ClientSex, opt => opt.MapFrom(src => (int?)src.ClientSex))
                .ForMember(dest => dest.PensionPlan, opt => opt.MapFrom(src => (int?)src.PensionPlan))
                .ForMember(dest => dest.QuoteDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<UpdateQuoteModel, Quote>()
                .ForMember(dest => dest.ClientSex, opt => opt.MapFrom(src => (int?)src.ClientSex))
                .ForMember(dest => dest.PensionPlan, opt => opt.MapFrom(src => (int?)src.PensionPlan))
                .ForMember(dest => dest.QuoteDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<Quote, ResponseQuoteModel>()
                .ForMember(destination => destination.ClientSex,
                     opt => opt.MapFrom(source => Enum.GetName(typeof(ClientSex), source.ClientSex)))
                .ForMember(destination => destination.ClientSex,
                     opt => opt.MapFrom(source => Enum.GetName(typeof(ClientSex), source.ClientSex)));
        }
    }
}
