using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using static Newtonsoft.Json.Linq.Extensions;

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
        public JsonResult Post([FromBody]Dictionary<string,object> param)
        {
            var data = new Dictionary<string,object>();
            
            foreach (var key in param.Keys)
            {
                // 戻り値のKey名を設定
                var returnName = string.Format("param_{0}",key);
                var returnValue = string.Empty;

                switch(param[key]){
                    case string s:
                        returnValue = string.Format("string:[{0}]", s);
                        break;
                    case double d:
                        returnValue = string.Format("double:[{0}]", d);
                        break;
                    //case int i:
                    case Int64 i:
                        returnValue = string.Format("int:[{0}]", i);
                        break;
                    case Newtonsoft.Json.Linq.JArray j:
                        var sb = new StringBuilder();
                        foreach(var i in j){
                            sb.Append(getValue(i));
                            sb.Append(",");
                        }
                        if(sb.Length > 0){
                            sb.Remove(sb.Length - 1,1);
                        }
                        returnValue = string.Format("JArray:[{0}]", sb.ToString());
                        break;
                    case object o:
                        returnValue = string.Format("object:[{0}]", o);
                        break;
                }

                data.Add(returnName, returnValue);
            }

            string getValue(Newtonsoft.Json.Linq.JToken value) {
                switch(value.Type) {
                    case Newtonsoft.Json.Linq.JTokenType.String:
                        return string.Format("string:{0}", value.Value<string>());
                    case Newtonsoft.Json.Linq.JTokenType.Float:
                        return string.Format("double:{0}", value.Value<double>());
                    case Newtonsoft.Json.Linq.JTokenType.Integer:
                        return string.Format("int:{0}", value.Value<Int64>());
                    default:
                        return string.Format("object:{0}", value);
                }
            }

            var result = new Dictionary<string,Dictionary<string,object>>();
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
