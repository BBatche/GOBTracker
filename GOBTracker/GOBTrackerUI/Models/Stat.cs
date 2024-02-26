using System;
using System.Collections.Generic;

namespace GOBTrackerUI.Models;

public partial class Stat
{
    public int Id { get; set; }

    public int PlayerTeamId { get; set; }

    public int GameId { get; set; }

    public int StatTypeId { get; set; }

    public decimal StatValue { get; set; }

}
