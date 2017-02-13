using DF_DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Xml;


namespace DF_DAL
{
    public  class DfConfigTools
    {
        /// <summary>
        /// 读取配置文件
        /// </summary>
        /// <param name=""></param>
        /// <returns>返回需要下载的表相关操作</returns>
        public static List<DownModel> GetDownObj(XmlNodeList xn)
        {
            List<DownModel> DownM = new List<DownModel>();
            // ResourceManager rs = new ResourceManager("DF_SOA", Assembly.GetEntryAssembly());
            //            string configPath =
            //                System.Web.MapPath("ClientUpload/") +
            //+ConfigurationManager.AppSettings["DF_path"].ToString();
            //            // Directory.GetParent(Directory.GetCurrentDirectory()).ToString()+ "/DF_LocalResources/DF_SOA.config";
            //            Trace.WriteLine(configPath);
            //            XmlDocument xmlDoc = new XmlDocument();
            //            xmlDoc.Load(configPath);
            //            XmlNodeList xn = xmlDoc.DocumentElement.ChildNodes;  
            //            foreach (XmlNode singleXmlNode in xn)
            //            {
            //                if (singleXmlNode.Name == "DF_DownLoad")
            //                {
            //                    XmlNodeList xnl = singleXmlNode.ChildNodes;
                    foreach (XmlNode xn2 in xn)
                    {
                        DownModel down = new DownModel();
                        XmlNodeList xnl0 = xn2.ChildNodes;
                      // Trace.WriteLine("T_name:" + xnl0.Item(0).InnerText);
                        down.T_name1 = xnl0.Item(0).InnerText;
                        down.T_sql1 = xnl0.Item(1).InnerText;
                        down.T_action1 = xnl0.Item(2).InnerText;
                        down.T_count1 = 0; 
                        DownM.Add(down);
                    }
            return DownM;
        }
        /// <summary>
        /// 读取配置文件,测试使用
        /// </summary>
        /// <param name=""></param>
        /// <returns>返回需要上传的表相关操作</returns>
        public static  List<UpModel> GetUpObj(XmlNodeList xn)
        {
            List<UpModel> UpM = new List<UpModel>();
            //string configPath = Directory.GetCurrentDirectory() + ConfigurationManager.AppSettings["DF_path"].ToString();
            //// Directory.GetParent(Directory.GetCurrentDirectory()).ToString()+ "/DF_LocalResources/DF_SOA.config";
            //Trace.WriteLine(configPath);
            //XmlDocument xmlDoc = new XmlDocument();
            //xmlDoc.Load(configPath);
            //XmlNodeList xn = xmlDoc.DocumentElement.ChildNodes;
            //foreach (XmlNode singleXmlNode in xn)
            //{
            //    if (singleXmlNode.Name == "DF_UpLoad")
            //    {
            //        XmlNodeList xnl = singleXmlNode.ChildNodes;
                    foreach (XmlNode xn2 in xn)
                    {
                        UpModel up = new UpModel();
                        XmlNodeList xnl0 = xn2.ChildNodes;
                        Trace.WriteLine("T_name:" + xnl0.Item(0).InnerText);
                        up.T_name1 = xnl0.Item(0).InnerText;
                        up.T_sql1 = xnl0.Item(1).InnerText;
                        up.T_action1 = xnl0.Item(2).InnerText;
                         // if (TsqlCheck(up.T_sql1))
                         //   {
                               UpM.Add(up);
                         //   }
                    }
            return UpM;
        }
        public static List<UpModel> GetUpObj()
        {
            List<UpModel> UpM = new List<UpModel>();
            string configPath = "E:\\coder\\DFED\\DF_SOA\\DF_SOA" + ConfigurationManager.AppSettings["DF_path"].ToString();
            //// Directory.GetParent(Directory.GetCurrentDirectory()).ToString()+ "/DF_LocalResources/DF_SOA.config";
            //Trace.WriteLine(configPath);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(configPath);
            XmlNodeList xn = xmlDoc.DocumentElement.ChildNodes;
            foreach (XmlNode singleXmlNode in xn)
            {
                if (singleXmlNode.Name == "DF_UpLoad")
                {
                    XmlNodeList xnl = singleXmlNode.ChildNodes;
                    foreach (XmlNode xn2 in xnl)
                    {
                        UpModel Up = new UpModel();
                        XmlNodeList xnl0 = xn2.ChildNodes;
                        Trace.WriteLine("T_name:" + xnl0.Item(0).InnerText);
                        Up.T_name1 = xnl0.Item(0).InnerText;
                        Up.T_sql1 = xnl0.Item(1).InnerText;
                        Up.T_action1 = xnl0.Item(2).InnerText;     
                        UpM.Add(Up);
                    }
                }
            }
            return UpM;
        }
      
    }

}
