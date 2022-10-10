using Microsoft.EntityFrameworkCore;
using WhatDoesTheWulfSay.Domain.Entities;

namespace WhatDoesTheWulfSay.Infrastructure
{
    public class EcommerceManagementContext : DbContext
    {
        public EcommerceManagementContext(DbContextOptions<EcommerceManagementContext> options) : base(options) { }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }

    }
}
