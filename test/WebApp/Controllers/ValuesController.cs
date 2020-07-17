using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NLog;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using WebApp.Services;
using Sino.Web.Filter;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private static readonly Logger log = LogManager.GetCurrentClassLogger();

        private IValueService _service;

        public class a
        {
            public string message { get; set; }
            public DateTime date { get; set; }
        }

        public ValuesController(IValueService service)
        {
            _service = service;
        }

        // GET api/values
        [HttpGet]
        [OutputLog]
        public async Task<IEnumerable<a>> Get()
        {
            Stopwatch watch = Stopwatch.StartNew();
            log.Error(new ArgumentNullException("id"), JsonConvert.SerializeObject(new a
            {
                message = "看看能不能支持中文",
                date = DateTime.Now
            }));
            watch.Stop();
            return new a[] { new a { message = "test", date = DateTime.Now }, new a { message = "ffee", date = DateTime.Now } };
        }

        // GET api/values/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<object> Get(int id)
        {
            _service.Get();
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
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
