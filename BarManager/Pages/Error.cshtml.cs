using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BarManager.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ErrorModel : PageModel
    {
        public string RequestId { get; set; }
        public string dbString { get; set; }
        private readonly ILogger<ErrorModel> _logger;
        private Util _util;
        private readonly IConfiguration _config;
        private readonly BarManager.Models.BarManagerContext _context;

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        public bool ShowDbString=> !string.IsNullOrEmpty(dbString);

        public ErrorModel(BarManager.Models.BarManagerContext context, IConfiguration configuration, ILogger<ErrorModel> logger)
        {
            _logger = logger;
            _config= configuration;
            _util = new Util(logger);
            _context = context;
        }

        public void OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            dbString = _util.getDbString(_config);
            /*foreach(KeyValuePair<string, string> pair in _config.AsEnumerable().ToList())
            {
                dbString += pair.Key + ": " + pair.Value + "<br>";
            }*/
        }
    }
}
