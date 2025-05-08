using TutorialDomain.Entities;

namespace TutorialApplication.DTO
{
    public class DishDto
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; }
        public int? KiloCalories { get; set; }
        public int RestaurantId { get; set; }

        //replaced by automapper
        //public static DishDto FromEntity(Dish? dish)
        //{
        //    if (dish == null)
        //    {
        //        return default!;
        //    }

        //    return new DishDto()
        //    {
        //        Name = dish.Name,
        //        Description = dish.Description,
        //        Price = dish.Price,
        //        KiloCalories = dish.KiloCalories,
        //    };
        //}
    }
}
