using Microsoft.AspNetCore.Mvc;
using OdeToFood.Entities;
using OdeToFood.Services;
using OdeToFood.ViewModels;

namespace OdeToFood.Controllers
{
    public class HomeController : Controller
    {

        private IRestaurantData _restaurantData;

        public HomeController(IRestaurantData restaurantData)
        {
            _restaurantData = restaurantData;
        }

        public IActionResult Index()
        {
            var model = new HomePageVM {
                Restaurants = _restaurantData.GetAll()
            };

            return View(model);
        }

        public IActionResult Detail(int id)
        {
            var model = _restaurantData.Get(id);

            if (model == null)
                return RedirectToAction(nameof(Index));

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RestaurantEditVM model)
        {

            if (ModelState.IsValid)
            {

                var newRestaurant = new Restaurant
                {
                    Name = model.Name,
                    Cuisine = model.Cuisine
                };

                newRestaurant = _restaurantData.Add(newRestaurant);

                _restaurantData.Commit();

                return RedirectToAction("Detail", new { id = newRestaurant.Id });
            }

            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = _restaurantData.Get(id);

            if (model == null)
                return RedirectToAction(nameof(Index));

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(int id, RestaurantEditVM model)
        {

            var restaurant = _restaurantData.Get(id);

            if (restaurant == null)
                return RedirectToAction(nameof(Index));

            if (ModelState.IsValid)
            {

                restaurant.Name = model.Name;
                restaurant.Cuisine = model.Cuisine;

                _restaurantData.Commit();

                return RedirectToAction(nameof(Detail), new { id = restaurant.Id });
            }

            return View(restaurant);
        }
    }
}
