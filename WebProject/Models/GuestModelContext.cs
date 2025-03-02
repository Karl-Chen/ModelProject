using Microsoft.EntityFrameworkCore;
using WebProject.ViewModels;

namespace WebProject.Models
{
    public class GuestModelContext : DbContext
    {
        public GuestModelContext(DbContextOptions<GuestModelContext> options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => new
                {
                    e.OrderNo, e.ProductID
                });
            });
            modelBuilder.Entity<HandleOrder>(entity =>
            {
                entity.HasKey(e => new
                {
                    e.StaffID, e.OrderNo
                });
            });
        }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<Member> Member { get; set; }
        public DbSet<MemberAcc> MemberAcc { get; set; }
        public DbSet<MemberTel> MemberTel { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<Ordertatus> Ordertatus { get; set; }
        public DbSet<PayWay> PayWay { get; set; }
        public DbSet<Product> Prodect { get; set; }
        public DbSet<ProductSpecification> ProductSpecification { get; set; }
        public DbSet<ProductType> ProductType { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<ShippingWay> ShippingWay { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Supplier> Supplier { get; set; }
        public DbSet<HandleOrder> HandleOrder { get; set; }
        public DbSet<WebProject.ViewModels.VMMemberAcc> VMMemberAcc { get; set; } = default!;
        public DbSet<WebProject.ViewModels.VMMembers> VMMembers { get; set; } = default!;
        public DbSet<WebProject.ViewModels.VMEditMemberAcc> VMEditMemberAcc { get; set; } = default!;
        public DbSet<WebProject.ViewModels.VMEditMember> VMEditMember { get; set; } = default!;
    }
}
