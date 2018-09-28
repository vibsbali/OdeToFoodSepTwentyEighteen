using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OdeToFood.Models;
using OdeToFood.Services;
using OdeToFood.ViewModels;

namespace OdeToFood.Controllers
{
    public class HomeController : Controller
    {
       private readonly IRestaurantData _restaurantData;
       private readonly IGreeter _greeter;

       public HomeController(IRestaurantData restaurantData, IGreeter greeter)
       {
          _restaurantData = restaurantData;
          _greeter = greeter;
       }
       public IActionResult Index()
       {
          var restaurants = _restaurantData.GetAll();

          var model = new HomeIndexViewModel
          {
             CurrentMessage = _greeter.GetMessageOfTheDay(),
             Restaurants = restaurants
          };

          return View(model);
       }

       public IActionResult Details(int id)
       {
          var model = _restaurantData.Get(id);
         if (model == null)
         {
            return RedirectToAction(nameof(Index));
         }
          return View(model);
       }

       public IActionResult Create()
       {
          return View();
       }

       [HttpPost]
       [ValidateAntiForgeryToken]
       public IActionResult Create(RestaurantViewModel viewModel)
       {
          if (ModelState.IsValid)
          {
             var newRestaurant = new Restaurant
             {
                Name = viewModel.Name,
                Cuisine = viewModel.Cuisine
             };

             newRestaurant = _restaurantData.Add(newRestaurant);
             return RedirectToAction(nameof(Details), new { id = newRestaurant.Id});
          }

          return View(viewModel);
       }
   }
}
