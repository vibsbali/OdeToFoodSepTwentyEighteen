﻿using System.ComponentModel.DataAnnotations;
using OdeToFood.Models;

namespace OdeToFood.ViewModels
{
   public class RestaurantViewModel
    {
       [Required, MaxLength(80)]
      public string Name { get; set; }
       public CuisineType Cuisine { get; set; }
   }
}
