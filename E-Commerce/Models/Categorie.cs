namespace E_Commerce.Models;

public partial class Categorie
{
    public int IdCategorie { get; set; }

    public string? NomCategorie { get; set; }

    public string? DescCategorie { get; set; }

    public virtual ICollection<Scategorie> Scategories { get; set; } = new List<Scategorie>();
}
