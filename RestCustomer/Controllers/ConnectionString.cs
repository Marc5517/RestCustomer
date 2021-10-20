using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestCustomer.Controllers
{
    internal class ConnectionString
    {
        /// <summary>
        /// Skaber forbindelse til databasen i Azure.
        /// </summary>
        public static readonly string connectionString = "Server=tcp:oursqlservice.database.windows.net,1433;Initial Catalog=RestDB;Persist Security Info=False;User ID=Secret!;Password=12345678A!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
    }
}
