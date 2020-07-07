using xjjxmm.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace xjjxmm.Repository
{
    public class BlogContext : DbContext
    {
        

        public BlogContext(DbContextOptions<BlogContext> options)
            : base(options)
        {
                     
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //按model的名字生成表，而不是DbSet的名字
           

            base.OnModelCreating(modelBuilder);



        }


        public DbSet<Category> Categories { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public DbSet<Commment> Commments { get; set; }
    }
}
