using DF_DAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace DF_DAL
{
   public  class SqlDmo
    {
        /// <summary>
        /// 得到数据库表结构
        /// </summary>
        /// <param name="res">解析数组</param>
        /// <returns>类型,字段名,长度,默认值,精度</returns>
        public Dictionary<string,string> GetSqlCol(string tbname,string colname)
        {
            SqlConnection conn = DBconn.Conn;
            /* string[] restrictions = new string[4];  规则数组 有4个值
             restrictions[1] = "dbo";    拥友者
             restrictions[2] = "spkfk";  表名，
             restrictions[3] = "spid";   字段名 */
            string[] res = new string[4];
            res[1] = "dbo";
            res[2] = tbname;
            res[3] = colname;
            DataTable sqldt = new DataTable();
            Dictionary<string, string> sqlcol = new Dictionary<string, string>();
            try
            {
                conn.Open();
                sqldt = conn.GetSchema("Columns", res);
            }
            catch (Exception e)
            {
                string ex = string.Format("获取表[{0}.{1}]结构出现异常:" + e.Message, res[2], res[3]);
                Trace.TraceError(ex);
            }
            finally {
                conn.Close();
            }
            if (sqldt != null && sqldt.Rows.Count > 0)
            {
                sqlcol.Add("D_type", sqldt.Rows[0]["DATA_TYPE"].ToString());//类型
                sqlcol.Add("D_name", sqldt.Rows[0]["COLUMN_NAME"].ToString().ToLower());//字段名
                sqlcol.Add("D_len", sqldt.Rows[0]["CHARACTER_MAXIMUM_LENGTH"].ToString());//长度
                sqlcol.Add("D_def", sqldt.Rows[0]["COLUMN_DEFAULT"].ToString());//默认值
                sqlcol.Add("D_SCALE", sqldt.Rows[0]["NUMERIC_SCALE"].ToString());//小数位数
               // Trace.WriteLine("获取表"+ res[2] + "."+ res[3] + "结构成功::");
            }
            else {
                string ex = string.Format("获取表[{0}.{1}]结构为空: ", res[2],res[3]);
                Trace.TraceError(ex);
            }
            return sqlcol;
        }
        /// <summary>
        /// 对解析过的json数据进行检查，并对有问题数据进行一致性修改,保证能插如到数据库里
        /// </summary>
        /// <param name="JM">解析的Json数据生成的JosnModel实例</param>
        /// <param name="UMList">解析上传配置文件的数据生成的上传实例集合</param>
        /// <returns></returns>
        public bool CheckJson(JsonModel JM,List<UpModel> UMList)
        {
            string[] s1 = { "char", "varchar" };
            string[] s2 = { "int", "decimal", "numeric", "float" };
            string[] s3 = { "date", "datetime" };
            try
            {
                foreach (UpModel u in UMList)
                {
                    if (JsonTools.ListContainsKey(JM.JsonKeys(), u.T_name1))
                    {
                        List<string> sqlcol = DBCheck.GetCol(u.T_sql1);
                        u.Col_list = sqlcol;
                        foreach (string colname in sqlcol)
                        {
                            //得到数据库表结构
                            Dictionary<string, string> dsql = GetSqlCol(u.T_name1,colname);
                            //设置默认值
                            string def = dsql["D_def"].Replace("'", "").Replace("(", "").Replace(")", "");
                            if (def != string.Empty)
                            {
                                dsql["D_def"] = def;
                            }
                           else  if (s1.Contains(dsql["D_type"]))
                                dsql["D_def"]=string.Empty;
                            else if (s2.Contains(dsql["D_type"]))
                                dsql["D_def"] = "0";
                            else if (s3.Contains(dsql["D_type"]))
                                dsql["D_def"] = DateTime.Now.Date.ToString("yyyy-MM-dd");
                            //获取数据列表
                            JM.Tb_list = JM.Jsons[u.T_name1];
                            if (JM.Tb_list.Count == 0) {
                                Trace.TraceError("CheckJson获取表"+ u.T_name1 + "数据为空！");
                                throw new Exception();
                            }
                            foreach (MyDic<string,string> datadic in JM.Tb_list)
                            {
                                JM.Tb_dic = datadic;
                                //检查json时候如果json数据存在这一列就添加 
                                //不存在就取默认值
                                if (JM.Tb_dic.ContainsKey(colname)) {
                                    JM.SetData(JM, dsql);
                                }
                                else {
                                    int index = JM.Tb_list.IndexOf(datadic);
                                        JM.Jsons[u.T_name1][index].Add(colname, dsql["D_def"]);
                                    }
                                //如果数据正常则判断是否为日清表pos_mast  是的话就纪录lshh
                                if (u.T_name1 == "pos_mast" && JM.Tb_dic.ContainsKey("lshh")
                                    && colname=="lshh")
                                {
                                    //判断lshh存在不存在,不存在就增加
                                    if (!JM.Lshh_list.Contains(JM.Tb_dic["lshh"]))
                                    {
                                        JM.Lshh_list.Add(JM.Tb_dic["lshh"]);
                                        Trace.WriteLine("增加日清流水号成功，流水号为：" + JM.Tb_dic["lshh"]);
                                    }
                                }
                            }
                        }
                    }
          
                }
                return true;
            }
            catch(Exception ex){
                Trace.TraceError("CheckJson检查数据出错！::"+ex);
                return false;
            } 
        }
        /// <summary>
        /// 自动生成insert into sql语句
        /// </summary>
        /// <param name="JM">解析的Json数据生成的JosnModel实例</param>
        /// <param name="UMList">解析上传配置文件的数据生成的上传实例集合</param>
        /// <returns>插入sql语句</returns>
        public string InsSql(JsonModel JM, List<UpModel> UMList)
        {
            string inssql = "";
            try
            {
                foreach (UpModel u in UMList)
                {
                    if (JsonTools.ListContainsKey(JM.JsonKeys(), u.T_name1))
                    {
                        JM.Tb_list = JM.Jsons[u.T_name1];
                        List<string> sqlcol = DBCheck.GetCol(u.T_sql1);
                        foreach (MyDic<string, string> datadic in JM.Tb_list)
                        {
                            string sql = u.Ins_sql1;
                            foreach (string colname in sqlcol)
                            {
                                sql += "'" + datadic[colname] + "',";
                            }
                            sql = sql.Substring(0, sql.Length - 1) + ");";
                            inssql += sql;
                        }
                    }
                }
                Trace.WriteLine("insert sql::" + inssql);
            }
            catch (Exception ex) {
                Trace.TraceError("InsSql生成insert into 语句出错！::" + ex);
            }
             return inssql;
        }

        public ArrayList InsSqlArr(JsonModel JM, List<UpModel> UMList)
        {
            ArrayList arrsql = new ArrayList();
            string inssql = "";
            try
            {
                foreach (UpModel u in UMList)
                {
                    if (JsonTools.ListContainsKey(JM.JsonKeys(), u.T_name1))
                    {
                        JM.Tb_list = JM.Jsons[u.T_name1];
                        List<string> sqlcol = u.Col_list;//DBCheck.GetCol(u.T_sql1);
                        foreach (MyDic<string, string> datadic in JM.Tb_list)
                        {
                            string sql = u.Ins_sql1;
                            foreach (string colname in sqlcol)
                            {
                                sql += "'" + datadic[colname] + "',";
                            }
                            sql = sql.Substring(0, sql.Length - 1) + ");";
                            inssql += sql;
                            arrsql.Add(sql);
                        }
                    }
                }
                 Trace.WriteLine("insert sql::" + inssql);
            }
            catch (Exception ex)
            {
                Trace.TraceError("InsSql生成insert into 语句出错！::" + ex);
            }
            return arrsql;
        }
    }
}
