using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static xjjxmm.Helper.JwtHelper;

namespace xjjxmm.Helper
{
    public class JwtHelper
    {
        /// <summary>
        /// 颁发JWT字符串
        /// </summary>
        /// <param name="tokenModel"></param>
        /// <returns></returns>
        public static string IssueJwt(TokenModelJwt tokenModel)
        {
            string iss = "xjjxmm Issuer";
            string aud = "xjjxmm Audience";
            string secret = "ddfdsfdfdfdgjfdiogjierjt89r4324343";

            //var claims = new Claim[] //old
            var claims = new List<Claim>
         {
          /*
          * 特别重要：
            1、这里将用户的部分信息，比如 uid 存到了Claim 中，如果你想知道如何在其他地方将这个 uid从 Token 中取出来，请看下边的SerializeJwt() 方法，或者在整个解决方案，搜索这个方法，看哪里使用了！
            2、你也可以研究下 HttpContext.User.Claims ，具体的你可以看看 Policys/PermissionHandler.cs 类中是如何使用的。
          */

             

         //new Claim(JwtRegisteredClaimNames.Jti, tokenModel.Uid.ToString()),
         //new Claim(JwtRegisteredClaimNames.Iat, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
         //new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}") ,
         ////这个就是过期时间，目前是过期1000秒，可自定义，注意JWT有自己的缓冲过期时间
         //new Claim (JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.AddSeconds(1000)).ToUnixTimeSeconds()}"),
         //new Claim(ClaimTypes.Expiration, DateTime.Now.AddSeconds(1000).ToString()),
         //new Claim(JwtRegisteredClaimNames.Iss,iss),
         //new Claim(JwtRegisteredClaimNames.Aud,aud),
         
         //new Claim(ClaimTypes.Role,tokenModel.Role),//为了解决一个用户多个角色(比如：Admin,System)，用下边的方法
        };

            // 可以将一个用户的多个角色全部赋予；
            // 作者：DX 提供技术支持；
           // claims.AddRange(tokenModel.Role.Split(',').Select(s => new Claim(ClaimTypes.Role, s)));



            //秘钥 (SymmetricSecurityKey 对安全性的要求，密钥的长度太短会报出异常)
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                issuer: iss,
                claims: claims,
                signingCredentials: creds);

            var jwtHandler = new JwtSecurityTokenHandler();
            var encodedJwt = jwtHandler.WriteToken(jwt);

            return encodedJwt;
        }



        public static TokenModelJwt SerializeJwt(string jwtStr)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(jwtStr);
            object role;
            try
            {
                jwtToken.Payload.TryGetValue(ClaimTypes.Role, out role);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            var tm = new TokenModelJwt
            {
                Uid = int.Parse(jwtToken.Id),
                Role = role != null ? role.ToString() : "",
            };
            return tm;
        }
        /// <summary>
        /// 令牌
        /// </summary>
        public class TokenModelJwt
        {
            /// <summary>
            /// Id
            /// </summary>
            public long Uid { get; set; }
            /// <summary>
            /// 角色
            /// </summary>
            public string Role { get; set; }
            /// <summary>
            /// 职能
            /// </summary>
            public string Work { get; set; }

        }
    }


    public class JwtTokenAuth
    {
        // 中间件一定要有一个next，将管道可以正常的走下去
        private readonly RequestDelegate _next;
        public JwtTokenAuth(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {

            //检测是否包含'Authorization'请求头
            if (!httpContext.Request.Headers.ContainsKey("Authorization"))
            {
                return _next(httpContext);
            }
            var tokenHeader = httpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            try
            {
                if (tokenHeader.Length >= 128)
                {
                    TokenModelJwt tm = JwtHelper.SerializeJwt(tokenHeader);

                    //授权 Claim 关键
                    var claimList = new List<Claim>();
                    var claim = new Claim(ClaimTypes.Role, tm.Role);
                    claimList.Add(claim);
                    var identity = new ClaimsIdentity(claimList);
                    var principal = new ClaimsPrincipal(identity);
                    httpContext.User = principal;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine($"{DateTime.Now} middleware wrong:{e.Message}");
            }
            return _next(httpContext);
        }

    }
    // 这里定义一个中间件Helper，主要作用就是给当前模块的中间件取一个别名
    public static class MiddlewareHelpers
    {
        public static IApplicationBuilder UseJwtTokenAuth(this IApplicationBuilder app)
        {
            return app.UseMiddleware<JwtTokenAuth>();
        }
    }
}
