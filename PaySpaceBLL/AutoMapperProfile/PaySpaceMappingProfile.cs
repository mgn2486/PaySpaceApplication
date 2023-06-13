using AutoMapper;
using PaySpaceBLL.DomainModels;
using PaySpaceDAL;
using PaySpaceDAL.Entities;

namespace PaySpaceBLL.AutoMapperProfile;

public class PaySpaceMappingProfile : Profile
{
    public PaySpaceMappingProfile()
    {
        CreateMap<TaxRangeDto, TaxRange>().ReverseMap();

        CreateMap<TaxNameDto, TaxName>()
            .ForMember(s => s.TaxRanges, c => c.MapFrom(m => m.TaxRanges)).ReverseMap();

        CreateMap<TaxNameCreateDto, TaxName>().ReverseMap();

        CreateMap<PostalCodeDto, PostalCode>().ReverseMap();

        CreateMap<GetPostalCodeDto, PostalCode>().ReverseMap();
    }
}
