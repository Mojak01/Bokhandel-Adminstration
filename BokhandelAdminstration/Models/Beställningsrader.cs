using System;
using System.Collections.Generic;

namespace BokhandelAdminstration.Models;

public partial class Beställningsrader
{
    public string BeställningId { get; set; } = null!;

    public string Isbn13 { get; set; } = null!;

    public int Antal { get; set; }

    public decimal PrisVidKöp { get; set; }

    public virtual Beställningar Beställning { get; set; } = null!;

    public virtual Böcker Isbn13Navigation { get; set; } = null!;
}
