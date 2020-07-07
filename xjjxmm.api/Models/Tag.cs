using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using xjjxmm.Models;

namespace xjjxmm.Models
{
    public class Tag : IBase
    {
        [MaxLength(10)]
        public string Name { get; set; }

        [JsonIgnore]
        public List<Blog_Tag> Blog_Tags { get; set; }
    }
}
