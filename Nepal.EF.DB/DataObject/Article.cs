using System;
using System.Collections.Generic;
using System.Text;

namespace Nepal.EF.DB.DataObject
{
    public class Article: BaseObject, IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string ImageUrl { get; set; }
        public DateTime ArticleDate { get; set; }
    }
}
