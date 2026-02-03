using System;
using System.Collections.Generic;

namespace BokhandelAdminstration.Models;

public partial class Leverantörer
{
    public int Id { get; set; }

    public string Namn { get; set; } = null!;

    public string? Kontaktperson { get; set; }

    public string? Epost { get; set; }

    public string? Telefon { get; set; }

    public string? Adress { get; set; }

    public string? Postnummer { get; set; }

    public string? Ort { get; set; }

    public virtual ICollection<Förlag> Förlags { get; set; } = new List<Förlag>();
}
