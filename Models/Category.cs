using Microsoft.AspNetCore.Mvc;
using ServiceStack.DataAnnotations;

namespace WebAPI.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
    }
}