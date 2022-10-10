using AutoMapper;
using System;
using WhatDoesTheWulfSay.Application.Common.Mappers;
using WhatDoesTheWulfSay.Application.Models.ReviewModels;
using WhatDoesTheWulfSay.Application.ReviewModels;
using WhatDoesTheWulfSay.Domain.Entities;
using Xunit;

namespace WhatDoesTheWulfSay.Application.Test
{
    public class MapperTests
    {
        private readonly IMapper _mapper;

        public MapperTests()
        {
            IConfigurationProvider configuration = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperConfig());
            });
            _mapper = configuration.CreateMapper();
        }


        [Theory]
        [InlineData("Sample Title", "Sample Comment", 2, false, "Sample Username", "fb0f9c4a-c5aa-4092-9d12-454990eada7b")]
        public void CreateReviewCOmmandaMappings(string title, string comment, int score, bool recommended, string username, Guid userId)
        {
            var command = new ReviewInsertCommand
            {
                Comment = comment,
                Score = score,
                IsRecommended = recommended,
                Title = title,
                UserId = userId
            };
            var review = _mapper.Map<Review>(command);
            Assert.Equal(title, review.Title);
            Assert.Equal(comment, review.Comment);  
            Assert.Equal(score, review.Score);  
            Assert.Equal(recommended, review.IsRecommended);    
            Assert.Equal(userId, review.UserId);
        }

        [Theory]
        [InlineData("Sample Title", "Sample Comment", 2, false, "Sample Username", "fb0f9c4a-c5aa-4092-9d12-454990eada7b", "30b8e82d-c745-4792-82fc-c03666012545")]
        public void ReviewVMMappings(string title, string comment, int score, bool recommended, string username, Guid userId, Guid productId)
        {
            var user = new User
            {
                Id = userId,
                Username = username,
            };
            var review = new Review
            {
                Comment = comment,
                Score = score,
                IsRecommended = recommended,
                Title = title,
                UserId = userId,
                ProductId = productId,
                CreatedUser = user
            };
            var vm = _mapper.Map<ReviewViewModel>(review);
            Assert.Equal(title, vm.Title);
            Assert.Equal(comment, vm.Comment);
            Assert.Equal(score, vm.Score);
            Assert.Equal(recommended, vm.IsRecommended);
            Assert.Equal(username, vm.User.Username);
        }
    }
}
