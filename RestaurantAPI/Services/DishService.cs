using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Database;
using RestaurantAPI.Database.Dtos;
using RestaurantAPI.Database.Entities;
using RestaurantAPI.Exceptions;

namespace RestaurantAPI.Services
{
    public class DishService: IDishService
    {
        private readonly RestaurantDbContext _context;
        private readonly IMapper _mapper;

        public DishService(RestaurantDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Create(int restaurantId, CreateDishDto dto)
        {
            var restaurant = GetRestaurantById(restaurantId);

            var dishEntity = _mapper.Map<Dish>(dto);
            dishEntity.RestaurantId = restaurantId;

            _context.Dishes.Add(dishEntity);
            _context.SaveChanges();


            return dishEntity.Id;
        }

        public DishDto GetById(int restaurantId, int dishId)
        {
            var restaurant = GetRestaurantById(restaurantId);

            var dish = _context.Dishes.FirstOrDefault(d => d.Id == dishId);
            if(dish is null || dish.RestaurantId != restaurantId)
            {
                throw new NotFoundException($"Dish with id {dishId} not found in Restaurant with id {restaurantId}");
            }

            var dishDto = _mapper.Map<DishDto>(dish);
            return dishDto;

        }

        public List<DishDto> GetAll(int restaurantId)
        {
            var restaurant = GetRestaurantById(restaurantId);

            var dishDtos = _mapper.Map<List<DishDto>>(restaurant.Dishes);

            return dishDtos;
        }

        public void RemoveAll(int restaurantId)
        {
            var restaurant = GetRestaurantById(restaurantId);

            _context.RemoveRange(restaurant.Dishes);
            _context.SaveChanges();
        }

        public void RemoveDish(int restaurantId, int dishId)
        {
            var restaurant = GetRestaurantById(restaurantId);
            var dish = _context.Dishes.FirstOrDefault(d => d.Id == dishId);
            if (dish is null || dish.RestaurantId != restaurantId)
            {
                throw new NotFoundException($"Dish with id {dishId} not found in Restaurant with id {restaurantId}");
            }

            _context.Remove(dish);
            _context.SaveChanges();
        }


        private Restaurant GetRestaurantById(int restaurantId)
        {
            var restaurant = _context
                .Restaurants
                .Include(r => r.Dishes)
                .FirstOrDefault(r => r.Id == restaurantId);

            if (restaurant is null)
                throw new NotFoundException($"Restaurant with id {restaurantId} not found");

            return restaurant;
        }
    }
}
