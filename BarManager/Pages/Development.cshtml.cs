using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace BarManager.Pages
{
    public class DevelopmentModel : PageModel
    {
        public string Variables { get; set; }
        public string iis { get; set; }
        public string Testing { get; set; }
        private readonly IConfiguration _config;

        public DevelopmentModel(IConfiguration config)
        {
            _config = config;
        }

        public void OnGet()
        {
            if (Util.isLocalEnv())
            {
                foreach (KeyValuePair<string, string> pair in _config.AsEnumerable().ToList())
                {
                    Variables += pair.Key + ": " + pair.Value + "<br>";
                }
                iis = "iis: " + _config.GetValue<string>("iis:env:3: Testing");
                Testing = "Testing: " + _config.GetValue<string>("Testing");
            }
        }
    }
}