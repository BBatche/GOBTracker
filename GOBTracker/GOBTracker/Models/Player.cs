﻿using System;
using System.Collections.Generic;

namespace GOBTracker.Models;

public partial class Player
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int? IsDeleted { get; set; }
}
