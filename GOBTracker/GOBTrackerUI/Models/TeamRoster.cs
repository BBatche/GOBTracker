using System;
using System.Collections.Generic;

namespace GOBTrackerUI.Models;

public partial class TeamRoster
{
    public int Id { get; set; }

    public int PlayerId { get; set; }

    public int TeamId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string TeamName { get; set; } = null!;
}
