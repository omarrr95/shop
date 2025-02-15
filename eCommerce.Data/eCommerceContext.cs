using eCommerce.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Data
{
    public class eCommerceContext : IdentityDbContext<eCommerceUser>
    {
        public eCommerceContext(DbContextOptions<eCommerceContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Promo>()
                .HasIndex(p => p.Code)
                .IsUnique();

            modelBuilder.Entity<eCommerceUser>().ToTable("Users");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
        }

        public DbSet<Language> Languages { get; set; }
        public DbSet<LanguageResource> LanguageResources { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryRecord> CategoryRecords { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductRecord> ProductRecords { get; set; }
        public DbSet<Promo> Promos { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<ProductPicture> ProductPictures { get; set; }
        public DbSet<ProductSpecification> ProductSpecifications { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Configuration> Configurations { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderHistory> OrderHistories { get; set; }
        public DbSet<NewsletterSubscription> NewsletterSubscriptions { get; set; }

        //public static eCommerceContext Create()
        //{
        //    var optionsBuilder = new DbContextOptionsBuilder<eCommerceContext>();
        //    optionsBuilder.UseSqlServer("YourConnectionStringHere");

        //    return new eCommerceContext(optionsBuilder.Options);
        //}
    }
}
