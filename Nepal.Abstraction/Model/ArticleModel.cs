using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Nepal.Abstraction.Model
{
    public class ArticleViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string ImageFile { get; set; }
        public DateTime ArticleDate { get; set; }
    }

    public class ArticleModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
