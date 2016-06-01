using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace WebApplication1.Models
{
    public class wifi
    {
        [Display(Name = "熱點id")]
        public string id { get; set; }

        [Display(Name = "熱點名稱")]
        public string spot_name { get; set; }

        [Display(Name = "熱點類別")]
        public string type { get; set; }

        [Display(Name = "業者")]
        public string company { get; set; }

        [Display(Name = "鄉鎮市區")]
        public string district { get; set; }

        [Display(Name = "地址")]
        public string address { get; set; }

        [Display(Name = "機關構名稱")]
        public string apparatus_name { get; set; }

        [Display(Name = "緯度")]
        public string latitude { get; set; }

        [Display(Name = "經度")]
        public string longitude { get; set; }
    }

}
