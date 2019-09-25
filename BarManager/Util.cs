using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;

namespace BarManager
{
    public class Util
    {
        private readonly ILogger _logger;

        public Util(ILogger logger)
        {
            _logger = logger;
        }

        public static bool isLocalEnv()
        {
            // Console.WriteLine(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").Equals("Local"))
                return true;
            else
                return false;
        }

        public static string GetFromEnvironmentVariables(string key)
        {
            return Environment.GetEnvironmentVariable(key);
        }

        public string getDbString(IConfiguration config)
        {
            if (isLocalEnv())
            {
                _logger.LogInformation("Db Connection string: " + config.GetConnectionString("BarManagerContext"));
                return config.GetConnectionString("BarManagerContext");
            }
            else
            {
                string dbname = config.GetValue<string>("plugins:rds:env:RDS_DB_NAME");

                if (string.IsNullOrEmpty(dbname)) return null;

                string username = config.GetValue<string>("plugins:rds:env:RDS_USERNAME");
                string password = config.GetValue<string>("plugins:rds:env:RDS_PASSWORD");
                string hostname = config.GetValue<string>("plugins:rds:env:RDS_HOSTNAME");
                string port = config.GetValue<string>("plugins:rds:env:RDS_PORT");
                string dbConnect =  "Server=" + hostname + ";Database=" + dbname + ";User=" + username + ";Password=" + password + ";MultipleActiveResultSets=true;";
                
                _logger.LogInformation("Db Connection string: " + dbConnect);
                return dbConnect;
            }
        }
    }
}
