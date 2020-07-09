using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using xjjxmm.DTO;
using xjjxmm.Models;
using xjjxmm.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace xjjxmm.api.Controllers
{
   

    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : BaseController<CommentService>
    {
        public CommentController(CommentService service, IMapper mapper):base(service, mapper)
        {
            
        }


        // GET: api/<CommentController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<CommentController>/5
        [HttpGet("{blogId}")]
        public async Task<MessageModel<IEnumerable<Commment>>> Get(long blogId)
        {
            var result = await _service.GetByBlogId(blogId);

            return new MessageModel<IEnumerable<Commment>>() {
                response = result
            };
        }

        // POST api/<CommentController>
        [HttpPost]
        public async Task<MessageModel<Commment>> Post([FromBody] CreateCommentDTO commment)
        {
            var model = _mapper.Map<Commment>(commment);

            var result =  await _service.Add(model);

            return new MessageModel<Commment>()
            {
                response = result
            };
        }

        // PUT api/<CommentController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CommentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
