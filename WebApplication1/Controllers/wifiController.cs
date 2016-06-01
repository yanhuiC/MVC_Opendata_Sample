using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Web.Mvc;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebApplication1.Models;
using System.Runtime.Caching;
using PagedList;

namespace WebApplication1.Controllers
{


    public class wifiController : Controller
    {
        // GET: wifi from opendata
        public async Task<ActionResult> Index(int? page, string districts,string types,string companys)
        {

            //districts
            ViewBag.Districts =
                await this.GetSelectList(await this.GetDistricts(), districts);
            ViewBag.selectedDistrict = districts;

            //types
           var typeselectList=
                 await this.GetSelectList(await this.GetWifiTypes(),types);
            ViewBag.Types = typeselectList.ToList();
            ViewBag.selectedType = types;

            //companys
            var companyselectList =
                await this.GetSelectList(await this.GetCompany(), companys);
            ViewBag.Companys = companyselectList.ToList();
            ViewBag.selectCompay = companys;

            var source = await this.GetWifiData();
            source = source.AsQueryable();

            if(!string.IsNullOrWhiteSpace(districts))
            {
                source = source.Where(a => a.district == districts);
            }         

            if (!string.IsNullOrWhiteSpace(types))
            {
                source = source.Where(a => a.type == types);
            }

            if (!string.IsNullOrWhiteSpace(companys))
            {
                source = source.Where(a => a.company == companys);
            }

            int pageIndex = page ?? 1;
            int pageSize = 10;
            int totalCount = 0;
            totalCount = source.Count();
            source = source.OrderBy(a => a.district).Skip((pageIndex - 1) * pageSize).Take(pageSize);

            var pageResult = new StaticPagedList<wifi>(source, pageIndex, pageSize, totalCount);

            return View(pageResult);
        }

        private async Task<IEnumerable<wifi>> GetWifiData()
        {
            string cacheName = "WIFI_LIST";
            ObjectCache cache = MemoryCache.Default;
            CacheItem cacheContencts = cache.GetCacheItem(cacheName);
            if (cacheContencts == null)
            {
                return await RetriveWifiData(cacheName);
            }
            else
            {
                return cacheContencts.Value as IEnumerable<wifi>;
            }

        }

        private async Task<IEnumerable<wifi>> RetriveWifiData(string cacheName)
        {
            string targetURL = "http://data.ntpc.gov.tw/od/data/api/04958686-1B92-4B74-889D-9F34409B272B?$format=json";
            HttpClient client = new HttpClient();
            client.MaxResponseContentBufferSize = Int32.MaxValue;
            var response = await client.GetStringAsync(targetURL);

            var collection = JsonConvert.DeserializeObject<IEnumerable<wifi>>(response);

            CacheItemPolicy policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTime.Now.AddMinutes(30);

            ObjectCache cacheItem = MemoryCache.Default;
            cacheItem.Add(cacheName, collection, policy);

            return collection;
        }
       
        
        /// <summary>
        /// 取得區域LIST
        /// </summary>
        private async Task<List<string>> GetDistricts()
        {
            var source = await this.GetWifiData();
            if (source != null)
            {
                var districts = source.OrderBy(a => a.district).Select(a => a.district).Distinct();
                return districts.ToList();
            }
            return new List<string>();
        }

        /// <summary>
        /// 取得WIFI type LIST
        /// </summary>
        /// <returns></returns>
        private async Task<List<string>> GetWifiTypes()
        {
            var source = await this.GetWifiData();
            if(source != null)
            {
                var types = source.OrderBy(a => a.type).Select(a => a.type).Distinct();
                return types.ToList();
            }
            return new List<string>();
        }

        /// <summary>
        /// 取得 業者 LIST
        /// </summary>
        /// <returns></returns>
        private async Task<List<string>> GetCompany()
        {
            var source = await this.GetWifiData();
            if (source != null)
            {
                var companys = source.OrderBy(a => a.company).Select(a => a.company).Distinct();
                return companys.ToList();
            }
            return new List<string>();
            
        }

        //getlist function
        private async Task<IEnumerable<SelectListItem>> GetSelectList(IEnumerable<string> source, string selectedItem)
        {
            var selectlist = source.Select(item => new SelectListItem()
            {
                Text = item,
                Value = item,
                Selected = !string.IsNullOrWhiteSpace(selectedItem)
                             && item.Equals(selectedItem, StringComparison.OrdinalIgnoreCase)

            });
            return selectlist;
        }
    }


}
