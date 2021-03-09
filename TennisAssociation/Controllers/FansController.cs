using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TennisAssociation.Models;

namespace TennisAssociation.Controllers
{
    [Route("api/fans")]
    [ApiController]
    public class FansController : Controller
    {
        private readonly TennisAssociationContext db;

        public FansController(TennisAssociationContext context)
        {
            db = context;
        }
        
        // GET 
        [HttpGet]
        public IActionResult Get()
        {
            var fans = db.Fans.ToList();
            return Json(fans);
        }

        [HttpGet("{username}")]
        public IActionResult Get(string username, string password)
        {
            var fan = db.Fans.Where(b => b.Username == username).First();
            return password == fan.Password ? Json(fan) : Json("Wrong password");
        }

        [HttpPost]
        public Boolean InsertFan(Fan fan)
        {
            db.Fans.Add(fan);
            try
            {
                db.SaveChanges();
            }
            
            catch (DbUpdateException ex)
            {
                db.Fans.Remove(fan);
                return false;
            }

            return true;
        }
    }
}