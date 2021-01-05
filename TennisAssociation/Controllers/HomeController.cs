using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using TennisAssociation.DAL;
using TennisAssociation.Models;

namespace TennisAssociation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public TennisContext db;
        public HomeController(ILogger<HomeController> logger, TennisContext context )
        {
            _logger = logger;
            db = context;
        }

        public IActionResult Index()
        {
            var tmp = db.Model;
            /*
            db.Fans.Add(new FanModel { Email = "pera@gmal.com", ID = 1, Name = "Pera", Surname = "Peric" });
            db.SaveChanges();
            var listaFanova = db.Fans.ToList();
            */
            SqlConnection connection = new SqlConnection("Server=EN510626\\SQLEXPRESS;Initial Catalog=TennisAssociation;persist security info=True;Integrated Security=SSPI;");
            SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM players", connection);
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
            

            var count = db.Players.Count();
            return View(db.Players.ToList());
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
