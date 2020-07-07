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
    public class CreateCategoryDTO 
    {

        public string Code { get; set; }

        public string Description { get; set; }

        public CategoryType Type { get; set; } = CategoryType.Menu;
       
        /// <summary>
        /// 层级
        /// </summary>
        public int Level { get; set; } = 1; 

        /// <summary>
        /// 父ID，默认 去掉
        /// </summary>
        public long? ParentId { get; set; } = null;


        
    }
}
