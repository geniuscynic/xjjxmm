using xjjxmm.Models;
using xjjxmm.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace xjjxmm.Services
{
    public class CategoryService: BaseService<Category>
    {
        public CategoryService(BlogContext context):base(context)
        {
            Console.WriteLine("CategoryService");
        }


        public override async Task<Category> Save(Category category)
        {
            var seq = await _context.Categories.AsNoTracking()
                .Where(c => c.Level == category.Level && c.ParentId == category.ParentId)
                .MaxAsync(c => c.Seq);

            category.Seq = seq + 1;

            return await base.Save(category);
            //_context.Entry<Category>(category).State = EntityState.Added;
            //await _context.SaveChangesAsync();

            //return category;
        }
    }
}
