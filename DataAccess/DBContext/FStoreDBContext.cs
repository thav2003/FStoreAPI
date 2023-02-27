using DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DBContext
{
    public class FStoreDBContext:DbContext
    {
        public FStoreDBContext(DbContextOptions<FStoreDBContext> contextOptions)
            : base(contextOptions)
        {
        }

       

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Shipper> Shippers { get; set; }
        public virtual DbSet<RoleDto> Roles { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

            });
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("products");
        

            });
            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("orders");

            });
            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.ToTable("order_details");

            });
            modelBuilder.Entity<Shipper>(entity =>
            {
                entity.ToTable("shippers");

            });

            foreach (Role role in Enum.GetValues(typeof(Role)).Cast<Role>())
            {
                RoleDto roleDto = new RoleDto
                {
                    Id = role,
                    Name = role.ToString()
                };
                modelBuilder.Entity<RoleDto>(entity =>
                {
                    entity.ToTable("roles");
                    entity.HasData(roleDto);

                });
            }

        }
       
    }
}

