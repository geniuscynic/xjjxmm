using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace xjjxmm.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<T> : ControllerBase
    {
        protected readonly T _service;
        protected readonly IMapper _mapper;
        public BaseController(T service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

    }
}
