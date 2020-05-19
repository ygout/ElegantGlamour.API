using AutoMapper;
using ElegantGlamour.Core.Models;
using ElegantGlamour.Core.Dtos;

namespace ElegantGlamour.Api.Mapping
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            // Entity to Dto
            CreateMap<Category, GetCategoryDto>();
            CreateMap<Prestation, GetPrestationDto>();

            // Dto to Entity
            CreateMap<AddCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();
            
            CreateMap<AddPrestationDto, Prestation>();
            CreateMap<UpdatePrestationDto, Prestation>();

        }
    }
}