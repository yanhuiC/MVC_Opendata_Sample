using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace WebApplication1.Models
{
    public class Rootobject
    {
        public int retCode { get; set; }
        public ibike retVal { get; set; }
    }

   
    public class ibike
    {
       
            [Display(Name = "熱點代碼")]
            public string sno { get; set; }

            [Display(Name = "場站名")]
            public string sna { get; set; }

            [Display(Name = "場站總停車格")]
            public string tot { get; set; }

            [Display(Name = "場站目前車輛數量")]
            public string sbi { get; set; }

            [Display(Name = "場站區域")]
            public string sarea { get; set; }

            [Display(Name = "資料更新時間")]
            public string mday { get; set; }

            [Display(Name = "緯度")]
            public string lat { get; set; }

            [Display(Name = "經度")]
            public string lng { get; set; }

            [Display(Name = "地址")]
            public string ar { get; set; }

            [Display(Name = "場站區域")]
            public string sareaen { get; set; }

            [Display(Name = "場站名稱")]
            public string snaen { get; set; }

            [Display(Name = "地址")]
            public string aren { get; set; }

            [Display(Name = "空位數量")]
            public string bemp { get; set; }

            [Display(Name = "全站禁用狀態")]
            public string act { get; set; }

    }
}
