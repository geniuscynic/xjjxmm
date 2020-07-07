using xjjxmm.Models;
using xjjxmm.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace xjjxmm.Services
{
    public class CommentService: BaseService<Commment>
    {
        public CommentService(BlogContext context):base(context)
        {
            Console.WriteLine("CategoryService");
        }


        public override async Task<Commment> Save(Commment commment)
        {
            //var seq = await _context.Categories.AsNoTracking()
            //    .Where(c => c.Level == 1)
            //    .MaxAsync(c => c.Seq);

            //category.Seq = seq + 1;

            return await base.Save(commment);
            //_context.Entry<Category>(category).State = EntityState.Added;
            //await _context.SaveChangesAsync();

            //return category;
        }
    }
}
