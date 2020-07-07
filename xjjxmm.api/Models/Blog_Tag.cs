using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using xjjxmm.Models;

namespace xjjxmm.Models
{
    public class Blog_Tag
    {
        public long Id { get; set; }


        public long BlogId { get; set; }

        public long TagId { get; set; }

        //[JsonIgnore]
        [ForeignKey("BlogId")]
        public virtual Blog Blog { get; set; }

        [ForeignKey("TagId")]
        public Tag Tag { get; set; }
    }
}
