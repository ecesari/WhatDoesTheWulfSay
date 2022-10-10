using WhatDoesTheWulfSay.Application.Models.UserModels;

namespace WhatDoesTheWulfSay.Application.ReviewModels
{
    public class ReviewViewModel
    {
        public int Score { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public bool IsRecommended { get; set; }
        public UserViewModel User { get; set; }
    }
}
