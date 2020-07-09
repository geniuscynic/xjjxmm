using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xjjxmm.Common;
using xjjxmm.Models;

namespace xjjxmm.DTO
{
    public class CreateBlogDTO
    {

        public string Title { get; set; }

        public string Content { get; set; }


        public string From { get; set; }


        public long? CategoryId { get; set; }

        public List<string> Tag { get; set; }
    }

    public class ResultBlogDTO
    {
        public long Id { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;

        public DateTime Updated { get; set; } = DateTime.Now;

        public int? CreateBy { get; set; }

        public int? UpdateBy { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }


        public string From { get; set; }

        public long? CategoryId { get; set; }

        public Category Category { get; set; }

        public List<Tag> Tags { get; set; } = new List<Tag>();
    }


}
