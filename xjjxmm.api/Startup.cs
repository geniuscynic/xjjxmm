using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using xjjxmm.Repository;
using xjjxmm.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using xjjxmm.Helper;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Swashbuckle.AspNetCore.Filters;
using xjjxmm.Common;

namespace demo
{
    public class Startup
    {
        const string ApiName = "XJJXMM";
        const string Version = "v1";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            services.AddAutoMapper(typeof(Startup));//这是AutoMapper的2.0新特性

            #region add service
            services.AddScoped<CategoryService, CategoryService>();
            services.AddScoped<BlogService, BlogService>();
            services.AddScoped<TagService, TagService>();
            services.AddScoped<CommentService, CommentService>();
            #endregion

            //数据库
            var connection = Configuration.GetConnectionString("MysqlConnection");
            services.AddDbContext<BlogContext>(opt => opt.UseMySql(connection));


            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );


            //swagger ui
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(Version, new OpenApiInfo
                {
                    // {ApiName} 定义成全局变量，方便修改
                    Version = Version,
                    Title = $"{ApiName} 接口文档——Netcore 3.0",
                    Description = $"{ApiName} HTTP API V1",
                    Contact = new OpenApiContact { Name = ApiName, Email = "Blog.Core@xxx.com", Url = new Uri("https://www.jianshu.com/u/94102b59cc2a") },
                    License = new OpenApiLicense { Name = ApiName, Url = new Uri("https://www.jianshu.com/u/94102b59cc2a") }
                });

                //c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                //c.OrderActionsBy(o => o.RelativePath);

                var xmlPath = Path.Combine(AppContext.BaseDirectory, "demo.xml");//这个就是刚刚配置的xml文件名
                c.IncludeXmlComments(xmlPath, true);//默认的第二个参数是false，这个是controller的注释，记得修改


                c.OperationFilter<AddResponseHeadersFilter>();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

                c.OperationFilter<SecurityRequirementsOperationFilter>();


                #region Token绑定到ConfigureServices
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                    Name = "Authorization",//jwt默认的参数名称
                    In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
                    Type = SecuritySchemeType.ApiKey
                });
                #endregion


            });


            //services.AddAuthentication("mytest");
            //services.AddAuthorization(options =>
            //  {
            //      options.AddPolicy("Client", policy => policy.RequireRole("Client").Build());
            //      options.AddPolicy("Admin", policy => policy.RequireRole("Admin").Build());
            //      options.AddPolicy("SystemOrAdmin", policy => policy.RequireRole("Admin", "System"));
            //  });

           var jwtOptions = Configuration.GetOptions<JWTOptions>(CommonConstant.Option_JWT);
            
            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key));
            services.AddAuthentication("Bearer")
                .AddJwtBearer(c =>
                {
                    c.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = securityKey,

                        ValidateIssuer = true,
                        ValidIssuer = jwtOptions.Issuer,

                        ValidateAudience = true,
                        ValidAudience = jwtOptions.Audience,


                        RequireExpirationTime = true,
                        ValidateLifetime = true

                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint($"/swagger/{Version}/swagger.json", $"{ApiName} {Version}");

                    //路径配置，设置为空，表示直接在根域名（localhost:8001）访问该文件,注意localhost:8001/swagger是访问不到的，去launchSettings.json把launchUrl去掉，如果你想换一个路径，直接写名字即可，比如直接写c.RoutePrefix = "doc";
                    c.RoutePrefix = "";
                });
            }

            app.UseRouting();

            app.UseAuthentication();
            //app.UseAuthorization
            //app.UseJwtTokenAuth();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
