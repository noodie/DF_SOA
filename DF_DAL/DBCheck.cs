using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DF_DAL;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Data;

namespace DF_DAL
{
 public  class DBCheck
    {
        /// <summary>
        /// 执行表检查
        /// </summary>
        /// <param name="T_name"></param>
        /// <returns>表中明细条目数</returns>
        private static int TbaleCheck(string T_name)
        {
            string sql = "select count(*) from " + T_name;
            int t_count = 0;
            using (SqlCommand com = new SqlCommand(sql, DBconn.Conn))
            {
                try
                {
                    com.Connection.Open();
                    t_count = int.Parse(com.ExecuteScalar().ToString());
                }
                catch (Exception ex)
                {
                    Trace.TraceError("[" + T_name + "]出现异常:" + ex.Message);
                }
                finally
                {
                    com.Connection.Close();
                    com.Dispose();
                }
                //Trace.WriteLine(T_name+"::"+sql+"::"+t_count);
                return t_count;
            }
        }
        /// <summary>
        /// 执行语句检查
        /// </summary>
        /// <param name="T_name"></param>
        /// <returns>第一行第一列</returns>
        private static bool TsqlCheck(string T_sql)
        {
            string sql = "select top 1 * from (" + T_sql.ToLower().Replace(":fdbs", "'a01'").Replace(":down_time", "'2002-01-01 12:00:00'") + ") a";
            bool Tsql_r = false;
            using (SqlCommand com = new SqlCommand(sql, DBconn.Conn))
            {
                try
                {
                    com.Connection.Open();
                    com.ExecuteNonQuery();
                    Tsql_r = true;
                }
                catch (Exception ex)
                {
                    Trace.TraceError("[" + sql + "]出现异常:" + ex.Message);
                }
                finally
                {
                    com.Connection.Close();
                    com.Dispose();
                }
                  // Trace.WriteLine("执行TsqlCheck::" + sql + "::" + Tsql_r);
                return Tsql_r;
            }
        }
        /// <summary>
        /// 得到要上传的数据字段结构
        /// </summary>
        /// <param name="T_sql">上传的sql</param>
        /// <returns>返回上传字段集合List<string></returns>
        public static List<string> GetCol(string T_sql)
        {
            List<string> ColList = new List<string>();
            if (T_sql != string.Empty)
            {
                if (TsqlCheck(T_sql)) {
                    //为提高效率，直接返回结构不需要数据
                    string sql = "select  * from (" + T_sql.ToLower()+ ") a  where 1>2";
                    DataSet ds = Pubmethod.GetDataSet(sql);
                    foreach (DataColumn col in ds.Tables[0].Columns)
                    {
                        ColList.Add(col.ColumnName.ToLower());
                    }
                }
            }
           if(ColList.Count==0)
            {
                Trace.TraceError("得到字段错误T_sql::" + T_sql);
            }
            return ColList;
        }
        public static bool CheckDown(DownModel down)
        {
            bool Cr = false;
            if (down != null && down.T_name1 !="" && down.T_sql1 != null) {
                //down.T_count1 = TbaleCheck(down.T_name1);
                //if (down.T_count1 > 0)
                //{
                    if (TsqlCheck(down.T_sql1))
                    {
                        Cr = true;
                    }
               // }
            }
            return Cr;
        }
        public static bool CheckUp(UpModel up)
        {
                if (TsqlCheck(up.T_sql1))
                {
                    return true;
                }
            return false;
        }
    }
}
