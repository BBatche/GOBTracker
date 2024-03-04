using System;
using System.Collections.Generic;

namespace GOBTracker.Models;

public partial class TeamGameScore
{
    public int GameId { get; set; }

    public int OurTeamId { get; set; }

    public string OurTeamName { get; set; } = null!;

    public int OpponentTeamId { get; set; }

    public string OpponentTeamName { get; set; } = null!;

    public DateTimeOffset GameDateTime { get; set; }

    public decimal? OurTeamPoints { get; set; }

    public decimal? OpponentTeamPoints { get; set; }

    public decimal? TotalPoints { get; set; }
}
