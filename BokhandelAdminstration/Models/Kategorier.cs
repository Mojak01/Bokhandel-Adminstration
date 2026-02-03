using System;
using System.Collections.Generic;

namespace BokhandelAdminstration.Models;

public partial class Kategorier
{
    public int Id { get; set; }

    public string KategoriNamn { get; set; } = null!;

    public virtual ICollection<Böcker> Böckers { get; set; } = new List<Böcker>();
}
