using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TennisAssociation.Models;

namespace TennisAssociation.Controllers
{
    /// <summary>
    /// Controller for statistics.
    /// </summary>
    [Route("api/statistics")]
    [ApiController]
    public class StatisticsContorller : Controller
    {
        private readonly TennisAssociationContext db;
        public StatisticsContorller(TennisAssociationContext context)
        {
            db = context;
        }

        // GET api/statistics/countries
        /// <summary>
        /// Get country occurrence in top 100.
        /// </summary>
        /// <returns>Json of country, occurrence</returns>
        [HttpGet("countries")]
        public IActionResult GetCountries()
        {
            var countries = db.Players
                .Where(p => p.CurrentRankingSingle <= 100)
                .GroupBy(p => p.Country)
                .Select(g => new { country = g.Key, count = g.Count() })
                .OrderByDescending(x => x.count)
                .ToList();

            return Json(countries);
        }


        // GET api/statistics/hand
        /// <summary>
        /// Get number of players who play with right and left hand in top 100.
        /// </summary>
        /// <returns>Json of hand, occurrence</returns>
        [HttpGet("hand")]
        public IActionResult GetHand()
        {
            var hand = db.Players
                .Where(p => p.CurrentRankingSingle <= 100)
                .GroupBy(p => p.Hand)
                .Select(g => new { hand = g.Key, count = g.Count() })
                .OrderByDescending(x => x.count)
                .ToList();

            return Json(hand);
        }

        /// <summary>  
        /// Calculates age.
        /// </summary>  
        /// <param name="dateOfBirth">Date of birth</param>  
        /// <returns> age e.g. 26</returns>  
        private static int CalculateAge(DateTime dateOfBirth)  
        {  
            return (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear) ?
                    DateTime.Now.Year - dateOfBirth.Year - 1:
                    DateTime.Now.Year - dateOfBirth.Year;  
        }  

        // GET api/statistics/years
        /// <summary>
        /// Get players' years in top 100.
        /// </summary>
        /// <returns>Json of year, occurrence</returns>
        [HttpGet("years")]
        public IActionResult GetYears()
        {
            var years = db.Players
                .Where(p => p.CurrentRankingSingle <= 100 && p.Birth.HasValue)
                .AsEnumerable() // Get result to LINQ to Objects
                .GroupBy(p => CalculateAge(p.Birth.Value))
                .Select(g => new { age = g.Key, count = g.Count() })
                .OrderByDescending(x => x.count)
                .ToList();

            return Json(years);
        }


        /// <summary>  
        /// Gets height range.
        /// </summary>  
        /// <param name="height">Height.</param>  
        /// <returns> age e.g. 180-185</returns>  
        private static string GetHeightRange(int height)  
        {  
            int x = height % 5;
            int lowerBound = height - x;
            int upperBound = lowerBound + 5;
            return lowerBound.ToString() + "-" + upperBound.ToString();
        }  

        /// GET api/statistics/heights
        /// <summary>
        /// Get players' heights in top 100.
        /// </summary>
        /// <returns>Json of height range, occurrence</returns>
        [HttpGet("heights")]
        public IActionResult GetHeights()
        {
            var heights = db.Players
                .Where(p => p.CurrentRankingSingle <= 100 && p.Height.HasValue)
                .AsEnumerable() // Get result to LINQ to Objects
                .GroupBy(p => GetHeightRange(p.Height.Value))
                .Select(g => new { heightRange = g.Key, count = g.Count() })
                .OrderByDescending(x => x.count)
                .ToList();

            return Json(heights);
        }
    }
}