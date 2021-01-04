using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TennisAssociation.Models
{
    [Table("dbo.players")]
    public class Player
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string country { get; set; }
        public int height { get; set; }
        public int weight { get; set; }
        public DateTime birth { get; set; }
        public int currentRankingSingle { get; set; }
        public int bestRankingSingle { get; set; }
        public int currentRankingDouble { get; set; }
        public int bestRankingDouble { get; set; }
        public string sex { get; set; }
        public string hand { get; set; }
        byte[] img { get; set; }

    }
}
