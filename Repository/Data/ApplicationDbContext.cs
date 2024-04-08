using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Domain.Models;

namespace Repository.Data
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Arborescence> Arborescence { get; set; } = null!;
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; } = null!;
        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; } = null!;
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; } = null!;
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; } = null!;
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; } = null!;
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; } = null!;
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; } = null!;
        public virtual DbSet<Categorie_Client> Categorie_Client { get; set; } = null!;
        public virtual DbSet<Client> Client { get; set; } = null!;
        public virtual DbSet<Collaborateur> Collaborateur { get; set; } = null!;
        public virtual DbSet<Etat_Stock> Etat_Stock { get; set; } = null!;
        public virtual DbSet<Evenement_Type> Evenement_Type { get; set; } = null!;
        public virtual DbSet<Marque> Marque { get; set; } = null!;
        public virtual DbSet<Merchandising> Merchandising { get; set; } = null!;
        public virtual DbSet<Merchandising_Photo> Merchandising_Photo { get; set; } = null!;
        public virtual DbSet<Merchandising_Type> Merchandising_Type { get; set; } = null!;
        public virtual DbSet<Nature_Vitrine> Nature_Vitrine { get; set; } = null!;
        public virtual DbSet<Nouveaute> Nouveaute { get; set; } = null!;
        public virtual DbSet<Objet_Visite> Objet_Visite { get; set; } = null!;
        public virtual DbSet<PlanningVisite> PlanningVisite { get; set; } = null!;
        public virtual DbSet<Produit> Produit { get; set; } = null!;
        public virtual DbSet<Secteur> Secteur { get; set; } = null!;
        public virtual DbSet<SellOut> SellOut { get; set; } = null!;
        public virtual DbSet<Statut> Statut { get; set; } = null!;
        public virtual DbSet<Statut_Produit> Statut_Produit { get; set; } = null!;
        public virtual DbSet<Type_Animation> Type_Animation { get; set; } = null!;
        public virtual DbSet<Type_Produit> Type_Produit { get; set; } = null!;
        public virtual DbSet<Veille_Concurrentielle> Veille_Concurrentielle { get; set; } = null!;
        public virtual DbSet<Ville> Ville { get; set; } = null!;
        public virtual DbSet<Visibilite> Visibilite { get; set; } = null!;
        public virtual DbSet<Visibilite_Photo> Visibilite_Photo { get; set; } = null!;
        public virtual DbSet<Visite> Visite { get; set; } = null!;
        public virtual DbSet<VO> VO { get; set; } = null!;
        public virtual DbSet<DotationDistribues> DotationDistribues { get; set; } = null!;
        public virtual DbSet<PrdVentesParMarque> PrdVentesParMarque { get; set; } = null!;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=alexsyssql.database.windows.net;Database=CRMSHOP_DB;User ID=srvadmin;Password=P@ssw0rd2023***;Integrated Security=false;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("French_CI_AS");

            modelBuilder.Entity<Arborescence>(entity =>
            {
                entity.HasNoKey();

               
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
             
            });

            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasNoKey();

               
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasNoKey();

            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasNoKey();

                
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasNoKey();

               

                entity.HasOne(d => d.Role)
                    .WithMany()
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AspNetUserRoles_AspNetRoles");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AspNetUserRoles_AspNetUsers");
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.LoginProvider).HasMaxLength(450);

                entity.Property(e => e.Name).HasMaxLength(450);

                entity.Property(e => e.UserId).HasMaxLength(450);
            });

            modelBuilder.Entity<Categorie_Client>(entity =>
            {
                entity.HasKey(e => e.CategorieClient_Id).HasName("PK_Categorie_Client");

            });

           

            modelBuilder.Entity<Collaborateur>(entity =>
            {
                entity.HasKey(e => e.Collaborateur_Id).HasName("PK_Collaborateur");

                entity.HasOne(d => d.CollaborateurUser)
                    .WithMany(p => p.Collaborateurs)
                    .HasForeignKey(d => d.Collaborateur_UserId)
                    .HasConstraintName("FK_Collaborateur_AspNetUsers");

                entity.HasOne(d => d.CollaborateurVille)
                    .WithMany(p => p.Collaborateurs)
                    .HasForeignKey(d => d.Collaborateur_VilleId)
                    .HasConstraintName("FK_Collaborateur_Ville");
            });

            modelBuilder.Entity<Etat_Stock>(entity =>
            {
                entity.HasKey(e => e.EtatStock_Id).HasName("PK_Etat_Stock");

                entity.HasOne(d => d.EtatStockStatut)
                    .WithMany(p => p.EtatStocks)
                    .HasForeignKey(d => d.EtatStock_StatutId)
                    .HasConstraintName("FK_Etat_Stock_Statut_Produit");

                entity.HasOne(d => d.EtatStockVisite)
                    .WithMany(p => p.EtatStocks)
                    .HasForeignKey(d => d.EtatStock_VisiteId)
                    .HasConstraintName("FK_Etat_Stock_Visite");
            });

            modelBuilder.Entity<Evenement_Type>(entity =>
            {
                entity.HasKey(e => e.EvenementType_Id).HasName("PK_Evenement_Type");

            });

            modelBuilder.Entity<Marque>(entity =>
            {
                entity.HasKey(e => e.Marque_Id).HasName("PK_Marque");

            });

            modelBuilder.Entity<Merchandising>(entity =>
            {
                entity.HasKey(e => e.Merchandising_Id).HasName("PK_Merchandising");
                

                entity.Property(e => e.Merchandising_Id).HasColumnName("Merchandising_Id");

                entity.Property(e => e.Merchandising_MarqueId).HasColumnName("Merchandising_MarqueId");

                entity.Property(e => e.Merchandising_TypeId).HasColumnName("Merchandising_TypeId");

                entity.Property(e => e.Merchandising_VisiteId).HasColumnName("Merchandising_VisiteId");

                entity.HasOne(d => d.MerchandisingMarque)
                    .WithMany(p => p.Merchandisings)
                    .HasForeignKey(d => d.Merchandising_MarqueId)
                    .HasConstraintName("FK_Merchandising_Marque");

                entity.HasOne(d => d.MerchandisingType)
                    .WithMany(p => p.Merchandisings)
                    .HasForeignKey(d => d.Merchandising_TypeId)
                    .HasConstraintName("FK_Merchandising_Merchandising_Type");

                entity.HasOne(d => d.MerchandisingVisite)
                    .WithMany(p => p.Merchandisings)
                    .HasForeignKey(d => d.Merchandising_VisiteId)
                    .HasConstraintName("FK_Merchandising_Visite");
            });

            modelBuilder.Entity<Merchandising_Photo>(entity =>
            {
                entity.HasKey(e => e.MerchandisingPhoto_Id).HasName("PK_Merchandising_Photo");
                entity.HasOne(d => d.MerchandisingPhotoMerchandising)
                    .WithMany(p => p.MerchandisingPhotos)
                    .HasForeignKey(d => d.MerchandisingPhoto_MerchandisingId)
                    .HasConstraintName("FK_Merchandising_Photo_Merchandising");

            });

            modelBuilder.Entity<Merchandising_Type>(entity =>
            {
                entity.HasKey(e => e.MerchandisingType_Id).HasName("PK_Merchandising_Type");

            });

            modelBuilder.Entity<Nature_Vitrine>(entity =>
            {
                entity.HasKey(e => e.NatureVitrine_Id).HasName("PK_Nature_Vitrine");

            });

            modelBuilder.Entity<Nouveaute>(entity =>
            {
                entity.HasKey(e => e.Nouveaute_Id).HasName("PK_Nouveaute");


                entity.HasOne(d => d.NouveauteMarque)
                    .WithMany(p => p.Nouveautes)
                    .HasForeignKey(d => d.Nouveaute_MarqueId)
                    .HasConstraintName("FK_Nouveaute_Marque");

                entity.HasOne(d => d.NouveauteVisite)
                    .WithMany(p => p.Nouveautes)
                    .HasForeignKey(d => d.Nouveaute_VisiteId)
                    .HasConstraintName("FK_Nouveaute_Visite");
            });

            modelBuilder.Entity<Objet_Visite>(entity =>
            {
                entity.HasKey(e => e.ObjetVisite_Id).HasName("PK_Objet_Visite");

            });

            modelBuilder.Entity<PlanningVisite>(entity =>
            {
                entity.HasKey(e => e.PlanningVisite_Id).HasName("PK_PlanningVisite");


                entity.HasOne(d => d.planningVisiteClient)
                    .WithMany(p => p.planningVisites)
                    .HasForeignKey(d => d.PlanningVisite_ClientId)
                    .HasConstraintName("FK_PlanningVisite_Client");

                entity.HasOne(d => d.PlanningVisiteCollaborateur)
                    .WithMany(p => p.PlanningVisites)
                    .HasForeignKey(d => d.PlanningVisite_CollaborateurId)
                    .HasConstraintName("FK_PlanningVisite_Collaborateur");
                entity.HasOne(d => d.user)
                   .WithMany(p => p.PlanningVisites)
                   .HasForeignKey(d => d.PlanningVisite_userId)
                   .HasConstraintName("FK_PlanningVisite_AspNetUsers");
            });
            modelBuilder.Entity<Client>(entity =>
            {


                entity.HasKey(e => e.Client_Id).HasName("PK_Client");


                entity.HasOne(d => d.ClientCatégorie)
                    .WithMany(p => p.Clients)
                    .HasForeignKey(d => d.Client_CatégorieId)
                    .HasConstraintName("FK_Client_Categorie_Client");

                entity.HasOne(d => d.ClientStatut)
                    .WithMany(p => p.Clients)
                    .HasForeignKey(d => d.Client_StatutId)
                    .HasConstraintName("FK_Client_Statut");

                entity.HasOne(d => d.ClientVille)
                    .WithMany(p => p.Clients)
                    .HasForeignKey(d => d.Client_VilleId)
                    .HasConstraintName("FK_Client_Ville");
            });

            modelBuilder.Entity<Produit>(entity =>
            {
                entity.HasKey(e => e.Produit_Id).HasName("PK_Produit");

                entity.HasOne(d => d.ProduitMarque)
                    .WithMany(p => p.Produits)
                    .HasForeignKey(d => d.Produit_MarqueId)
                    .HasConstraintName("FK_Produit_Marque");

                entity.HasOne(d => d.ProduitType)
                    .WithMany(p => p.Produits)
                    .HasForeignKey(d => d.Produit_TypeId)
                    .HasConstraintName("FK_Produit_Type_Produit");
            });

            modelBuilder.Entity<Secteur>(entity =>
            {
                entity.HasNoKey();

                
            });

            modelBuilder.Entity<SellOut>(entity =>
            {
                entity.HasKey(e => e.SellOut_Id).HasName("PK_SellOut");


                entity.HasOne(d => d.SellOutEvenementType)
                    .WithMany(p => p.SellOuts)
                    .HasForeignKey(d => d.SellOut_EvenementTypeId)
                    .HasConstraintName("FK_SellOut_Evenement_Type");

                entity.HasOne(d => d.SellOutVisite)
                    .WithMany(p => p.SellOuts)
                    .HasForeignKey(d => d.SellOut_VisiteId)
                    .HasConstraintName("FK_SellOut_Visite");
            });

            modelBuilder.Entity<Statut>(entity =>
            {
                entity.HasKey(e => e.Statut_Id).HasName("PK_Statut");

            });

            modelBuilder.Entity<Statut_Produit>(entity =>
            {
                entity.HasKey(e => e.StatutProduit_Id).HasName("PK_Statut_Produit");

            });

            modelBuilder.Entity<Type_Animation>(entity =>
            {
                entity.HasKey(e => e.TypeAnimation_Id).HasName("PK_Type_Animation");

            });

            modelBuilder.Entity<Type_Produit>(entity =>
            {
                entity.HasKey(e => e.TypeProduit_Id).HasName("PK_Type_Produit");

            });

            modelBuilder.Entity<Veille_Concurrentielle>(entity =>
            {
                entity.HasKey(e => e.VeilleConcurrentielle_Id).HasName("PK_Veille_Concurrentielle");


                entity.HasOne(d => d.VeilleConcurrentielleVisite)
                    .WithMany(p => p.VeilleConcurrentielles)
                    .HasForeignKey(d => d.VeilleConcurrentielle_VisiteId)
                    .HasConstraintName("FK_Veille_Concurrentielle_Visite");
            });

            modelBuilder.Entity<Ville>(entity =>
            {
                entity.HasKey(e => e.Ville_Id).HasName("PK_Ville");

            });

            modelBuilder.Entity<Visibilite>(entity =>
            {
                entity.HasKey(e => e.Visibilite_Id).HasName("PK_Visibilite");
                entity.ToTable("Visibilite");
                entity.HasOne(d => d.VisibiliteNatureIdVitrineNavigation)
                    .WithMany(p => p.Visibilites)
                    .HasForeignKey(d => d.Visibilite_NatureIdVitrine)
                    .HasConstraintName("FK_Visibilite_Nature_Vitrine");

                entity.HasOne(d => d.VisibiliteVisite)
                    .WithMany(p => p.Visibilites)
                    .HasForeignKey(d => d.Visibilite_VisiteId)
                    .HasConstraintName("FK_Visibilite_Visite");
            });

            modelBuilder.Entity<Visibilite_Photo>(entity =>
            {
                entity.HasKey(e => e.VisibilitePhoto_Id).HasName("PK_Visibilite_Photo");


                entity.HasOne(d => d.VisibilitePhotoVisibilite)
                    .WithMany(p => p.VisibilitePhotos)
                    .HasForeignKey(d => d.VisibilitePhoto_VisibiliteId)
                    .HasConstraintName("FK_Visibilite_Photo_Visibilite");
            });

            modelBuilder.Entity<Visite>(entity =>
            {
                entity.HasKey(e => e.Visite_Id).HasName("PK_Visite");


                /*entity.HasOne(d => d.VisiteObjet)
                    .WithMany(p => p.Visites)
                    .HasForeignKey(d => d.Visite_ObjetId)
                    .HasConstraintName("FK_Visite_Objet_Visite");*/

                entity.HasOne(d => d.VisitePlanning)
                    .WithMany(p => p.Visites)
                    .HasForeignKey(d => d.Visite_PlanningId)
                    .HasConstraintName("FK_Visite_PlanningVisite");

                entity.HasOne(d => d.VisiteTypeAnimation)
                    .WithMany(p => p.Visites)
                    .HasForeignKey(d => d.Visite_TypeAnimationId)
                    .HasConstraintName("FK_Visite_Type_Animation");
            });
            modelBuilder.Entity<VO>(entity =>
            {
                entity.HasKey(e => e.VO_Id).HasName("PK_VO");

                entity.HasOne(d => d.visite)
                    .WithMany(p => p.Vos)
                    .HasForeignKey(d => d.VO_VisiteId)
                    .HasConstraintName("FK_VO_Visite");

                entity.HasOne(d => d.objetVisite)
                    .WithMany(p => p.Voss)
                    .HasForeignKey(d => d.VO_ObjetVisiteId)
                    .HasConstraintName("FK_VO_Objet_Visite");
            });
            modelBuilder.Entity<DotationDistribues>(entity =>
            {
                entity.HasKey(e => e.DotationDistribues_Id).HasName("PK_DotationDistribues");

                entity.HasOne(d => d.DotationDistribuesVisite)
                    .WithMany(p => p.DotationDistribues)
                    .HasForeignKey(d => d.DotationDistribues_VisiteId)
                    .HasConstraintName("FK_Collaborateur_AspNetUsers");
                


            });
            modelBuilder.Entity<PrdVentesParMarque>(entity =>
            {
                entity.HasKey(e => e.PrdVentesParMarque_Id).HasName("PK_PrdVentesParMarque");

                entity.HasOne(d => d.Visite)
                    .WithMany(p => p.PrdVentesParMarques)
                    .HasForeignKey(d => d.PrdVentesParMarque_VisiteId)
                    .HasConstraintName("FK_PrdVentesParMarque_Visite");

            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
