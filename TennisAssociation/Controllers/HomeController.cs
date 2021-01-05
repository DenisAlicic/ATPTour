using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using TennisAssociation.Models;

namespace TennisAssociation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public TennisAssociationContext db;
        public HomeController(ILogger<HomeController> logger, TennisAssociationContext context )
        {
            _logger = logger;
            db = context;
        }

        public IActionResult Index()
        {
            var tmp = db.Model;
            
            SqlConnection connection = new SqlConnection("Server=EN510626\\SQLEXPRESS;Initial Catalog=TennisAssociation;Trusted_connection=True");
            SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM matches", connection);
            connection.Open();
            using (IDataReader dr = command.ExecuteReader())
            {
                while (dr.Read())
                {
                    Console.WriteLine(dr[0].ToString());
                }
            }
            
            connection.Close();
            connection.Dispose();
            
            var exist = db.Database.CanConnect();
            

            var query = from b in db.Players
              select b.FirstName;

            Console.WriteLine("All All student in the database:");

            foreach (var item in query) {
                Console.WriteLine(item);
            }
            return View(query);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
