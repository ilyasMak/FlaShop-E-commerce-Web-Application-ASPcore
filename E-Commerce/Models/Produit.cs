namespace E_Commerce.Models;

public partial class Produit
{
    public int IdProduit { get; set; }

    public string? NomProduit { get; set; }

    public string? DescProduit { get; set; }

    public int? Qte { get; set; }

    public int IdScategorie { get; set; }

    public int IdProprietaire { get; set; }

    public double PrixUnitaire { get; set; }

    public int PourcentageSolde { get; set; }

    public virtual ICollection<Evaluation> Evaluations { get; set; } = new List<Evaluation>();

    public virtual Proprietaire IdProprietaireNavigation { get; set; } = null!;

    public virtual Scategorie IdScategorieNavigation { get; set; } = null!;

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    public virtual ICollection<Panier> Paniers { get; set; } = new List<Panier>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
