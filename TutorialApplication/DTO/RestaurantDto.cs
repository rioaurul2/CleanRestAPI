using TutorialDomain.Entities;

namespace TutorialApplication.DTO
{
    public class RestaurantDto
    {
        public string Name { get; set; } = default!;
        public string Category { get; set; } = default!;
        public bool HasDelivery { get; set; }
        public string Street { get; set; } = default!;
        public string City { get; set; } = default!;
        public string PostalCode { get; set; } = default!;
        public List<DishDto> Dishes { get; set; } = [];


        //replaced by automapper
        //public static RestaurantDto FromEntity(Restaurant? restaurant)
        //{
        //    if(restaurant == null)
        //        return default!;

        //    return new RestaurantDto()
        //    {
        //        Name = restaurant.Name,
        //        Category = restaurant.Category,
        //        HasDelivery = restaurant.HasDelivery,
        //        Street = restaurant.Address?.Street!,
        //        PostalCode = restaurant.Address?.PostalCode!,
        //        City = restaurant.Address?.City!,

        //        Dishes = restaurant.Dishes.Select(DishDto.FromEntity).ToList(),
        //    };
        //}
    }
}
