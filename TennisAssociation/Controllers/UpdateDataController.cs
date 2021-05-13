using System;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using TennisAssociation.Models;
using TennisAssociation.Interfaces;
using TennisAssociation.Services;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using TennisAssociation.Utils;

namespace TennisAssociation.Controllers
{
    /// <summary>
    /// Controller for admin to update each table in database.
    /// </summary>
    [Route("api/updateData")]
    [ApiController]
    [Authorize(Roles="Admin")]
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
        private EmailSender _emailSenderSevice;
        private UserManager<MyUser> userManager;

        public UpdateDataController(TennisAssociationContext context, UserManager<MyUser> userManager)
        {
            db = context;
            this.userManager = userManager;
            _emailSenderSevice = new EmailSender();
        }
        /// GET api/updateData/tournaments
        /// <summary>
        /// Update database table of tournaments.
        /// </summary>
        /// <returns></returns>
        [HttpGet("tournaments")]
        public bool UpdateTournaments()
        {
            IDatabaseUpdater<Tournament> service = new DatabaseUpdaterService<Tournament>(ScriptDirectory,
                TournamentScript, TournamentsFile, db,
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

            if (service.PrepareData() && service.UpdateData(db.Tournaments, "Tournaments"))
            {
                var users = userManager.Users;
                var mail = new Email
                {
                    Body = "Tournaments data in TennisAssocation app are changed!\n \nYour admin team!",
                    Subject = "Data changed"
                };
                foreach (var user in users)
                {
                    _emailSenderSevice.Send(user.Email, mail);
                }

                return true;
            }

            return false;
        }
        
        /// GET api/updateData/players
        /// <summary>
        /// Update database table of players.
        /// </summary>
        /// <returns></returns>
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
                        Birth = DateTime.ParseExact(row[columnNames["birth"]], "MM/dd/yyyy", CultureInfo.InvariantCulture),
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
            
            if (service.PrepareData() && service.UpdateData(db.Players, "Players"))
            {
                var users = userManager.Users;
                var mail = new Email
                {
                    Body = "Players data in TennisAssocation app are changed!\n \nYour admin team!",
                    Subject = "Data changed"
                };
                foreach (var user in users)
                {
                    _emailSenderSevice.Send(user.Email, mail);
                }

                return true;
            }

            return false;
        }

        /// GET api/updateData/matches
        /// <summary>
        /// Update database table of matches.
        /// </summary>
        /// <returns></returns>
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
                    Date = DateTime.ParseExact(row[columnNames["date"]], "MM/dd/yyyy", CultureInfo.InvariantCulture)
                });
            
            if (service.PrepareData() && service.UpdateData(db.Matches, "Matches"))
            {
                var users = userManager.Users;
                var mail = new Email
                {
                    Body = "Matches data in TennisAssocation app are changed!\n \nYour admin team!",
                    Subject = "Data changed"
                };
                foreach (var user in users)
                {
                    _emailSenderSevice.Send(user.Email, mail);
                }

                return true;
            }
            
            return false;
        }
    }
}