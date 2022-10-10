using FluentValidation;
using WhatDoesTheWulfSay.Application.Common.Interfaces;
using WhatDoesTheWulfSay.Application.Models.ReviewModels;

namespace WhatDoesTheWulfSay.Application.Tests.Validation
{
	class InsertReviewCommandValidator : AbstractValidator<ReviewInsertCommand>
	{
		public InsertReviewCommandValidator(IEcommerceManagementContext context)
		{
			RuleFor(v => v.Title)
				.NotEmpty().WithMessage("Title is required.");

			RuleFor(v => v.Comment)
				.NotEmpty().WithMessage("Comment is required.");

			RuleFor(v => v.Score)
				.NotEmpty().WithMessage("Score is required.")
				.InclusiveBetween(1,5).WithMessage("Score must be between 1 and 5.");
		}
	}
}
