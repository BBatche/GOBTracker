using System;
using System.Collections.Generic;

namespace GOBTrackerUI.Models;

public partial class Schedule
{
    public string OurTeam { get; set; } = null!;

    public string Opponent { get; set; } = null!;

    public DateTimeOffset GameDateTime { get; set; }

    public string? Location { get; set; }
}
