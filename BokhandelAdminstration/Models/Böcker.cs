using System;
using System.Collections.Generic;

namespace BokhandelAdminstration.Models;

public partial class Böcker
{
    public string Isbn13 { get; set; } = null!;

    public string Titel { get; set; } = null!;

    public string Språk { get; set; } = null!;

    public decimal Pris { get; set; }

    public DateOnly? Utgivningsdatum { get; set; }

    public int? FörlagId { get; set; }

    public int? KategoriId { get; set; }

    public virtual ICollection<Beställningsrader> Beställningsraders { get; set; } = new List<Beställningsrader>();

    public virtual Förlag? Förlag { get; set; }

    public virtual Kategorier? Kategori { get; set; }

    public virtual ICollection<LagerSaldo> LagerSaldos { get; set; } = new List<LagerSaldo>();

    public virtual ICollection<Författare> Författares { get; set; } = new List<Författare>();
}
