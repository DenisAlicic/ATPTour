using System;
using System.Collections.Generic;

#nullable disable

namespace TennisAssociation.Models
{
    public partial class Match
    {
        public Guid Id { get; set; }
        public string Tournament { get; set; }
        public string FirstPlayer { get; set; }
        public string SecondPlayer { get; set; }
        public short? HeadToHeadFirst { get; set; }
        public short? HeadToHeadSecond { get; set; }
        public short? ResultFirst { get; set; }
        public short? ResultSecond { get; set; }
        public DateTime Date { get; set; }
    }
}
