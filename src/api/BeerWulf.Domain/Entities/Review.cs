namespace WhatDoesTheWulfSay.Domain.Entities
{
    public class Review:BaseEntity
    {
        public int Score { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public bool IsRecommended { get; set; }
        public Guid UserId { get; set; }
        public User CreatedUser { get; set; }
        public Guid ProductId { get; set; }
        public Product ReviewedProduct { get; set; }

        
    }
}
