using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using xjjxmm.Helper;
using static xjjxmm.Helper.JwtHelper;

namespace xjjxmm.api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        //private readonly BlogService _service;
       // private readonly IMapper _mapper;
        public AuthorizationController()
        {
            //_service = service;
            //_mapper = mapper;
        }

        [HttpGet]
        public string GetToken()
        {
            SecurityToken securityToken = new JwtSecurityToken(
                issuer: "issuer",
                audience: "audience",
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes("dsf677877dsdsfdsf443fdgfdjdsfdsfdfdfsdfjjjj")),
                    SecurityAlgorithms.HmacSha256
                    ),

                expires: DateTime.Now.AddDays(1),
                claims: new Claim[] { }
            );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(securityToken);


            return jwtToken;
        }

        [HttpGet]
        public async Task<object> GetJwtStr(string name, string pass)
        {
            // 将用户id和角色名，作为单独的自定义变量封装进 token 字符串中。
            TokenModelJwt tokenModel = new TokenModelJwt { Uid = 1, Role = "Admin" };
            var jwtStr = JwtHelper.IssueJwt(tokenModel);//登录，获取到一定规则的 Token 令牌
            var suc = true;
            return Ok(new
            {
                success = suc,
                token = jwtStr
            });
        }
    }
}
