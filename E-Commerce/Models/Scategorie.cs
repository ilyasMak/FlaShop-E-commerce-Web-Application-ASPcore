namespace E_Commerce.Models;

public partial class Scategorie
{
    public int IdScategorie { get; set; }

    public string? NomScategorie { get; set; }

    public string? DescScategorie { get; set; }

    public int IdCategorie { get; set; }

    public virtual Categorie IdCategorieNavigation { get; set; } = null!;

    public virtual ICollection<Produit> Produits { get; set; } = new List<Produit>();
}
