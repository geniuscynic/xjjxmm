using xjjxmm.Models;
using xjjxmm.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace xjjxmm.Services
{
    public abstract class BaseService<T> where T:IBase
    {
        protected BlogContext _context;
        public BaseService(BlogContext context)
        {
            Console.WriteLine("Base");
            _context = context;
        }

        public virtual async Task<IEnumerable<T>> Get(int offset, int pageSize)
        {
            return await _context.Set<T>().Skip(offset).Take(pageSize).ToListAsync<T>();
            //return await _context.Set<T>().ToListAsync<T>();
        }

        public virtual async Task<T> Find(params object[] keyValues)
        {
            return await _context.Set<T>().FindAsync(keyValues);
        }
        public virtual async Task<T> Save(T t)
        {
            //_context.Entry<T>(t).State = EntityState.Added;
            await _context.Set<T>().AddAsync(t);
            await _context.SaveChangesAsync();

            return t;
        }
    }
}
