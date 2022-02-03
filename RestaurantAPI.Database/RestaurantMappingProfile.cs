using AutoMapper;
using RestaurantAPI.Database.Dtos;
using RestaurantAPI.Database.Entities;

namespace RestaurantAPI.Database
{
    public class RestaurantMappingProfile: Profile
    {
        public RestaurantMappingProfile()
        {
            CreateMap<Restaurant, RestaurantDto>()
                .ForMember(m => m.City, c => c.MapFrom(s => s.Address.City))
                .ForMember(m => m.Street, c => c.MapFrom(s => s.Address.Street))
                .ForMember(m => m.PostalCode, c => c.MapFrom(s => s.Address.PostalCode));

            CreateMap<Dish, DishDto>();

            CreateMap<CreateRestaurantDto, Entities.Restaurant>()
                .ForMember(r => r.Address, 
                    c => c.MapFrom(dto => new Address()
                        { City = dto.City, PostalCode = dto.PostalCode, Street = dto.Street }));


            CreateMap<CreateDishDto, Dish>();
        }
    }
}
