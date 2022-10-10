using WhatDoesTheWulfSay.Domain.Entities;

namespace WhatDoesTheWulfSay.Domain.Specifications.ReviewSpecifications
{
    public class HasProductSpecification : BaseSpecification<Review>
	{
		public HasProductSpecification(Guid productId) : base(p => p.ProductId == productId)
		{

		}
	}

}
