using System.Collections.Generic;
using System.Linq;
using OdeToFood.Data;
using OdeToFood.Models;

namespace OdeToFood.Services
{
    public class SqlRestaurantData : IRestaurantData
    {
       private readonly OdeToFoodDbContext _ctx;

       public SqlRestaurantData(OdeToFoodDbContext ctx)
       {
          _ctx = ctx;
       }
       public IEnumerable<Restaurant> GetAll()
       {
          return _ctx.Restaurants.OrderBy(r => r.Name);
       }

       public Restaurant Get(int id)
       {
          return _ctx.Restaurants.FirstOrDefault(r => r.Id == id);
       }

       public Restaurant Add(Restaurant restaurant)
       {
          _ctx.Restaurants.Add(restaurant);
          _ctx.SaveChanges();

          return restaurant;
       }
    }
}
