using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Models;

public partial class Proprietaire
{
    [Key]
    public int IdProprietaire { get; set; }

    public string? NomProprietaire { get; set; }

    public string? PrenomProprietaire { get; set; }

    public string? EmailProprietaire { get; set; }

    public string? MotPassProprietaire { get; set; }

    public string? ActiviteProprietaire { get; set; }

    public string? StatusProprietaire { get; set; }

    public virtual ICollection<Produit> Produits { get; set; } = new List<Produit>();
}
