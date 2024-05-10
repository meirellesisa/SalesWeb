using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesWebMvc.Models;

namespace SalesWebMvc.Data.Map
{
    public class SellerMap : IEntityTypeConfiguration<Seller>
    {
        public void Configure(EntityTypeBuilder<Seller> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(60).IsRequired();
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.BirthDate).IsRequired();
            builder.Property(x => x.BaseSalary).IsRequired();

            // Configuração da relação com SalesRecord
            builder.HasMany(seller => seller.Sales)
                   .WithOne(sr => sr.Seller)
                   .OnDelete(DeleteBehavior.Restrict); // Restrição de chave estrangeira

            builder.HasOne(s => s.Departments)
                   .WithMany(d => d.Sellers)
                   .HasForeignKey(s => s.DepartmentId);
        }
    }
}
