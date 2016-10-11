using OdeToFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdeToFood.Services
{

    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetAll();
    }

    public class InMemoryRestaurantData : IRestaurantData
    {

        private List<Restaurant> _restaurants;

        public InMemoryRestaurantData()
        {
            _restaurants = new List<Restaurant>
            {
                new Restaurant { Id = 1, Name = "King Curry" },
                new Restaurant { Id = 2, Name = "Queen Wok" },
                new Restaurant { Id = 3, Name = "Prince Chippy" }
            };
        }

        #region IRestaurantData implementation

        public IEnumerable<Restaurant> GetAll()
        {
            return _restaurants;
        }

        #endregion
    }
}
