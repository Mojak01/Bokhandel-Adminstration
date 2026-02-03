using System;
using System.Collections.Generic;

namespace BokhandelAdminstration.Models;

public partial class Beställningar
{
    public string BeställningId { get; set; } = null!;

    public int KundId { get; set; }

    public DateOnly Datum { get; set; }

    public virtual ICollection<Beställningsrader> Beställningsraders { get; set; } = new List<Beställningsrader>();

    public virtual Kunder Kund { get; set; } = null!;
}
