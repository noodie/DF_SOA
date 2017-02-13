using DF_DAL;
using DF_Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Xml;

namespace DF_SOA
{
    public class DF_Controllers
    {
        const string ASSEMBLY_NAME = "DF_DAL";
        static readonly DateTime DateTimeDef = DateTime.Parse("2000-01-01 00:00:00");


        /// <summary>
        /// 日期处理函数
        /// </summary>
        /// <param name="strDate"></param>
        /// <returns></returns>
        public static DateTime Isdate(string strDate) {
            DateTime dtDate;
            if (DateTime.TryParse(strDate, out dtDate))
            {
                return dtDate;
            }
            else
            {
                return DateTimeDef;
            }

        }
          

/// <summary>
/// 获取下载数据
/// </summary>
/// <param name="t_name">下载表</param>
/// <param name="fdbs">门店标识</param>
/// <returns></returns>
public static string getDownJson(string t_name,string fdbs, string lastdt)
        { 
            List<DownModel> DownL = DfConfigTools.GetDownObj(getCongig("DF_DownLoad"));
            DownModel downp = new DownModel();
            foreach (DownModel d in DownL)
            {
                if (d.T_name1.ToLower() == t_name.ToLower())
                {
                    downp = d;
                }
            }
             if(DBCheck.CheckDown(downp)) { 
          //  if (!string.IsNullOrEmpty(downp.T_sql1)) {
                string s = ASSEMBLY_NAME + "." + downp.T_action1;
                //通过自动初始化一个类，创建实例，
                DownAction DownA = (DownAction)Assembly.Load(ASSEMBLY_NAME).CreateInstance(
                  s, false,
                     BindingFlags.Default, null,null, null, null);
                downp.T_sql1=downp.T_sql1.ToLower().Replace(":fdbs", "'" + fdbs + "'").Replace(":down_time", "'" + lastdt + "'");
                return DownA.DownJson(downp);
            }
            else
            {
                Trace.WriteLine("函数名:getDownJson表：[" + t_name +"]下载失败，没有找到相应配置！");
                return "";
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="t_name"></param>
        /// <param name="fdbs"></param>
        /// <returns></returns>
        public static string getDownJsonForPage(string t_name, string fdbs,int CurrentPage,int pagesize,string lastdt)
        {
            List<DownModel> DownL = DfConfigTools.GetDownObj(getCongig("DF_DownLoad"));
            DownModel downp = new DownModel();
            foreach (DownModel d in DownL)
            {
                if (d.T_name1.ToLower() == t_name.ToLower())
                {
                    downp = d;
                }
            }
            if (DBCheck.CheckDown(downp))
           // if (!string.IsNullOrEmpty(downp.T_sql1))
             {
                string s = ASSEMBLY_NAME + "." + downp.T_action1;
                //通过自动初始化一个类，创建实例，
                DownAction DownA = (DownAction)Assembly.Load(ASSEMBLY_NAME).CreateInstance(
                  s, false,
                     BindingFlags.Default, null, null, null, null);
                downp.T_sql1 = downp.T_sql1.ToLower().Replace(":fdbs", "'" + fdbs + "'").Replace(":down_time", "'" + lastdt + "'");
                return DownA.DownJson(downp,CurrentPage,pagesize);
            }
            else
            {
                Trace.WriteLine("函数名:getDownJsonForPage表：[" + t_name + "]下载失败，没有找到相应配置！");
                return "";
            }
        }
        /// <summary>
        /// 获取表里的数据条目数
        /// </summary>
        /// <param name="t_name">表名</param>
        /// <returns></returns>
        public static  int getDownCount(string t_name) {
            List<DownModel> DownL = DfConfigTools.GetDownObj(getCongig("DF_DownLoad"));
            DownModel downp = new DownModel();
            foreach (DownModel d in DownL)
            {
                if (d.T_name1 == t_name.ToLower())
                {
                    downp = d;
                }
            }
                if (DBCheck.CheckDown(downp))
                {
                    return downp.T_count1;
                }
            else
            {
                Trace.WriteLine("函数名:getDownCount表：[" + t_name + "]下载失败，没有找到相应配置！");
                return 0;
            }
        }
        /// <summary>
        /// 获取要下载的数据条目数
        /// </summary>
        /// <param name="t_name">表名</param>
        /// <param name="fdbs">机构标识</param>
        /// <returns></returns>
        public static int getDownCount(string t_name, string fdbs, string lastdt)
        {
            List<DownModel> DownL = DfConfigTools.GetDownObj(getCongig("DF_DownLoad"));
            DownModel downp = new DownModel();
            foreach (DownModel d in DownL)
            {
                if (d.T_name1 == t_name.ToLower())
                {
                    downp = d;
                }
            }
            if (!string.IsNullOrEmpty(downp.T_sql1)) {
            downp.T_sql1 = downp.T_sql1.ToLower().Replace(":fdbs", "'" + fdbs + "'");
            downp.T_sql1 = downp.T_sql1.ToLower().Replace(":down_time", "'" + lastdt + "'");
                return Pubmethod.GetDataCount(downp.T_sql1);
            }
            else
            {
                Trace.WriteLine("函数名:getDownCount表：[" + t_name + "]下载失败，没有找到相应配置！");
                return 0;
            }
        }
        /// <summary>
        /// 得到项目传输配置节点数据集合
        /// </summary>
        /// <param name="node">节点名称</param>
        /// <returns>节点数据集合</returns>
        public static XmlNodeList getCongig(string node) {
            string configPath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["DF_path"].ToString());
           // Trace.WriteLine(configPath);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(configPath);
            XmlNodeList xn = xmlDoc.DocumentElement.ChildNodes;
            XmlNodeList xn1 = null;
            foreach (XmlNode singleXmlNode in xn)
            {
                if (singleXmlNode.Name == node)
                {
                    xn1 = singleXmlNode.ChildNodes;
                }
            }
                    return xn1;
        }
        /// <summary>
        /// 下载服务器上的文件
        /// </summary>
        /// <returns></returns>
        public static  byte[] DownloadFile(string df_version)
        {
            FileStream fs = null;
            System.IO.MemoryStream tempStream = new System.IO.MemoryStream();
            string CurrentUploadFilePath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["DF_path"].ToString());
            if (File.Exists(CurrentUploadFilePath))
            {
                try
                {
                    ///打开现有文件以进行读取。
                    fs = File.OpenRead(CurrentUploadFilePath);
                    int b1;
                    while ((b1 = fs.ReadByte()) != -1)
                    {
                        tempStream.WriteByte(((byte)b1));
                    }
                }
                catch (Exception ex)
                {
                    Trace.WriteLine("::更新配置::" + ex);
                }
                finally
                {
                    fs.Close();
                }
            }
            return tempStream.ToArray();
        }
        /// <summary>
        /// 数据上传操作
        /// </summary>
        /// <param name="jsons"></param>
        /// <returns></returns>
        public static int DataUp(string jsons) {
            UpAction up = new UpAction();
            List<UpModel> UpL = DfConfigTools.GetUpObj(getCongig("DF_UpLoad"));
                return up.Insert(jsons, UpL);
        }
        public static string GetMaxlshh(string fdbs, string sktid) {
            string sql = "select max(lshh) from (select max(lshh) as lshh from retmast where fdbs='" + fdbs + "' and sktid='" + sktid + "'"
                        + " union all select max(lshh) from pos_mast  where fdbs='" + fdbs + "' and sktid='" + sktid + "'"
                          + " ) a";
            object ret = "";
            try
            {
                ret = Pubmethod.ExecuteSca(sql);
                if (string.IsNullOrEmpty((string)ret))
                {
                    ret = "";
                }
            }
            catch(Exception e) {
                Trace.WriteLine("获取后台最大流水号出现异常，异常原因：" +e.Message);
            }
            Trace.WriteLine("获取后台最大流水号，流水号为："+ret);
            return ret.ToString();
        }
        public static DataSet getDownDS(string t_name, string fdbs)
        {
            List<DownModel> DownL = DfConfigTools.GetDownObj(getCongig("DF_DownLoad"));
            DownModel downp = new DownModel();
            foreach (DownModel d in DownL)
            {
                if (d.T_name1 == t_name)
                {
                    downp = d;
                }
            }
                string s = ASSEMBLY_NAME + "." + downp.T_action1;
                //通过自动初始化一个类，创建实例，
                DownAction DownA = (DownAction)Assembly.Load(ASSEMBLY_NAME).CreateInstance(
                  s, false,
                     BindingFlags.Default, null, null, null, null);
            downp.T_sql1 = downp.T_sql1.ToLower().Replace(":fdbs", "'" + fdbs + "'");
            downp.T_sql1 = downp.T_sql1.ToLower().Replace(":down_time", "'" + DateTimeDef + "'");
            return DownA.DownSet(downp);
        }
        public static string GetICCard(string value)
        {
            string ICsql = ConfigurationManager.AppSettings["CardSql"].ToString();
            ICsql = ICsql.Replace("@value", "'" + value + "'");
            return JsonTools.DsToJson(Pubmethod.GetDataSet(ICsql));
        }
        public static int SetICCard(string value)
        {
           MyDic<string, string> cardic = new MyDic<string, string>();
          cardic=JsonTools.JsonToObject(value);
           return Pubmethod.SetICCard(cardic);
        }

        public static bool SendPay(string fdbs, string lshh, string auth_code, double je, string pay_type) {
            bool breturn = false;
            try
            {
                PayAction pa = new PayAction();
                PayModel pm = pa.PaySet(fdbs, lshh, auth_code, je, pay_type);
                string sbure = pa.PaySubmit(pm);
                MyDic<string, string> dreturn = JsonTools.JsonToObject(sbure);
                if (dreturn["code"] == "0")
                {
                    if (dreturn["trade_state"] == "1")
                    {
                        breturn = true;
                    }
                }
            }
            catch(Exception e)
            {
                Trace.WriteLine("支付失败，异常原因：" + e.Message);
            }
            return breturn;
        }
    }
}