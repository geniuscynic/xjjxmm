using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using xjjxmm.Common;
using xjjxmm.DTO;
using xjjxmm.Models;
using xjjxmm.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace xjjxmm.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : BaseController<BlogService>
    {
       
        public BlogController(BlogService service, IMapper mapper):base(service, mapper)
        {
           ;
        }

        // GET: api/<BlogController>
        [HttpGet("p/{page:int?}")]
        public async Task<MessageModel<PageModel<Blog>>> Get(int? page = 1)
        {
            int offset = (page.GetValueOrDefault(1) - 1) * CommonConstant.Page_Size;

            var blogs = await _service.Get(offset, CommonConstant.Page_Size);

            return new MessageModel<PageModel<Blog>>()
            {
                msg = CommonConstant.Message_Successfully,
                response = new PageModel<Blog>()
                {
                    results = blogs,
                    page = page
                }
            };
        }
    

        // GET api/<BlogController>/5
        [HttpGet("{id}")]
        public async Task<MessageModel<Blog>> Get(long id)
        {
            var blog = await _service.FindById(id);
            return new MessageModel<Blog>
            {
                msg = CommonConstant.Message_Successfully ,
                response = blog
            };
        }

        // POST api/<BlogController>
        [HttpPost]
        public async Task<MessageModel<ResultBlogDTO>> Post([FromBody] CreateBlogDTO blog)
        {
            var model = _mapper.Map<Blog>(blog);

            blog.Tag.ForEach(t=> {
                model.Blog_Tags.Add(new Blog_Tag()
                {
                     Tag = new Tag()
                     {
                          Name = t.Trim(),
                          
                     },
                     //Blog = model
                });

               
            });


            //model.Blog_Tags.ForEach(bt =>
            //{
            //    bt.Tag.Blog_Tags.Add(bt);
            //});


           var ca = await _service.Add(model);

           var result = _mapper.Map<ResultBlogDTO>(model);

            model.Blog_Tags.ForEach(t =>
            {
                result.Tags.Add(t.Tag);
            });

            return new MessageModel<ResultBlogDTO>()
            {

                msg = "成功",
                response = result
            };
        }

        // PUT api/<BlogController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BlogController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
