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
    /// <summary>
    /// Basic controller.
    /// </summary>
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private TennisAssociationContext db;
        public HomeController(/*ILogger<HomeController> logger, */TennisAssociationContext context )
        {
          //  _logger = logger;
            db = context;
        }

        public IActionResult Index()
        {
            var exist = db.Database.CanConnect();

            var players = db.Matches.ToList();

            /*var query = from b in db.Players
              select b.FirstName;

            foreach (var item in query) {
                Debug.WriteLine(item);
            }*/

            return this.Json(players);
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
