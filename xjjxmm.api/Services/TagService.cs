using xjjxmm.Models;
using xjjxmm.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace xjjxmm.Services
{
    public class TagService: BaseService<Tag>
    {
        public TagService(BlogContext context):base(context)
        {
            Console.WriteLine("CategoryService");
        }


       
    }
}
