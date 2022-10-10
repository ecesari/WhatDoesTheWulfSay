using WhatDoesTheWulfSay.Application.Models.ReviewModels;
using WhatDoesTheWulfSay.Application.ReviewModels;

namespace WhatDoesTheWulfSay.Application.Common.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewViewModel>> GetAllAsync(Guid productId);
        Task<ReviewSummaryViewModel> GetSummaryAsync(Guid productId);
        Task<ReviewViewModel> InsertAsync(ReviewInsertCommand insertCommand);
    }
}
