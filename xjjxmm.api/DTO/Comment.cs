using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static xjjxmm.Common.CommonConstant;

namespace xjjxmm.DTO
{
    public class CreateCommentDTO 
    {

        public long BlogId { get; set; }

        public string Content { get; set; }

        public long? ParentId { get; set; }

    }
}
