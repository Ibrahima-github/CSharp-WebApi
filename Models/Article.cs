using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Article
    {
        public int ArticleId { get; set; }
        public string ArticleName { get; set; }
        public string ArticleSummary { get; set; }
        public string ArticleDescription { get; set; }
        public string ArticlePhotoFileName { get; set; }
        public string ArticleUploadDate { get; set; }
        public string ArticleAffiliateLink { get; set; }
        public string Category { get; set; }
    }
}
