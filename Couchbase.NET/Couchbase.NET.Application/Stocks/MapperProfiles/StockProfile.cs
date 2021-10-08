using AutoMapper;
using Couchbase.NET.Domain.Entities;

namespace Couchbase.NET.Application.ViewModels.MapperProfiles
{
    public class StockProfile : Profile
    {
        public StockProfile()
        {
            CreateMap<Stock, StockViewModel>();
            //.ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country));
            CreateMap<StockViewModel, Stock>();
        }
    }
}
