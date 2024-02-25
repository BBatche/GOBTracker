using System;
using System.Collections.Generic;

namespace GOBTracker.Models;

public partial class Game
{
    public int Id { get; set; }

    public int OurTeamId { get; set; }

    public int OpponentTeamId { get; set; }

    public string? Location { get; set; }

    public DateTimeOffset GameDateTime { get; set; }

}
