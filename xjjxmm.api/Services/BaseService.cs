using xjjxmm.Models;
using xjjxmm.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace xjjxmm.Services
{
    public class BaseService<T>  where T:IBase
    {
        protected BlogContext _context;
        public BaseService(BlogContext context)
        {
            Console.WriteLine("Base");
            _context = context;
        }

        public async Task<IEnumerable<T>> Get(int offset, int pageSize)
        {
            return await _context.Set<T>().Skip(offset).Take(pageSize).ToListAsync<T>();
            //return await _context.Set<T>().ToListAsync<T>();
        }

        public async Task<T> Query(params object[] keyValues)
        {
            return await _context.Set<T>().FindAsync(keyValues);
        }

        public IQueryable<T> Query(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression);
        }   

        public virtual async Task<T> Add(T t)
        {
            //_context.Entry<T>(t).State = EntityState.Added;
            await _context.Set<T>().AddAsync(t);
            await _context.SaveChangesAsync();

            return t;
        }
    }
}
