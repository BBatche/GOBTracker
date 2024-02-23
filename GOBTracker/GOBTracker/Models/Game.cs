using System;
using System.Collections.Generic;

namespace GOBTracker.Models;

public partial class Game
{
    public int GameId { get; set; }

    public int MyTeamId { get; set; }

    public int OpponentTeamId { get; set; }

    public DateTime GameDateTime { get; set; }
}
