using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApiSample.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        //public JsonResult<string,string> Get()
        public JsonResult Get()
        {
            var result = new Dictionary<string,string>(){{"value","aaa"}};
            return Json(result);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public JsonResult Post([FromBody]Dictionary<string,string> param)
        {
            var data = new Dictionary<string,string>();
            
            foreach (var key in param.Keys)
            {
                data.Add(string.Format("param_{0}",key),param[key]);
            }

            var result = new Dictionary<string,Dictionary<string,string>>();
            result.Add("value",data);

            return Json(result);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
