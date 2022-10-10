using AutoMapper;
using WhatDoesTheWulfSay.Application.Common.Interfaces;
using WhatDoesTheWulfSay.Application.Models.ReviewModels;
using WhatDoesTheWulfSay.Application.ReviewModels;
using WhatDoesTheWulfSay.Domain.Entities;
using WhatDoesTheWulfSay.Domain.Repositories;
using WhatDoesTheWulfSay.Domain.Specifications.ReviewSpecifications;

namespace WhatDoesTheWulfSay.Application.Services
{
    public class ReviewService : IReviewService
    {

        //private readonly ICameraRepository cameraRepository;
        //private readonly IMapper mapper;

        //public CameraService(ICameraRepository cameraRepository, IMapper mapper)
        //{
        //    this.cameraRepository = cameraRepository;
        //    this.mapper = mapper;
        //}

        //public async Task<IEnumerable<ReviewViewModel>> GetAll()
        //{
        //    var cameras = await cameraRepository.GetAllAsync();
        //    var viewModel = mapper.Map<List<ReviewViewModel>>(cameras);
        //    return await Task.FromResult(viewModel);
        //}

        //public async Task<IEnumerable<ReviewViewModel>> GetByName(string name)
        //{
        //    var specification = new HasNameSpecification(name);
        //    var entities = await cameraRepository.GetAsync(specification);
        //    var viewModel = mapper.Map<List<ReviewViewModel>>(entities);
        //    return await Task.FromResult(viewModel);
        //}

        //public async Task<IEnumerable<ReviewViewModel>> BulkAdd(List<ReviewInsertCommand> insertCommands)
        //{
        //    var entities = mapper.Map<List<Domain.Entities.Camera>>(insertCommands);
        //    var cameras = await cameraRepository.AddBulkAsync(entities);
        //    var returnModel = mapper.Map<List<ReviewViewModel>>(cameras);
        //    return await Task.FromResult(returnModel);
        //}

        private readonly IReviewRepository reviewRepository;
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public ReviewService(IReviewRepository reviewRepository, IProductRepository productRepository, IMapper mapper)
        {
            this.reviewRepository = reviewRepository;
            this.productRepository = productRepository;
            this.mapper = mapper;
        }


        public async Task<IEnumerable<ReviewViewModel>> GetAllAsync(Guid productId)
        {
            var specification = new HasProductSpecification(productId);
            var entities = await reviewRepository.GetAsync(specification);
            var returnModels = mapper.Map<List<ReviewViewModel>>(entities);

            return returnModels;
        }

        public async Task<ReviewSummaryViewModel> GetSummaryAsync(Guid productId)
        {
            var specification = new HasProductSpecification(productId);
            var entities = await reviewRepository.GetAsync(specification);
            var totalReviewCount = entities.Count();
            var summary = new ReviewSummaryViewModel
            {
                AverageScore = entities.Sum(x => x.Score) / totalReviewCount,
                RecommendationPercentage = entities.Where(x => x.IsRecommended).Count() / totalReviewCount * 100,
                NumberOfReviews = totalReviewCount
            };

            return summary;
        }

        public async Task<ReviewViewModel> InsertAsync(ReviewInsertCommand insertCommand)
        {
            var entity = mapper.Map<Review>(insertCommand);
            var review = await reviewRepository.AddAsync(entity);
            var returnModel = mapper.Map<ReviewViewModel>(review);
            return returnModel;

        }
    }
}
