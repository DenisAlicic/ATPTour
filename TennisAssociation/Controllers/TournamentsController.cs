using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TennisAssociation.Models;
using Microsoft.AspNetCore.Authorization;

namespace TennisAssociation.Controllers
{
    /// <summary>
    /// Controller for tournaments handling.
    /// </summary>
    [Route("api/tournaments")]
    [ApiController]
    public class TournamentsController : Controller
    {
        private readonly TennisAssociationContext db;
        public TournamentsController(TennisAssociationContext context)
        {
            db = context;
        }

        // GET api/tournaments/
        /// <summary>
        /// Return all tournaments from database.
        /// </summary>
        /// <returns>json containing information about tournaments</returns>
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            var tournaments = db.Tournaments.ToList();
            return Json(tournaments);
        }

        /// GET api/tournaments/<name>
        /// <summary>
        /// Return tournament with a given name.
        /// </summary>
        /// <param name="firstName">Tournament's name.</param>
        /// <returns>json of tournament</returns>
        [HttpGet("{name}")]
        public IActionResult Get(string name)
        {
            var tournament = db.Tournaments
                .Where(b => b.Tournament1 == name)
                .ToList();
            return Json(tournament);
        }

        /// GET api/tournaments/points/<points>
        /// <summary>
        /// Return tournaments with a given points.
        /// </summary>
        /// <param name="points">Tournament's points.</param>
        /// <returns>json of tournaments</returns>
        [HttpGet("points/{points}")]
        public IActionResult Get(int points)
        {
            var tournament = db.Tournaments
                .Where(b => b.WinnerPoints == points)
                .ToList();
            return Json(tournament);
        }
    }
}