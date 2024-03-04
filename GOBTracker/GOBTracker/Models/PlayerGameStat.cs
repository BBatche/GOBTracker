using System;
using System.Collections.Generic;

namespace GOBTracker.Models;

public partial class PlayerGameStat
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public decimal? Total2ptsMade { get; set; }

    public decimal? Total2ptsMissed { get; set; }

    public decimal? Total3ptsMade { get; set; }

    public decimal? Total3ptsMissed { get; set; }

    public decimal? TotalSteals { get; set; }

    public decimal? TotalTurnovers { get; set; }

    public decimal? TotalAssists { get; set; }

    public decimal? TotalBlocks { get; set; }

    public decimal? TotalFouls { get; set; }

    public decimal? TotalOffensiveRebounds { get; set; }

    public decimal? TotalDefensiveRebounds { get; set; }

    public decimal? TotalPoints { get; set; }

    public int GameId { get; set; }

    public DateTimeOffset GameDateTime { get; set; }

    public int PlayerId { get; set; }
}
