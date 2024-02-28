using System;
using System.Collections.Generic;

namespace GOBTracker.Models;

public partial class Stat
{
    public int Id { get; set; }

    public int PlayerTeamId { get; set; }

    public int GameId { get; set; }

    public int StatTypeId { get; set; }

    public decimal StatValue { get; set; }

    public virtual Game Game { get; set; } = null!;

    public virtual PlayerTeam PlayerTeam { get; set; } = null!;

    public virtual StatType StatType { get; set; } = null!;
}
