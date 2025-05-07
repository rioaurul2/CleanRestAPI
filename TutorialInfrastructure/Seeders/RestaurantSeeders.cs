
using TutorialDomain.Entities;
using TutorialInfrastructure.Context;

namespace TutorialInfrastructure.Seeders
{
    internal class RestaurantSeeders : IRestaurantSeeders
    {
        public readonly TutorialDbContext _dbContext;
        public RestaurantSeeders(TutorialDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Seed()
        {
            if (await _dbContext.Database.CanConnectAsync())
            {
                if (!_dbContext.Restaurants.Any())
                {
                    var restaurants = GetRestaurants();

                    _dbContext.Restaurants.AddRange(restaurants);
                    await _dbContext.SaveChangesAsync();
                }
            }
        }

        private List<Restaurant> GetRestaurants()
        {
            List<Restaurant> restaurants = new List<Restaurant>
            {
                new Restaurant
                {
                    Name = "La Mama",
                    Category = "Romanian",
                    HasDelivery = true,
                    ContactEmail = "contact@lamama.ro",
                    ContactNumber = "0740 123 456",
                    Address = new Address
                    {
                        Street = "Strada Bucuresti 10",
                        City = "Bucuresti",
                        PostalCode = "010101"
                    },
                    Dishes = new List<Dish>
                    {
                        new Dish
                        {
                            Name = "Sarmale",
                            Description = "Sarmale traditionale cu mamaliguta si smantana",
                            Price = 35.99m,
                            RestaurantId = 1
                        },
                        new Dish
                        {
                            Name = "Ciorba de burta",
                            Description = "Ciorba cu smantana si otet",
                            Price = 22.50m,
                            RestaurantId = 1
                        }
                    }
                },
                new Restaurant
                {
                    Name = "Pizza Fest",
                    Category = "Italian",
                    HasDelivery = true,
                    ContactEmail = "order@pizzafest.com",
                    ContactNumber = "0755 987 654",
                    Address = new Address
                    {
                        Street = "Bd. Libertatii 5",
                        City = "Cluj-Napoca",
                        PostalCode = "400123"
                    },
                    Dishes = new List<Dish>
                    {
                        new Dish
                        {
                            Name = "Pizza Margherita",
                            Description = "Clasica pizza cu sos de roșii și mozzarella",
                            Price = 29.90m,
                            RestaurantId = 2
                        },
                        new Dish
                        {
                            Name = "Spaghetti Carbonara",
                            Description = "Paste cu pancetta, ou și parmezan",
                            Price = 34.00m,
                            RestaurantId = 2
                        }
                    }
                },
                new Restaurant
                {
                    Name = "Green Bowl",
                    Category = "Vegan",
                    HasDelivery = false,
                    ContactEmail = "hello@greenbowl.eco",
                    ContactNumber = "0730 333 222",
                    Address = new Address
                    {
                        Street = "Str. Ecologiei 21",
                        City = "Timisoara",
                        PostalCode = "300045"
                    },
                    Dishes = new List<Dish>
                    {
                        new Dish
                        {
                            Name = "Bowl cu tofu",
                            Description = "Bowl vegan cu tofu prajit, quinoa și legume",
                            Price = 31.75m,
                            RestaurantId = 3
                        },
                        new Dish
                        {
                            Name = "Salata detox",
                            Description = "Salata verde cu seminte, avocado si dressing de lamaie",
                            Price = 27.80m,
                            RestaurantId = 3
                        }
                    }
                }
            };

            return restaurants;
        }
    }
}
