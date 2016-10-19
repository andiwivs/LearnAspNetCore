using OdeToFood.Entities;
using System.Collections.Generic;

namespace OdeToFood.ViewModels
{
    public class HomePageVM
    {
        public IEnumerable<Restaurant> Restaurants { get; set; }
    }
}
