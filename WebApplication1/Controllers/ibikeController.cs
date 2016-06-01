using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Web.Mvc;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ibikeController : Controller
    {
        // GET: ibike
        public async Task<ActionResult> Index()
        {
            string targetURL = "http://ybjson01.youbike.com.tw:1002/gwjs.json";
            HttpClient client = new HttpClient();
            client.MaxResponseContentBufferSize = Int32.MaxValue;
            var response = await client.GetStringAsync(targetURL);
            var collection = JsonConvert.DeserializeObject<IEnumerable<Rootobject>>(response);
           
            return View(collection);
        }
    }
}