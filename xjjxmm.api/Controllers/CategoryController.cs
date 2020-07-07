using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using xjjxmm.DTO;
using xjjxmm.Models;
using xjjxmm.Repository;
using xjjxmm.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace xjjxmm.api.Controllers
{
    /// <summary>
    /// 类别控制器
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _service;
        private readonly IMapper _mapper;
        public CategoryController(CategoryService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        /// <summary>
        /// 获取所有category
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<IEnumerable<Category>>> Get()
        {
            return new MessageModel<IEnumerable<Category>>(){

                msg ="成功",
                response = await _service.Get(0, 1000)
            };
                
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CategoryController>
        [HttpPost]
        public async Task<MessageModel<Category>> Post([FromBody] CreateCategoryDTO category)
        {
            var model = _mapper.Map<Category>(category);
            var ca =  await _service.Save(model);

            return new MessageModel<Category>()
            {

                msg = "成功",
                response = ca
            };

            
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
