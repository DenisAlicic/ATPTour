using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TennisAssociation.Models;

namespace TennisAssociation.Controllers
{
    [Route("api/matches")]
    [ApiController]
    public class MatchesController : Controller
    {
        private readonly TennisAssociationContext db;
        public MatchesController(TennisAssociationContext context)
        {
            db = context;
        }

        // GET api/matches/
        /// <summary>
        /// Return all matches from database.
        /// </summary>
        /// <returns>json containing information about matches</returns>
        [HttpGet]
        public IActionResult Get()
        {
            var matches = db.Matches.ToList();
            return Json(matches);
        }

        // GET api/matches/<id>
        /// <summary>
        /// Return a match based on id
        /// </summary>
        /// <param name="id">id of a match</param>
        /// <returns>json of match</returns>
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var match = db.Matches.Where(b => b.Id == id).First();
            return Json(match);
        }
    }
}
