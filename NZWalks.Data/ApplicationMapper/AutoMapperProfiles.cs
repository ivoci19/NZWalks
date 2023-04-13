using AutoMapper;
using NZWalks.Data.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.DTO;

namespace NZWalks.Data.ApplicationMapper
{
    public class AutoMapperProfiles : Profile
    {

        public AutoMapperProfiles() 
        {
            CreateMap<Region, RegionDTO>().ReverseMap();

            CreateMap<Region, RegionViewDTO>().ReverseMap();

            CreateMap<Walk, WalkDTO>()
               .ReverseMap();

            CreateMap<Difficulty, DifficultyDTO>()
                .ReverseMap();
        }

    }
}
