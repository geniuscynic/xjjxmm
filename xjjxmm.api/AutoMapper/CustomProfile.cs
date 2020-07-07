using AutoMapper;
using xjjxmm.DTO;
using xjjxmm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace xjjxmm.api.AutoMapper
{
    public class CustomProfile : Profile
    {
        /// <summary>
        /// 配置构造函数，用来创建关系映射
        /// </summary>
        public CustomProfile()
        {
            CreateMap<CreateCategoryDTO, Category>();
            CreateMap<CreateBlogDTO, Blog >();
            CreateMap<Blog, ResultBlogDTO>();
        }
    }
}
