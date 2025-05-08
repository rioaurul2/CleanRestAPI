using AutoMapper;
using TutorialApplication.DTO;
using TutorialDomain.Entities;

namespace TutorialApplication.Profiles
{
    public class DishProfile : Profile
    {
        public DishProfile()
        {
            CreateMap<Dish, DishDto>();
        }
    }
}
