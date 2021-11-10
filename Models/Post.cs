﻿using Microsoft.AspNetCore.Http;
using System;

namespace WebAPI.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public string PostName { get; set; }
        public string Category { get; set; }
        public DateTime ArticleUploadDate { get; set; }
        public string PostDescription { get; set; }
        public string PostYoutubeHref { get; set; }
        public string ImageFileName { get; set; }
    }
}
