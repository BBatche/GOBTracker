using System;
using System.Collections.Generic;

namespace GOBTrackerUI.Models;

public partial class AllRawStat
{
    public int PlayerTeamId { get; set; }

    public int GameId { get; set; }

    public int StatTypeId { get; set; }

    public decimal StatValue { get; set; }

    public string StatName { get; set; } = null!;

    public string StatNameAbr { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;
}
