using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xjjxmm.Common;

namespace xjjxmm.Models
{
    public class PageModel<T>
    {
        public int? page { get; set; }
        public int pageSize { get; set; } = CommonConstant.Page_Size;

        public IEnumerable<T> results { get; set; }
    }
}
