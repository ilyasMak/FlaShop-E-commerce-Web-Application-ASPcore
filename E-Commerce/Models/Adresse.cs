namespace E_Commerce.Models;

public partial class Adresse
{
    public int IdAdresse { get; set; }

    public int? NumMaison { get; set; }

    public string? Rue { get; set; }

    public string? Ville { get; set; }

    public string? Pays { get; set; }

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();
}
