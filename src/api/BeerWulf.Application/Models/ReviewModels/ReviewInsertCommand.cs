namespace WhatDoesTheWulfSay.Application.Models.ReviewModels
{
    public class ReviewInsertCommand
    {
        public int Score { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public bool IsRecommended { get; set; }
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
    }
}
