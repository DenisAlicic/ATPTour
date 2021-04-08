using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TennisAssociation.Controllers;
using TennisAssociation.Interfaces;
using TennisAssociation.Models;
using TennisAssociation.Utils;

namespace TennisAssociation.Services
{
    public class TournamentsUpdaterService : IDatabaseUpdater
    {
        private readonly string pathToScript;
        private readonly string pathToFile;
        private ICsvReader reader;
        private TennisAssociationContext db;
        
        public TournamentsUpdaterService(string pathToScript, string pathToFile, TennisAssociationContext context)
        {
            this.pathToFile = pathToFile;
            this.pathToScript = pathToScript;
            reader = new CsvReader(pathToFile);
            db = context;
        }

        public bool UpdateData()
        {
            List<Tournament> tournamentsForAdding = new List<Tournament>();
            Dictionary<string, int> columnNumbers = reader.GetColumns();
            List<List<string>> rows = reader.GetRows();
            if (columnNumbers is null || rows is null)
            {
                return false;
            }
            foreach (var row in rows)
            {
                var t = new Tournament
                {
                    _1RoundPrize = row[columnNumbers["1. round prize"]] != null
                        ? (int?) Int32.Parse(row[columnNumbers["1. round prize"]])
                        : null,
                    _2RoundPrize = row[columnNumbers["2. round prize"]] != null
                        ? (int?) Int32.Parse(row[columnNumbers["2. round prize"]])
                        : null,
                    Tournament1 = row[columnNumbers["tournament"]],
                    _1RoundPoints = row[columnNumbers["1. round points"]] != null
                        ? (int?) Int32.Parse(row[columnNumbers["1. round points"]])
                        : null,
                    _2RoundPoints = row[columnNumbers["2. round points"]] != null
                        ? (int?) Int32.Parse(row[columnNumbers["2. round points"]])
                        : null,
                    _3RountPoints = row[columnNumbers["3. rount points"]] != null
                        ? (int?) Int32.Parse(row[columnNumbers["3. rount points"]])
                        : null,
                    _3RoundPrize = row[columnNumbers["3. round prize"]] != null
                        ? (int?) Int32.Parse(row[columnNumbers["3. round prize"]])
                        : null,
                    RoundOf16Points = row[columnNumbers["round of 16 points"]] != null
                        ? (int?) Int32.Parse(row[columnNumbers["round of 16 points"]])
                        : null,
                    RoundOf16Prize = row[columnNumbers["round of 16 prize"]] != null
                        ? (int?) Int32.Parse(row[columnNumbers["round of 16 prize"]])
                        : null,
                    QuarterfinalPoints = row[columnNumbers["quarterfinal points"]] != null
                        ? (int?) Int32.Parse(row[columnNumbers["quarterfinal points"]])
                        : null,
                    QuarterfinalPrize = row[columnNumbers["quarterfinal prize"]] != null
                        ? (int?) Int32.Parse(row[columnNumbers["quarterfinal prize"]])
                        : null,
                    SemifinalPoints = row[columnNumbers["semifinal points"]] != null
                        ? (int?) Int32.Parse(row[columnNumbers["semifinal points"]])
                        : null,
                    SemifinalPrize = row[columnNumbers["semifinal prize"]] != null
                        ? (int?) Int32.Parse(row[columnNumbers["semifinal prize"]])
                        : null,
                    FinalPoints = row[columnNumbers["final points"]] != null
                        ? (int?) Int32.Parse(row[columnNumbers["final points"]])
                        : null,
                    FinalPrize = row[columnNumbers["semifinal prize"]] != null
                        ? (int?) Int32.Parse(row[columnNumbers["final prize"]])
                        : null,
                    WinnerPoints = row[columnNumbers["winner points"]] != null
                        ? (int?) Int32.Parse(row[columnNumbers["final points"]])
                        : null,
                    WinnerPrize = row[columnNumbers["winner prize"]] != null
                        ? (int?) Int32.Parse(row[columnNumbers["winner prize"]])
                        : null
                };
                
                tournamentsForAdding.Add(t);
            }
            db.Database.ExecuteSqlRaw("TRUNCATE TABLE Tournaments");    
            
            foreach (var t in tournamentsForAdding)
            {
                db.Tournaments.Add(t);
            }

            db.SaveChanges();

            return true;
        }
    }
}