using System;
using System.Collections.Generic;

namespace GOBTracker.Models;

public partial class PlayerGameStat
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public decimal? Total2ptsMade { get; set; }

    public decimal? Total3ptsMade { get; set; }

    public decimal? TotalPoints { get; set; }

    public int GameId { get; set; }

    public DateTimeOffset GameDateTime { get; set; }

    public int PlayerId { get; set; }
}
