using System;
using System.Collections.Generic;

namespace BokhandelAdminstration.Models;

public partial class KategoriStatistik
{
    public string Kategori { get; set; } = null!;

    public string AntalTitlar { get; set; } = null!;

    public string TotaltSåldaExemplar { get; set; } = null!;

    public string TotalFörsäljning { get; set; } = null!;
}
