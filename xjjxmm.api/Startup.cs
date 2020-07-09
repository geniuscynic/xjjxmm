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


            services.AddAutoMapper(typeof(Startup));//����AutoMapper��2.0������

            #region add service
            services.AddScoped<CategoryService, CategoryService>();
            services.AddScoped<BlogService, BlogService>();
            services.AddScoped<TagService, TagService>();
            services.AddScoped<CommentService, CommentService>();
            #endregion

            //���ݿ�
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
                    // {ApiName} �����ȫ�ֱ����������޸�
                    Version = Version,
                    Title = $"{ApiName} �ӿ��ĵ�����Netcore 3.0",
                    Description = $"{ApiName} HTTP API V1",
                    Contact = new OpenApiContact { Name = ApiName, Email = "Blog.Core@xxx.com", Url = new Uri("https://www.jianshu.com/u/94102b59cc2a") },
                    License = new OpenApiLicense { Name = ApiName, Url = new Uri("https://www.jianshu.com/u/94102b59cc2a") }
                });

                //c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                //c.OrderActionsBy(o => o.RelativePath);

                var xmlPath = Path.Combine(AppContext.BaseDirectory, "demo.xml");//������Ǹո����õ�xml�ļ���
                c.IncludeXmlComments(xmlPath, true);//Ĭ�ϵĵڶ���������false�������controller��ע�ͣ��ǵ��޸�


                c.OperationFilter<AddResponseHeadersFilter>();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

                c.OperationFilter<SecurityRequirementsOperationFilter>();


                #region Token�󶨵�ConfigureServices
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "JWT��Ȩ(���ݽ�������ͷ�н��д���) ֱ�����¿�������Bearer {token}��ע������֮����һ���ո�\"",
                    Name = "Authorization",//jwtĬ�ϵĲ�������
                    In = ParameterLocation.Header,//jwtĬ�ϴ��Authorization��Ϣ��λ��(����ͷ��)
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

                    //·�����ã�����Ϊ�գ���ʾֱ���ڸ�������localhost:8001�����ʸ��ļ�,ע��localhost:8001/swagger�Ƿ��ʲ����ģ�ȥlaunchSettings.json��launchUrlȥ����������뻻һ��·����ֱ��д���ּ��ɣ�����ֱ��дc.RoutePrefix = "doc";
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
