using xjjxmm.Models;
using xjjxmm.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using xjjxmm.Common;

namespace xjjxmm.Services
{
    public class BlogService: BaseService<Blog>
    {
        public BlogService(BlogContext context):base(context)
        {
            Console.WriteLine("CategoryService");
        }


        public async Task<Blog> FindById(long id)
        {

            return await _context.Blogs
                .Include(t=>t.Blog_Tags)
                .ThenInclude(ts=>ts.Tag)
                .Include(t=>t.Commments)
                .SingleOrDefaultAsync(t=>t.Id == id);
        }

        //public async Task<List<Blog>> GetBlogs ()
        //{
        //    return await _context.Blog .Take (20).ToListAsync();
        //}

        public override async Task<Blog> Add(Blog blog)
        {

            //foreach(var bt in blog.Blog_Tags) { 
            await blog.Blog_Tags.ForEachAsync(async bt=> {

                var tag = await _context.Tags.SingleOrDefaultAsync(t => t.Name == bt.Tag.Name);
                if (tag == null)
                {

                }
                else
                {
                    bt.TagId = tag.Id;
                    bt.Tag = tag;
                }
            });


            //_context.Blog.Add(blog);

            //_context.Set<Blog>().Add(blog);
            //await  _context.SaveChangesAsync();
            return await base.Add(blog);
            //_context.Entry<Category>(category).State = EntityState.Added;
            //await _context.SaveChangesAsync();

            //return category;
        }
    }
}
