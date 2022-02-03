using RestaurantAPI.Database.Entities;

namespace RestaurantAPI.Database
{
    public class RestaurantSeeder
    {
        private RestaurantDbContext _dbContext;
        public RestaurantSeeder(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if(_dbContext.Roles.Any() == false)
                {
                    var roles= GetRoles();
                    _dbContext.Roles.AddRange(roles);
                    _dbContext.SaveChanges();
                }

                if (_dbContext.Restaurants.Any() == false)
                {
                    var restaurants = GetRestaurants();
                    _dbContext.Restaurants.AddRange(restaurants);
                    _dbContext.SaveChanges();
                }            
            }
        }

        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role(){ Name = "User"},
                new Role(){ Name = "Manager"},
                new Role(){ Name = "Admin"},
            };

            return roles;
        }

        private IEnumerable<Entities.Restaurant> GetRestaurants()
        {
            return new List<Entities.Restaurant>()
            {
                new Entities.Restaurant()
                {
                    Name = "KFC",
                    Category = "Fast Food",
                    Description = "Kentucky Fried Chicken",
                    ContactEmail = "contact@kfc.com",
                    ContactNumber = "123456789",
                    HasDelivery = true,
                    Dishes = new List<Dish>()
                    {
                        new Dish()
                        {
                            Name = "Nashville Hot Chicken",
                            Description = "Some chickens with sauce",
                            Price = 10.30M
                        },
                        new Dish()
                        {
                            Name = "Chicken Nuggets",
                            Description = "Some chickens without sauce",
                            Price = 5.30M
                        }
                    },
                    Address = new Address()
                    {
                        City = "Krakow",
                        Street = "Dluga 5",
                        PostalCode = "30-001"
                    }
                },
                new Entities.Restaurant()
                {
                    Name = "McDonald",
                    Category = "Fast Food",
                    Description = "Hamburgers and stuff",
                    ContactEmail = "contact@mcd.com",
                    ContactNumber = "987654321",
                    HasDelivery = true,
                    Dishes = new List<Dish>()
                    {
                        new Dish()
                        {
                            Name = "Drwal Sandwich",
                            Description = "Very tasty sandwich only on winter time",
                            Price = 10.30M
                        },
                        new Dish()
                        {
                            Name = "BigMac",
                            Description = "Classic McDonalds burger",
                            Price = 5.30M
                        }
                    },
                    Address = new Address()
                    {
                        City = "Warsaw",
                        Street = "Conrada 20",
                        PostalCode = "01-922"
                    }
                }
            };
        }
    }
}
