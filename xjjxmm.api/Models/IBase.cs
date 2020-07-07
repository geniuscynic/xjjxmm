using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace xjjxmm.Models
{
    public class IBase
    {
        public long Id { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;

        public DateTime Updated { get; set; } = DateTime.Now;

        public int? CreateBy { get; set; }

        public int? UpdateBy { get; set; }

    }
}
