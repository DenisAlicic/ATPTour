using System;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using TennisAssociation.Models;
using TennisAssociation.Interfaces;
using TennisAssociation.Services;

namespace TennisAssociation.Controllers
{
    [Route("api/updateData")]
    [ApiController]
    public class UpdateDataController : Controller
    {
        private const string ScriptDirectory = "UpdatingScripts";
        private const string TournamentsFile = ScriptDirectory + "/tournaments.txt";
        private const string MatchesFile = ScriptDirectory + "/matches.txt";
        private const string PlayersFile = ScriptDirectory + "/players.txt";
        private const string PLayersScript = "players.py";
        private const string MatchesScript = "matches.py";
        private const string TournamentScript ="tournaments.py"; 
        private readonly TennisAssociationContext db;

        public UpdateDataController(TennisAssociationContext context)
        {
            db = context;
        }

        [HttpGet("tournaments")]
        // GET
        public bool UpdateTournaments()
        {
            IDatabaseUpdater<Tournament> service = new DatabaseUpdaterService<Tournament>(ScriptDirectory, TournamentScript, TournamentsFile, db,
                (row, columnNumbers) => new Tournament
                {
                    Tournament1 = row[columnNumbers["tournament"]],
                    _1RoundPrize = row[columnNumbers["1. round prize"]] != null
                        ? (int?) int.Parse(row[columnNumbers["1. round prize"]])
                        : null,
                    _1RoundPoints = row[columnNumbers["1. round points"]] != null
                        ? (int?) int.Parse(row[columnNumbers["1. round points"]])
                        : null,
                    _2RoundPrize = row[columnNumbers["2. round prize"]] != null
                        ? (int?) int.Parse(row[columnNumbers["2. round prize"]])
                        : null,
                    _2RoundPoints = row[columnNumbers["2. round points"]] != null
                        ? (int?) int.Parse(row[columnNumbers["2. round points"]])
                        : null,
                    _3RoundPrize = row[columnNumbers["3. round prize"]] != null
                        ? (int?) int.Parse(row[columnNumbers["3. round prize"]])
                        : null,
                    _3RountPoints = row[columnNumbers["3. rount points"]] != null
                        ? (int?) int.Parse(row[columnNumbers["3. rount points"]])
                        : null,
                    RoundOf16Prize = row[columnNumbers["round of 16 prize"]] != null
                        ? (int?) int.Parse(row[columnNumbers["round of 16 prize"]])
                        : null,
                    RoundOf16Points = row[columnNumbers["round of 16 points"]] != null
                        ? (int?) int.Parse(row[columnNumbers["round of 16 points"]])
                        : null,
                    QuarterfinalPoints = row[columnNumbers["quarterfinal points"]] != null
                        ? (int?) int.Parse(row[columnNumbers["quarterfinal points"]])
                        : null,
                    QuarterfinalPrize = row[columnNumbers["quarterfinal prize"]] != null
                        ? (int?) int.Parse(row[columnNumbers["quarterfinal prize"]])
                        : null,
                    SemifinalPoints = row[columnNumbers["semifinal points"]] != null
                        ? (int?) int.Parse(row[columnNumbers["semifinal points"]])
                        : null,
                    SemifinalPrize = row[columnNumbers["semifinal prize"]] != null
                        ? (int?) int.Parse(row[columnNumbers["semifinal prize"]])
                        : null,
                    FinalPoints = row[columnNumbers["final points"]] != null
                        ? (int?) int.Parse(row[columnNumbers["final points"]])
                        : null,
                    FinalPrize = row[columnNumbers["final prize"]] != null
                        ? (int?) int.Parse(row[columnNumbers["final prize"]])
                        : null,
                    WinnerPoints = row[columnNumbers["winner points"]] != null
                        ? (int?) int.Parse(row[columnNumbers["winner points"]])
                        : null,
                    WinnerPrize = row[columnNumbers["winner prize"]] != null
                        ? (int?) int.Parse(row[columnNumbers["winner prize"]])
                        : null
                });
            
            return service.PrepareData() && service.UpdateData(db.Tournaments, "Tournaments");
        }
        
        [HttpGet("players")]
        public bool UpdatePlayers()
        {
            IDatabaseUpdater<Player> service = new DatabaseUpdaterService<Player>(ScriptDirectory, PLayersScript, PlayersFile, db,
                (row, columnNames) =>
                {
                    Console.Out.WriteLine(row.Count);
                    var player = new Player
                    {
                        FirstName = row[columnNames["firstName"]],
                        LastName = row[columnNames["lastName"]],
                        Country = row[columnNames["country"]],
                        Sex = row[columnNames["sex"]],
                        Hand = row[columnNames["hand"]],
                        Height = row[columnNames["height"]] != null
                            ? (short?) short.Parse(row[columnNames["height"]])
                            : null,
                        Weight = row[columnNames["weight"]] != null
                            ? (short?) short.Parse(row[columnNames["weight"]])
                            : null,
                        Birth = DateTime.Parse(row[columnNames["birth"]]),
                        CurrentRankingSingle = row[columnNames["currentRankingSingle"]] != null
                            ? (short?) short.Parse(row[columnNames["currentRankingSingle"]])
                            : null,
                        CurrentRankingDouble = row[columnNames["currentRankingDouble"]] != null
                            ? (short?) short.Parse(row[columnNames["currentRankingDouble"]])
                            : null,
                        BestRankingDouble = row[columnNames["bestRankingDouble"]] != null
                            ? (short?) short.Parse(row[columnNames["bestRankingDouble"]])
                            : null,
                        BestRankingSingle = row[columnNames["bestRankingSingle"]] != null
                            ? (short?) short.Parse(row[columnNames["bestRankingSingle"]])
                            : null,
                        Img = Convert.FromBase64String(row[columnNames["img"]])
                    };
                    return player;
                });
            return service.PrepareData() && service.UpdateData(db.Players, "Players");
        }

        [HttpGet("matches")]
        public bool UpdateMatches()
        {
            IDatabaseUpdater<Match> service = new DatabaseUpdaterService<Match>(ScriptDirectory, MatchesScript, MatchesFile, db,
                (row, columnNames) => new Match
                {
                    Tournament = row[columnNames["tournament"]],
                    FirstPlayer = row[columnNames["fristPlayer"]],
                    SecondPlayer = row[columnNames["secondPlayer"]],
                    HeadToHeadFirst = row[columnNames["headToHeadFirst"]] != null
                        ? (short?) short.Parse(row[columnNames["headToHeadFirst"]])
                        : null,
                    HeadToHeadSecond = row[columnNames["headToHeadSecond"]] != null
                        ? (short?) short.Parse(row[columnNames["headToHeadSecond"]])
                        : null,
                    ResultFirst = row[columnNames["resultFirst"]] != null
                        ? (short?) short.Parse(row[columnNames["resultFirst"]])
                        : null,
                    ResultSecond = row[columnNames["resultSecond"]] != null
                        ? (short?) short.Parse(row[columnNames["resultSecond"]])
                        : null,
                    Date = DateTime.Parse(row[columnNames["date"]])
                });
            return service.PrepareData() && service.UpdateData(db.Matches, "Matches");
        }
    }
}