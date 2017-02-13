using DF_DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DF_DAL
{
    //作为默认的下载类，会有3个方法，之后可继承，可重写方法。
   public class DownAction
    {
       /*  不再控制构造函数
       private DownModel down ;//初始化参数

        /// <summary>
        /// 只能带参数进行初始化构造方法
        /// </summary>
        /// <param name="downs"></param>
        /// <param name="fdbs"></param>
        public DownAction(DownModel downs,string fdbs) {
            this.down = downs;
        }*/
        /// <summary>
        /// 下载方法不分页
        /// </summary>
        /// <param name="down"></param>
        /// <returns>json</returns>
        public string DownJson(DownModel down) {
            DataSet ds = new DataSet();
            ds = Pubmethod.GetDataSet(down.T_sql1, down.T_name1);
            return JsonTools.DsToJson(ds, down.T_name1);
        }
        /// <summary>
        /// 下载方法分页
        /// </summary>
        /// <param name="down">下载模板</param>
        /// <param name="CurrentPage">页码</param>
        /// <param name="pagesize">页条目数</param>
        /// <returns></returns>
        public string DownJson(DownModel down, int CurrentPage, int pagesize)
        {
            DataSet ds = new DataSet();
            ds = Pubmethod.GetDataSet(down.T_sql1, down.T_name1,CurrentPage, pagesize);
            return JsonTools.DsToJson(ds, down.T_name1);
        }
        public DataSet DownSet(DownModel down)
        {
        
            DataSet ds = new DataSet();
            if (DBCheck.CheckDown(down))
            {
                ds = Pubmethod.GetDataSet(down.T_sql1);
            }
            return ds;
        }
    }
}


