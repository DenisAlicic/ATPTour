using System;
using System.Collections.Generic;

#nullable disable

namespace TennisAssociation.Models
{
    public partial class Tournament
    {
        public string Tournament1 { get; set; }
        public int? _1RoundPrize { get; set; }
        public int? _1RoundPoints { get; set; }
        public int? _2RoundPrize { get; set; }
        public int? _2RoundPoints { get; set; }
        public int? _3RoundPrize { get; set; }
        public int? _3RountPoints { get; set; }
        public int? RoundOf16Prize { get; set; }
        public int? RoundOf16Points { get; set; }
        public int? QuarterfinalPrize { get; set; }
        public int? QuarterfinalPoints { get; set; }
        public int? SemifinalPrize { get; set; }
        public int? SemifinalPoints { get; set; }
        public int? FinalPrize { get; set; }
        public int? FinalPoints { get; set; }
        public int? WinnerPrize { get; set; }
        public int? WinnerPoints { get; set; }
    }
}
