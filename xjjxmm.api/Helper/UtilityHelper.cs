using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xjjxmm.Common;
using xjjxmm.Models;
using xjjxmm.Repository;

namespace xjjxmm.Helper
{
    public class UtilityHelper
    {
        BlogContext _context;
        public UtilityHelper(BlogContext context)
        {
            this._context = context;
        }

        public void DataSeed()
        {

            #region category
            var categories = new List<Category>
            {
                new Category()
                {
                    Code = "index",
                    Description = "首页",
                    Seq = 1,
                    Level = 1
                },
                new Category()
                {
                    Code = "qs",
                    Description = "糗事",
                    Seq = 2,
                    Level = 1
                },
                new Category()
                {
                    Code = "mt",
                    Description = "美图",
                    Seq = 3,
                    Level = 1
                },
            };

            _context.Categories.AddRange(categories);
            #endregion


            #region blog

            #endregion
        }

        //public static T GetOptions<T>(IConfiguration _configuration, string key) where T : new()
        //{
        //    var section = _configuration.GetSection(key);
        //    var t = new T();

        //    section.Bind(t);

        //    return t;
        //}
    }


    public static class OptionHelpers
    {

        public static T GetOptions<T>(this IConfiguration _configuration, string key) where T : new()
        {
            var section = _configuration.GetSection(key);
            var t = new T();

            section.Bind(t);

            return t;
        }
    }
}
