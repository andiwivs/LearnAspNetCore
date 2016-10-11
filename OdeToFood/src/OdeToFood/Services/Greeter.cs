using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdeToFood.Services
{
    public class Greeter : IGreeter
    {

        #region fields

        private string _greeting;

        #endregion

        #region constructor

        public Greeter(IConfiguration config)
        {
            _greeting = config["Greeting"];
        }

        #endregion

        #region IGreeting implementation

        public string GetGreeting()
        {
            return _greeting;
        }

        #endregion
    }
}
