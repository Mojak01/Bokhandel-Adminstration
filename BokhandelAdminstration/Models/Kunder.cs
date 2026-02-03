using System;
using System.Collections.Generic;

namespace BokhandelAdminstration.Models;

public partial class Kunder
{
    public int KundId { get; set; }

    public string? KundNummer { get; set; }

    public string Förnamn { get; set; } = null!;

    public string Efternamn { get; set; } = null!;

    public string? Mail { get; set; }

    public virtual ICollection<Beställningar> Beställningars { get; set; } = new List<Beställningar>();
}
