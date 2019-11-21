using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;//引用
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Blog.Core.Model;
using Microsoft.AspNetCore.Authorization;

namespace Blog.Core.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]//路由规则
    [ApiController]
    public class ValuesController : ControllerBase
    {

        /// <summary>
        /// 测试路由
        /// </summary>
        /// <returns></returns>
        /// 
        //[ApiExplorerSettings(IgnoreApi = true)]//隐藏swagger中接口显示
        [HttpGet("all")]
        [Authorize(Roles ="Admin")]//设置角色

        //
        public string GetProducts()
        {
            //return new JsonResult(new List<Products>
            //{
            //    new Products
            //    {
            //        ID = 1,
            //        Name = "牛奶",
            //        Price = 5

            //    },
            //    new Products
            //    {
            //        ID = 2,
            //        Name = "面包",
            //        Price = 2

            //    }

            //});

            return "xxxxxxxxxxxxxx";
        }


        /// <summary>
        /// 自带Get
        /// </summary>
        /// <returns></returns>
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}", Name  = "GetTodo")] //name:路由别名
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// 自带Post
        /// </summary>
        /// <param name="value"></param>
        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        /// <summary>
        /// 自带Put
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        /// <summary>
        /// 自带Delete
        /// </summary>
        /// <param name="id"></param>
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
