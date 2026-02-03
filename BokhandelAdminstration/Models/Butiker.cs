using System;
using System.Collections.Generic;

namespace BokhandelAdminstration.Models;

public partial class Butiker
{
    public int Id { get; set; }

    public string Butiksnamn { get; set; } = null!;

    public string? Adress { get; set; }

    public string? Postnummer { get; set; }

    public string? Ort { get; set; }

    public virtual ICollection<LagerSaldo> LagerSaldos { get; set; } = new List<LagerSaldo>();
}
