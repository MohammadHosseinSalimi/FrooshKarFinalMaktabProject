using FrooshKar.Domain.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FrooshKar.Infrastructures.Db.Sql.EF.DataBase;

public partial class FrooshKarDbContext : IdentityDbContext<AppUser, IdentityRole<int>,int>
{
    public FrooshKarDbContext()
    {
    }

    public FrooshKarDbContext(DbContextOptions<FrooshKarDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Data Source=.;Initial Catalog=FrooshKarDb;TrustServerCertificate=True;Integrated Security=True;");
    }

    public virtual DbSet<Bid> Bids { get; set; }

    public virtual DbSet<BidProduct> BidProducts { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Factor> Factors { get; set; }

    public virtual DbSet<FixedPriceProduct> FixedPriceProducts { get; set; }

    public virtual DbSet<Medal> Medals { get; set; }

    public virtual DbSet<ProductImage> ProductImages { get; set; }

    public virtual DbSet<Vendor> Vendors { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);


        modelBuilder.Entity<Bid>(entity =>
        {
            entity.HasIndex(e => e.BidProductId, "IX_Bids_BidProductId");

            entity.HasIndex(e => e.CustomerId, "IX_Bids_CustomerId");

            entity.HasOne(d => d.BidProduct).WithMany(p => p.Bids)
                .HasForeignKey(d => d.BidProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Bids_BidProducts");

            entity.HasOne(d => d.Customer).WithMany(p => p.Bids)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Bids_Customers");
        });

        modelBuilder.Entity<BidProduct>(entity =>
        {
            entity.HasIndex(e => e.CategoryId, "IX_BidProducts_CategoryId");

            entity.HasIndex(e => e.VendorId, "IX_BidProducts_VendorId");

            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.EndBidTime)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.StartBidTime)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Title).HasMaxLength(50);

            entity.HasOne(d => d.Category).WithMany(p => p.BidProducts)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BidProducts_Categories");

            entity.HasOne(d => d.Vendor).WithMany(p => p.BidProducts)
                .HasForeignKey(d => d.VendorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BidProducts_Vendors");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Cart");

            entity.HasIndex(e => e.CustomerId, "IX_Carts_CustomerId");

            entity.HasIndex(e => e.FactorId, "IX_Carts_FactorId");

            entity.HasOne(d => d.Customer).WithMany(p => p.Carts)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Carts_Customers");

            entity.HasOne(d => d.Factor).WithMany(p => p.Carts)
                .HasForeignKey(d => d.FactorId)
                .HasConstraintName("FK_Carts_Factors");

            entity.HasOne(d => d.FixedPriceProduct).WithMany(p => p.Carts)
                .HasForeignKey(d => d.FixedPriceProductId)
                .HasConstraintName("FK_Carts_FixedPriceProducts");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.CategoryImageUrl)
                .HasMaxLength(250)
                .HasColumnName("CategoryImageURL");
            entity.Property(e => e.Title).HasMaxLength(50);
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.Property(e => e.Title).HasMaxLength(50);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasIndex(e => e.FactorId, "IX_Comments").IsUnique();
            entity.Property(e => e.Description).HasMaxLength(200);


			entity.HasOne(d => d.Factor).WithOne(p => p.Comment)
                .HasForeignKey<Comment>(d => d.FactorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comments_Factors");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
	        entity.HasIndex(e => e.AppUserId, "IX_Users").IsUnique();
	        entity.Property(e => e.Address).HasMaxLength(150);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.Gender).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.ProfileImageUrl)
                .HasMaxLength(250)
                .HasColumnName("ProfileImageURL");

            entity.HasOne(d => d.City).WithMany(p => p.Customers)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Customers_Cities");


            entity.HasOne(d => d.AppUser).WithOne(p => p.Customer)
	            .HasForeignKey<Customer>(d => d.AppUserId)
	            .OnDelete(DeleteBehavior.ClientSetNull)
	            .HasConstraintName("FK_Customers_AppUsers");


		});

        modelBuilder.Entity<FixedPriceProduct>(entity =>
        {
            entity.HasIndex(e => e.CategoryId, "IX_FixedPriceProducts_CategoryId");

            entity.HasIndex(e => e.VendorId, "IX_FixedPriceProducts_VendorId");

            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Title).HasMaxLength(50);

            entity.HasOne(d => d.Category).WithMany(p => p.FixedPriceProducts)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FixedPriceProducts_Categories");

            entity.HasOne(d => d.Vendor).WithMany(p => p.FixedPriceProducts)
                .HasForeignKey(d => d.VendorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FixedPriceProducts_Vendors");
        });


        modelBuilder.Entity<Medal>(entity =>
        {
            entity.Property(e => e.Title).HasMaxLength(50);
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.HasIndex(e => e.BidProductId, "IX_ProductImages_BidProductId");

            entity.HasIndex(e => e.FixedPriceProductId, "IX_ProductImages_FixedPriceProductId");

            entity.Property(e => e.Url)
                .HasMaxLength(250)
                .HasColumnName("URL");

            entity.HasOne(d => d.BidProduct).WithMany(p => p.ProductImages)
                .HasForeignKey(d => d.BidProductId)
                .HasConstraintName("FK_ProductImages_BidProducts");

            entity.HasOne(d => d.FixedPriceProduct).WithMany(p => p.ProductImages)
                .HasForeignKey(d => d.FixedPriceProductId)
                .HasConstraintName("FK_ProductImages_FixedPriceProducts");
        });

        modelBuilder.Entity<Vendor>(entity =>
        {
            entity.HasIndex(e => e.AppUserId, "IX_Users").IsUnique();


			entity.Property(e => e.Address).HasMaxLength(150);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.Gender).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.ProfileImageUrl)
                .HasMaxLength(250)
                .HasColumnName("ProfileImageURL");

            entity.HasOne(d => d.City).WithMany(p => p.Vendors)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vendors_Cities");

            entity.HasOne(d => d.Medal).WithMany(p => p.Vendors)
	            .HasForeignKey(d => d.MedalId)
	            .OnDelete(DeleteBehavior.ClientSetNull)
	            .HasConstraintName("FK_Vendors_Medals1");


            entity.HasOne(d => d.AppUser).WithOne(p => p.Vendor)
	            .HasForeignKey<Vendor>(d => d.AppUserId)
	            .OnDelete(DeleteBehavior.ClientSetNull)
	            .HasConstraintName("FK_Vendors_AppUsers");

        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
