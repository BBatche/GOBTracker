using System;
using System.Collections.Generic;

namespace GOBTracker.Models;

public partial class PlayerTeam
{
    public int Id { get; set; }

    public int PlayerId { get; set; }

    public int TeamId { get; set; }

    public virtual Player Player { get; set; } = null!;

    public virtual ICollection<Stat> Stats { get; set; } = new List<Stat>();

    public virtual Team Team { get; set; } = null!;
}
