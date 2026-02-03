using System;
using System.Collections.Generic;

namespace BokhandelAdminstration.Models;

public partial class Förlag
{
    public int Id { get; set; }

    public string Namn { get; set; } = null!;

    public string Land { get; set; } = null!;

    public int? LeverantörId { get; set; }

    public virtual ICollection<Böcker> Böckers { get; set; } = new List<Böcker>();

    public virtual Leverantörer? Leverantör { get; set; }
}
