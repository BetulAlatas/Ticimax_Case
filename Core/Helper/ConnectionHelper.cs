using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helper
{
    public static class ConnectionHelper
    {
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["DbContext"].ToString();
        }

    }
}
