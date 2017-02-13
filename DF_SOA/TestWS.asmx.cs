/*
 * The Following Code was developed by Dewald Esterhuizen
 * View Documentation at: http://localhost.com
 * Licensed under Ms-PL 
*/
using DF_SOA;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace TestWS
{
    [WebService(Namespace = "http://localhost.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class TestWebService : System.Web.Services.WebService
    {
        [WebMethod(Description ="hello world")]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod(Description = "求和的方法")]
        public double addition(double i, double j)
        {
            return i + j;
        }
        [WebMethod(Description = "更新表")]
        public string getDownData(string t_name, string fdbs)
        {
            return DF_Controllers.getDownJson(t_name, fdbs,new DateTime().ToString());
        }
        [WebMethod(Description = "获得更新表的条目数")]
        public int getDownCount(string t_name)
        {
            return DF_Controllers.getDownCount(t_name);
        }
        [WebMethod(Description = "下载服务器站点文件，传递文件相对路径")]
        public byte[] getCongigFile(string df_version) {
            return DF_Controllers.DownloadFile(df_version);
        }
        }
}