using Microsoft.AspNetCore.Authorization;
using RestaurantAPI.Database;
using System.Security.Claims;

namespace RestaurantAPI.Authorization
{
    public class MinimumRestaurantsCreatedHandler : AuthorizationHandler<MinimumRestaurantsCreated>
    {
        private readonly RestaurantDbContext _dbContext;

        public MinimumRestaurantsCreatedHandler(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            MinimumRestaurantsCreated requirement)
        {
            var userId = int.Parse(context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var createdRestaurants = _dbContext.Restaurants.Count(r => r.CreatedById == userId);
            if (createdRestaurants >= requirement.MinimumRestaurantsCount)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;


            //var userEmail = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
            //_logger.LogInformation($"User: {userEmail} with date of birth [{dateOfBirth}]");

            //if (dateOfBirth.AddYears(requirement.MinimumAge) <= DateTime.Today)
            //{
            //    _logger.LogInformation("Authorization succeded");
            //    context.Succeed(requirement);
            //}
            //else
            //    _logger.LogInformation("Authorization failed");

            //return Task.CompletedTask;
        }
    }
}
