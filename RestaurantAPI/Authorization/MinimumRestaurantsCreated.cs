using Microsoft.AspNetCore.Authorization;

namespace RestaurantAPI.Authorization
{
    public class MinimumRestaurantsCreated: IAuthorizationRequirement
    {
        public int MinimumRestaurantsCount { get; }

        public MinimumRestaurantsCreated(int minimumRestaurantsCount)
        {
            MinimumRestaurantsCount = minimumRestaurantsCount;
        }
    }
}
