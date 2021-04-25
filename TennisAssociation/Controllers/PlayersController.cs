using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TennisAssociation.Models;

namespace TennisAssociation.Controllers
{
    [Route("api/players")]
    [ApiController]
    public class PlayersController : Controller
    {
        private readonly TennisAssociationContext db;
        public PlayersController(TennisAssociationContext context)
        {
            db = context;
        }

        // GET api/players/
        /// <summary>
        /// Return all players from database.
        /// </summary>
        /// <returns>json containing information about players</returns>
        [HttpGet]
        public IActionResult Get()
        {
            var players = db.Players.ToList();
            return Json(players);
        }

        // GET api/players/<firstName>/<lastName>
        /// <summary>
        /// Return player(s) with given name.
        /// </summary>
        /// <param name="firstName">Player's first name.</param>
        /// <param name="lastName">Player's last name.</param>
        /// <returns>json of players</returns>
        [HttpGet("{firstName}/{lastName}")]
        public IActionResult Get(string firstName, string lastName)
        {
            var players = db.Players
                .Where(b => b.FirstName == firstName && b.LastName == lastName)
                .ToList();
            return Json(players);
        }

        // GET api/players/<id>
        /// <summary>
        /// Return a player based on id
        /// </summary>
        /// <param name="id">id of player</param>
        /// <returns>json of player</returns>
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var player = db.Players.Where(b => b.Id == id).First();
            return Json(player);
        }

        [HttpGet("hand/left")]
        public IActionResult LeftHanded()
        {
            var players = db.Players.Where(p => p.Hand == "left").ToList();
            return Json(players);
        }
        
        [HttpGet("hand/right")]
        public IActionResult RightHanded()
        {
            var players = db.Players.Where(p => p.Hand == "right").ToList();
            return Json(players);
        }
    }
}
