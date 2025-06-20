using AutoMapper;
using TutorialApplication.DTO;
using TutorialApplication.Services.Commands;
using TutorialDomain.Entities;

namespace TutorialApplication.Profiles
{
    public class DishProfile : Profile
    {
        public DishProfile()
        {
            CreateMap<Dish, DishDto>();
            CreateMap<DishDto, Dish>();
            CreateMap<CreateDishCommand, Dish>();

        }
    }
}
