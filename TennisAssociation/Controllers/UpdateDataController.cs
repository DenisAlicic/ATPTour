using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;
using TennisAssociation.Models;
using Microsoft.EntityFrameworkCore.Migrations;
using TennisAssociation.Interfaces;
using TennisAssociation.Services;

namespace TennisAssociation.Controllers
{
    [Route("api/updateData")]
    [ApiController]
    public class UpdateDataController : Controller
    {
        private readonly string tournamentsFile = "UpdatingScripts/tournaments.txt";
        private readonly TennisAssociationContext db;

        public UpdateDataController(TennisAssociationContext context)
        {
            db = context;
        }

        [HttpGet("tournaments")]
        // GET
        public bool Get()
        {
            
           IDatabaseUpdater service = new TournamentsUpdaterService(".", tournamentsFile, db);
           return service.UpdateData();
        }
    }
}