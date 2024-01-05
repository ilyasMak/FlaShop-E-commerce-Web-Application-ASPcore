using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Models;

public partial class CUsersPcDesktopProjetsECommerceEcoleMdfContext : DbContext
{
    public CUsersPcDesktopProjetsECommerceEcoleMdfContext()
    {
    }

    public CUsersPcDesktopProjetsECommerceEcoleMdfContext(DbContextOptions<CUsersPcDesktopProjetsECommerceEcoleMdfContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Adresse> Adresses { get; set; }

    public virtual DbSet<Categorie> Categories { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Evaluation> Evaluations { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Panier> Paniers { get; set; }

    public virtual DbSet<Produit> Produits { get; set; }

    public virtual DbSet<Proprietaire> Proprietaires { get; set; }

    public virtual DbSet<Scategorie> Scategories { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Adresse>(entity =>
        {
            entity.HasKey(e => e.IdAdresse).HasName("PK__adresse__5C886CD37F9E7687");

            entity.ToTable("adresse");

            entity.Property(e => e.IdAdresse)
                .ValueGeneratedNever()
                .HasColumnName("Id_Adresse");
            entity.Property(e => e.NumMaison).HasColumnName("Num_Maison");
            entity.Property(e => e.Pays)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.Rue)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.Ville)
                .HasMaxLength(100)
                .IsFixedLength();
        });

        modelBuilder.Entity<Categorie>(entity =>
        {
            entity.HasKey(e => e.IdCategorie).HasName("PK__categori__CB903345ECE4FA3F");

            entity.ToTable("categorie");

            entity.Property(e => e.IdCategorie)
                .ValueGeneratedNever()
                .HasColumnName("Id_Categorie");
            entity.Property(e => e.DescCategorie)
                .HasMaxLength(300)
                .IsFixedLength()
                .HasColumnName("Desc_Categorie");
            entity.Property(e => e.NomCategorie)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("Nom_Categorie");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.IdClient).HasName("PK__client__668DFF3F4EFCD3E2");

            entity.ToTable("client");

            entity.Property(e => e.IdClient)
                .ValueGeneratedNever()
                .HasColumnName("Id_Client");
            entity.Property(e => e.EmailClient)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("Email_Client");
            entity.Property(e => e.IdAdresse).HasColumnName("Id_Adresse");
            entity.Property(e => e.MotPassClient)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("MotPass_Client");
            entity.Property(e => e.NomClient)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("Nom_Client");
            entity.Property(e => e.PrenomClient)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("Prenom_Client");
            entity.Property(e => e.StatusClient)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("Status_Client");

            entity.HasOne(d => d.IdAdresseNavigation).WithMany(p => p.Clients)
                .HasForeignKey(d => d.IdAdresse)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cnstrnt_adresse");
        });

        modelBuilder.Entity<Evaluation>(entity =>
        {
            entity.HasKey(e => e.IdEvaluation).HasName("PK__evaluati__C635C65D14168385");

            entity.ToTable("evaluation");

            entity.Property(e => e.IdEvaluation)
                .ValueGeneratedNever()
                .HasColumnName("Id_Evaluation");
            entity.Property(e => e.IdClient).HasColumnName("Id_Client");
            entity.Property(e => e.IdProduit).HasColumnName("Id_Produit");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.Evaluations)
                .HasForeignKey(d => d.IdClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cnstrnt_client3");

            entity.HasOne(d => d.IdProduitNavigation).WithMany(p => p.Evaluations)
                .HasForeignKey(d => d.IdProduit)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cnstrnt_produit4");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.IdImage).HasName("PK__images__DF48C8D553F8E2A6");

            entity.ToTable("images");

            entity.Property(e => e.IdImage)
                .ValueGeneratedNever()
                .HasColumnName("Id_Image");
            entity.Property(e => e.IdProduit).HasColumnName("Id_Produit");
            entity.Property(e => e.Image1)
                .HasColumnType("image")
                .HasColumnName("Image");

            entity.HasOne(d => d.IdProduitNavigation).WithMany(p => p.Images)
                .HasForeignKey(d => d.IdProduit)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cnstrnt_produit");
        });

        modelBuilder.Entity<Panier>(entity =>
        {
            entity.HasKey(e => e.IdPanier).HasName("PK__panier__BEEB40DBEF360A56");

            entity.ToTable("panier");

            entity.Property(e => e.IdPanier)
                .ValueGeneratedNever()
                .HasColumnName("Id_Panier");
            entity.Property(e => e.IdClient).HasColumnName("Id_Client");
            entity.Property(e => e.IdProduit).HasColumnName("Id_Produit");
            entity.Property(e => e.QteProduit).HasColumnName("Qte_Produit");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.Paniers)
                .HasForeignKey(d => d.IdClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cnstrnt_client");

            entity.HasOne(d => d.IdProduitNavigation).WithMany(p => p.Paniers)
                .HasForeignKey(d => d.IdProduit)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cnstrnt_produit2");
        });

        modelBuilder.Entity<Produit>(entity =>
        {
            entity.HasKey(e => e.IdProduit).HasName("PK__produit__6B1AEE330078159D");

            entity.ToTable("produit");

            entity.Property(e => e.IdProduit)
                .ValueGeneratedNever()
                .HasColumnName("Id_Produit");
            entity.Property(e => e.DescProduit)
                .HasMaxLength(300)
                .IsFixedLength()
                .HasColumnName("Desc_Produit");
            entity.Property(e => e.IdProprietaire).HasColumnName("Id_Proprietaire");
            entity.Property(e => e.IdScategorie).HasColumnName("Id_Scategorie");
            entity.Property(e => e.NomProduit)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("Nom_Produit");
            entity.Property(e => e.PourcentageSolde).HasColumnName("Pourcentage_Solde");
            entity.Property(e => e.PrixUnitaire).HasColumnName("Prix_Unitaire");

            entity.HasOne(d => d.IdProprietaireNavigation).WithMany(p => p.Produits)
                .HasForeignKey(d => d.IdProprietaire)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cnstrnt_proprietaire");

            entity.HasOne(d => d.IdScategorieNavigation).WithMany(p => p.Produits)
                .HasForeignKey(d => d.IdScategorie)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cnstrnt_scategorie");
        });

        modelBuilder.Entity<Proprietaire>(entity =>
        {
            entity.HasKey(e => e.IdProprietaire).HasName("PK__propriet__4974183EBAACACA9");

            entity.ToTable("proprietaire");

            entity.Property(e => e.IdProprietaire)
                .ValueGeneratedNever()
                .HasColumnName("Id_Proprietaire");
            entity.Property(e => e.ActiviteProprietaire)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("Activite_Proprietaire");
            entity.Property(e => e.EmailProprietaire)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("Email_Proprietaire");
            entity.Property(e => e.MotPassProprietaire)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("MotPass_Proprietaire");
            entity.Property(e => e.NomProprietaire)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("Nom_Proprietaire");
            entity.Property(e => e.PrenomProprietaire)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("Prenom_Proprietaire");
            entity.Property(e => e.StatusProprietaire)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("Status_Proprietaire");
        });

        modelBuilder.Entity<Scategorie>(entity =>
        {
            entity.HasKey(e => e.IdScategorie).HasName("PK__scategor__40AD12FFB6D678D8");

            entity.ToTable("scategorie");

            entity.Property(e => e.IdScategorie)
                .ValueGeneratedNever()
                .HasColumnName("Id_Scategorie");
            entity.Property(e => e.DescScategorie)
                .HasMaxLength(300)
                .IsFixedLength()
                .HasColumnName("Desc_Scategorie");
            entity.Property(e => e.IdCategorie).HasColumnName("Id_Categorie");
            entity.Property(e => e.NomScategorie)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("Nom_Scategorie");

            entity.HasOne(d => d.IdCategorieNavigation).WithMany(p => p.Scategories)
                .HasForeignKey(d => d.IdCategorie)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cnstrnt_Categorie");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.NumTransaction).HasName("PK__transact__17147CED6DA9B102");

            entity.ToTable("transaction");

            entity.Property(e => e.NumTransaction)
                .ValueGeneratedNever()
                .HasColumnName("Num_Transaction");
            entity.Property(e => e.DateTransaction).HasColumnName("Date_Transaction");
            entity.Property(e => e.Delivre)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.IdClient).HasColumnName("Id_Client");
            entity.Property(e => e.IdProduit).HasColumnName("Id_Produit");
            entity.Property(e => e.PrixTransaction).HasColumnName("Prix_Transaction");
            entity.Property(e => e.QteProduit).HasColumnName("Qte_Produit");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.IdClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cnstrnt_client2");

            entity.HasOne(d => d.IdProduitNavigation).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.IdProduit)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cnstrnt_produit3");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
