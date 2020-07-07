using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static xjjxmm.Common.CommonConstant;

namespace xjjxmm.Models
{
    public class Category : IBase
    {

        [MaxLength(10)]
        //[Column(name:"code")]
        public string Code { get; set; }

        [MaxLength(10)]
        //[Column(name: "desc")]
        public string Description { get; set; }


        public CategoryType Type { get; set; } = CategoryType.Menu;
        /// <summary>
        /// 顺序
        /// </summary>
        //[Column(name: "desc")]
        public int Seq { get; set; }

        /// <summary>
        /// 层级
        /// </summary>
        public int Level { get; set; }


       

        public long? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public List<Category> ChildCategories { get; set; }

        
        public List<Blog> Blogs { get; set; }
    }
}
