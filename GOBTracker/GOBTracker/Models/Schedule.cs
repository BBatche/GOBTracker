using System;
using System.Collections.Generic;

namespace GOBTracker.Models;

public partial class Schedule
{
    public string OurTeam { get; set; } = null!;

    public string Opponent { get; set; } = null!;

    public DateTimeOffset GameDateTime { get; set; }

    public string? Location { get; set; }

    public int OurTeamId { get; set; }

    public int OpponentTeamId { get; set; }

    public int GameId { get; set; }
}
