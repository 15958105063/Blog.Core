using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using static Blog.Core.SwaggerHelper.CustomApiVersion;//引用版本类（自定义）
using System.Reflection;//需要引用
using Swashbuckle.AspNetCore.Swagger;//需要引用
using Blog.Core.AuthHelper;//需要引用（自行一中间件）
using System.IO;
using Blog.Core.Middlewares;


namespace Blog.Core
{
    public class Startup
    {

        private const string ApiName = "Blog.Core";


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
            public void ConfigureServices(IServiceCollection services)
        {
            var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;

            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v0.1.0",
                    Title = "Blog.Core API",
                    Description = "框架说明文档",
                    TermsOfService = "None",
                    Contact = new Swashbuckle.AspNetCore.Swagger.Contact { Name = "Blog.Core", Email = "Blog.Core@xxx.com", Url = "https://www.jianshu.com/u/94102b59cc2a" }
                });

                //就是这里

                //就是这里（添加注释）
                var xmlPath = Path.Combine(basePath, "WebApplication4.xml");//这个就是刚刚配置的xml文件名
                c.IncludeXmlComments(xmlPath, true);//默认的第二个参数是false，这个是controller的注释，记得修改

                //var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                //var xmlPath = Path.Combine(basePath, "Blog.Core.xml");//这个就是刚刚配置的xml文件名
                //c.IncludeXmlComments(xmlPath, true);//默认的第二个参数是false，这个是controller的注释，记得修改


                //#region Token绑定到ConfigureServices
                ////添加header验证信息
                ////c.OperationFilter<SwaggerHeader>();
                //var security = new Dictionary<string, IEnumerable<string>> { { "Blog.Core", new string[] { } }, };
                //c.AddSecurityRequirement(security);
                ////方案名称“Blog.Core”可自定义，上下一致即可
                //c.AddSecurityDefinition("Blog.Core", new ApiKeyScheme
                //{
                //    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                //    Name = "Authorization",//jwt默认的参数名称
                //    In = "header",//jwt默认存放Authorization信息的位置(请求头中)
                //    Type = "apiKey"
                //});
                //#endregion

            });

            #endregion

            #region Authorize 权限认证三步走

            //#region 基于策略的授权（简单版）
            //// 1【授权】、这个和上边的异曲同工，好处就是不用在controller中，写多个 roles 。
            //// 然后这么写 [Authorize(Policy = "Admin")]
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("Client", policy => policy.RequireRole("Client").Build());
            //    options.AddPolicy("Admin", policy => policy.RequireRole("Admin").Build());
            //    options.AddPolicy("SystemOrAdmin", policy => policy.RequireRole("Admin", "System"));
            //});
            //#endregion

            #endregion


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //Configure方法是asp.net core程序用来具体指定如何处理每个http请求的
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //判断是否环境bian'lian'a
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            #region Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                //c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiHelp V1");
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiHelp V1");
                //c.RoutePrefix = "";//路径配置，设置为空，表示直接访问该文件，
                //路径配置，设置为空，表示直接在根域名（localhost:8001）访问该文件,注意localhost:8001/swagger是访问不到的，
                //这个时候去launchSettings.json中把"launchUrl": "swagger/index.html"去掉， 然后直接访问localhost:8001/index.html即可
            });
            #endregion

            #region 自定义认证中间件
            //自定义认证中间件
            //app.UseJwtTokenAuth(); //也可以app.UseMiddleware<JwtTokenAuth>();
            #endregion



            //#region Swagger

            //app.UseSwagger();
            //app.UseSwaggerUI(c =>
            //{

            //    //根据版本名称倒序 遍历展示
            //    typeof(ApiVersions).GetEnumNames().OrderByDescending(e => e).ToList().ForEach(version =>
            //    {
            //        c.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"{ApiName} {version}");
            //    });

            //    // 将swagger首页，设置成我们自定义的页面，记得这个字符串的写法：解决方案名.index.html
            //    c.IndexStream = () => GetType().GetTypeInfo().Assembly.GetManifestResourceStream("Blog.Core.index.html");//这里是配合MiniProfiler进行性能监控的，《文章：完美基于AOP的接口性能分析》，如果你不需要，可以暂时先注释掉，不影响大局。
            //    c.RoutePrefix = ""; //路径配置，设置为空，表示直接在根域名（localhost:8001）访问该文件,注意localhost:8001/swagger是访问不到的，去launchSettings.json把launchUrl去掉，如果你想换一个路径，直接写名字即可，比如直接写c.RoutePrefix = "doc";
            //});
            //#endregion


            //#region Authen
            ////app.UseMiddleware<JwtTokenAuth>();//注意此授权方法已经放弃，请使用下边的官方验证方法。但是如果你还想传User的全局变量，还是可以继续使用中间件
            //app.UseAuthentication();
            //#endregion


            //#region CORS
            ////跨域第二种方法，使用策略，详细策略信息在ConfigureService中
            //app.UseCors("LimitRequests");//将 CORS 中间件添加到 web 应用程序管线中, 以允许跨域请求。


            ////跨域第一种版本，请要ConfigureService中配置服务 services.AddCors();
            ////    app.UseCors(options => options.WithOrigins("http://localhost:8021").AllowAnyHeader()
            ////.AllowAnyMethod()); 
            //#endregion


            //// 跳转https
            //app.UseHttpsRedirection();
            //// 使用静态文件
            //app.UseStaticFiles();
            //// 使用cookie
            //app.UseCookiePolicy();
            //// 返回错误码
            //app.UseStatusCodePages();//把错误码返回前台，比如是404


            app.UseMvc();
        }
    }
}
