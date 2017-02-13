using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace DF_SOA
{
    /// <summary>
    /// DataDown 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://localhost.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class DataDown : System.Web.Services.WebService
    {
        [WebMethod(Description = "获取服务器时间")]
        public string getSDateTime() {
            return System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        [WebMethod(Description = "更新表")]
        public string getDownData(string t_name, string fdbs,string lastdt)
        {
            return DF_Controllers.getDownJson(t_name, fdbs, lastdt);
        }
        [WebMethod(Description = "分页方式更新表")]
        public string getDownDataForPage(string t_name, string fdbs,Int32 CurrentPage, Int32 PageSize, string lastdt)
        {
            return DF_Controllers.getDownJsonForPage(t_name, fdbs,CurrentPage, PageSize, lastdt);
        }
        [WebMethod(Description = "获得更新表的条目数")]
        public int getDownCount(string t_name, string fdbs, string lastdt)
        {
            return DF_Controllers.getDownCount(t_name,fdbs, lastdt);
        }
        [WebMethod(Description = "下载服务器站点文件，传递文件相对路径")]
        public byte[] getCongigFile(string df_version)
        {
            return DF_Controllers.DownloadFile(df_version);
        }
        [WebMethod(Description = "获取DataSet数据集合")]
        public DataSet getDownDS(string t_name, string fdbs)
        {
            return DF_Controllers.getDownDS(t_name, fdbs);
        }
        [WebMethod(Description = "得到储值卡集合")]
        public string GetICCard(string kkhcode)
        {
            Trace.WriteLine("检索储值卡传入参数-》：" + kkhcode);
            return DF_Controllers.GetICCard(kkhcode);
        }
        [WebMethod(Description = "支付完成更新服务端储值卡")]
        public int SetICCard(string jsons)
        {
            return DF_Controllers.SetICCard(jsons);
        }
        [WebMethod(Description = "后台微信或支付宝支付接口")]
        public bool Wpay(string fdbs, string lshh, string auth_code, double je, string pay_type) {
            return DF_Controllers.SendPay(fdbs, lshh, auth_code, je, pay_type);
        }
    }
}
