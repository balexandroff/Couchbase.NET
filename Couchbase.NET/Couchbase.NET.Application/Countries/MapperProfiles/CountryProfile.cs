using AutoMapper;
using Couchbase.NET.Domain.Entities;

namespace Couchbase.NET.Application.ViewModels.MapperProfiles
{
    public class CountryProfile : Profile
    {
        public CountryProfile()
        {
            CreateMap<Country, CountryViewModel>();
            CreateMap<CountryViewModel, Country>();
        }
    }
}
