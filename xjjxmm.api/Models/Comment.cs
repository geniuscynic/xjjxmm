using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace xjjxmm.Models
{
    public class Commment : IBase
    {


        [MaxLength(200)]
        public string Content { get; set; }

        public int Floor { get; set; }


        public long? ParentId { get; set; }

        [JsonIgnore]
        [ForeignKey("ParentId")]
        public Commment ParentComment { get; set; }

        public List<Commment> ChildComments { get; set; }

        public long BlogId { get; set; }

        [ForeignKey("BlogId")]
        public Blog Blog { get; set; }

      

    }
}
