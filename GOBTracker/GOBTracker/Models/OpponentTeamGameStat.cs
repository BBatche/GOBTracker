using System;
using System.Collections.Generic;

namespace GOBTracker.Models;

public partial class OpponentTeamGameStat
{
    public string? TeamName { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public decimal? TotalPoints { get; set; }

    public decimal? TotalTwoPmade { get; set; }

    public decimal? TotalTwoPmiss { get; set; }

    public decimal? TotalThreePmade { get; set; }

    public decimal? TotalThreePmiss { get; set; }

    public decimal? TotalSteals { get; set; }

    public decimal? TotalTurnovers { get; set; }

    public decimal? TotalAssists { get; set; }

    public decimal? TotalBlocks { get; set; }

    public decimal? TotalFouls { get; set; }

    public decimal? TotalOffRebounds { get; set; }

    public decimal? TotalDefRebounds { get; set; }

    public DateTimeOffset GameDateTime { get; set; }

    public int GameId { get; set; }
}
