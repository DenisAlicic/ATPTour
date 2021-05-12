using System;
using System.Collections.Generic;

#nullable disable

namespace TennisAssociation.Models
{
    /// <summary>
    /// Model according database table.
    /// </summary>
    public partial class Player
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public short? Height { get; set; }
        public short? Weight { get; set; }
        public DateTime? Birth { get; set; }
        public short? CurrentRankingSingle { get; set; }
        public short? BestRankingSingle { get; set; }
        public short? CurrentRankingDouble { get; set; }
        public short? BestRankingDouble { get; set; }
        public string Sex { get; set; }
        public string Hand { get; set; }
        public byte[] Img { get; set; }
    }
}
