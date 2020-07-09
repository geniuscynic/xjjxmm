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


        public override async Task<Commment> Add(Commment commment)
        {
            var floor = await Query(t=> t.BlogId == commment.BlogId)
                .MaxAsync(c => c.Floor);

            commment.Floor = floor + 1;
            //category.Seq = seq + 1;

            return await base.Add(commment);
            //_context.Entry<Category>(category).State = EntityState.Added;
            //await _context.SaveChangesAsync();

            //return category;
        }

        public async Task<IEnumerable<Commment>> GetByBlogId(long blogId)
        {
            var comments = await Query(t => t.BlogId == blogId).ToListAsync();

            return comments;
        }
    }
}
