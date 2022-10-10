using Microsoft.EntityFrameworkCore;
using WhatDoesTheWulfSay.Domain.Entities;

namespace WhatDoesTheWulfSay.Application.Common.Interfaces
{
    internal interface IEcommerceManagementContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<Review> Reviews { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
