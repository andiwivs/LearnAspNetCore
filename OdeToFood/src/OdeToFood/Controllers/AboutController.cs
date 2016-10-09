using Microsoft.AspNetCore.Mvc;

namespace OdeToFood.Controllers
{
    [Route("[controller]/[action]")]
    public class AboutController
    {
        //[Route("")]
        public string Phone()
        {
            return "01234 567890";
        }

        //[Route("[action]")]
        public string Address()
        {
            return "London, England";
        }
    }
}
