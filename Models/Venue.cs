﻿using System;
using System.Collections.Generic;

namespace endavaPractica.Net.Models;

public partial class Venue
{
    public int VenueId { get; set; }

    public string? VenueLocation { get; set; }

    public string? VenueType { get; set; }

    public int? Capacity { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
