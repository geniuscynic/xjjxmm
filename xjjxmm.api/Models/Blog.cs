using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace xjjxmm.Models
{
    public class Blog : IBase
    {
        [MaxLength(50)]
        public string Title { get; set; }

        public string Content { get; set; }

        [MaxLength(200)]
        public string From { get; set; }


        public long? CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        
        public List<Blog_Tag> Blog_Tags { get; set; } = new List<Blog_Tag>();


        public List<Commment> Commments { get; set; } = new List<Commment>();

    }
}
