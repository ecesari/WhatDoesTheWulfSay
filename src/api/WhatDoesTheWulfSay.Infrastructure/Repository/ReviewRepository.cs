using WhatDoesTheWulfSay.Domain.Entities;
using WhatDoesTheWulfSay.Domain.Repositories;

namespace WhatDoesTheWulfSay.Infrastructure.Repository
{
    public class ReviewRepository : BaseRepository<Review>, IReviewRepository
    {
        public ReviewRepository(EcommerceManagementContext dbContext) : base(dbContext)
        {

        }
    }
}
