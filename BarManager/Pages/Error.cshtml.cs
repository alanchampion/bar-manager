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
        private readonly ILogger<ErrorModel> _logger;
        private Util _util;
        private readonly IConfiguration _config;

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public ErrorModel(IConfiguration configuration, ILogger<ErrorModel> logger)
        {
            _logger = logger;
            _config= configuration;
            _util = new Util(logger);
        }

        public void OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }
    }
}
