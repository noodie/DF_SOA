using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;

namespace DF_DAL
{
    public class DBconn
    {
        public static SqlConnection Conn       
            //设置一个属性，用来获取和数据库的连接，也就是SqlConnection
        {
            get
            {   //以下注释的是使用XML读取配置方法 已废弃
               // {"未能找到路径“E:\\coder\\DFED\\DFYBsoft\\DFYBsoft\\bin\\DFmanager\\config.xml”的一部分。"}
              //  XmlDocument xml = new XmlDocument();
                //Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()).ToString()+"/DFmanager/config.xml"
              //  xml.Load(Application.ExecutablePath + ".config");
             //   XmlNode node = xml.SelectSingleNode("configuration");
                //Data Source=;Initial Catalog=;User Id=sa;Password=123456;  
                string ConnString = "Data Source=" + ConfigurationManager.AppSettings["servername"].ToString()
                    + ";Initial Catalog=" + ConfigurationManager.AppSettings["dataname"].ToString()
                    + ";User ID=" + ConfigurationManager.AppSettings["username"].ToString()
                    + ";Password=" + ConfigurationManager.AppSettings["password"].ToString();
              //  MessageBox.Show(ConnString);
                return new SqlConnection(ConnString);
            }
        } 
          
  }
}
