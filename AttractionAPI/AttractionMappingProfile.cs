using AttractionAPI.Entities;
using AttractionAPI.Models;
using AutoMapper;
using System.Collections.Generic;

namespace AttractionAPI
{
    public class AttractionMappingProfile : Profile
    {
        public AttractionMappingProfile()
        {
            CreateMap<Attraction, AttractionDto>()
                .ForMember(x => x.City, z => z.MapFrom(s => s.Address.City))
                .ForMember(x => x.Street, z => z.MapFrom(s => s.Address.Street))
                .ForMember(x => x.PostalCode, z => z.MapFrom(s => s.Address.PostalCode))
                .ForMember(x => x.PostalCode, z => z.MapFrom(s => s.Address.PostalCode));

            CreateMap<Comment, CommentDto>();

            CreateMap<CreateAttractionDto, Attraction>()
                .ForMember(x => x.Address, z => z.MapFrom(dto => new Address() 
                    { City = dto.City, Street = dto.Street, PostalCode = dto.PostalCode }));
        }
    }
}
