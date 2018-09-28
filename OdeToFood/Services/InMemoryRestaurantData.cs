using System.Collections.Generic;
using System.Linq;
using OdeToFood.Models;

namespace OdeToFood.Services
{
   public class InMemoryRestaurantData : IRestaurantData
   {
      private List<Restaurant> _restaurant;
      public InMemoryRestaurantData()
      {
         _restaurant = new List<Restaurant>
         {
            new Restaurant {Id = 1, Name = "Scott's Pizza Place"},
            new Restaurant {Id = 2, Name = "Tersiguels"},
            new Restaurant {Id = 3, Name = "King's Contrivance"}
         };
      }

      public IEnumerable<Restaurant> GetAll()
      {
         return _restaurant.OrderBy(o => o.Name);
      }

      public Restaurant Get(int id)
      {
         return _restaurant.FirstOrDefault(r => r.Id == id);
      }

      public Restaurant Add(Restaurant restaurant)
      {
         restaurant.Id = _restaurant.Max(r => r.Id) + 1;
         _restaurant.Add(restaurant);

         return restaurant;
      }
   }
}