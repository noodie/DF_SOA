using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace DF_SOA
{
    /// <summary>
    /// DataUp 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class DataUp : System.Web.Services.WebService
    {

        [WebMethod(Description = "上传json格式数据")]
        public int SetUpDate(string jsons)
        {
            return DF_Controllers.DataUp(jsons);
        }

        [WebMethod(Description = "获取后台最大流水号")]
        public string GetMaxLshh(string fdbs,string sktid)
        {
            return DF_Controllers.GetMaxlshh(fdbs, sktid);
        }
    }
}
