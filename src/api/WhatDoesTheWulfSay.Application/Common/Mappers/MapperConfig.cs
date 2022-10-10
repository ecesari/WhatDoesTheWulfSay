using AutoMapper;
using WhatDoesTheWulfSay.Application.Models.ReviewModels;
using WhatDoesTheWulfSay.Application.Models.UserModels;
using WhatDoesTheWulfSay.Application.ReviewModels;
using WhatDoesTheWulfSay.Domain.Entities;

namespace WhatDoesTheWulfSay.Application.Common.Mappers
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<ReviewInsertCommand, Review>();
            CreateMap<User, UserViewModel>();
            CreateMap<Review, ReviewViewModel>()
               .ForMember(dest => dest.User, opt => opt.MapFrom(s =>s.CreatedUser));

        }
    }
}
