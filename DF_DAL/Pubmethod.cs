using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Diagnostics;

namespace DF_DAL
{
    public class Pubmethod
    {
            #region 执行SQL语句返回DataSet数据集的方法
            public static DataSet GetDataSet(string sql)
            {
            // MessageBox.Show(sql);
            int count = 0;
                DataSet ds = new DataSet();
                SqlCommand com = new SqlCommand(sql,DBconn.Conn);
                SqlDataAdapter ada = new SqlDataAdapter(com);  //这是SqlDataAdapter的一个重载方法，参数可以使SqlCommnd类型，也可以是两个参数  前一个是sql语句，后一个是SqlConnection对象
                try
                {
                    com.Connection.Open();
                count=ada.Fill(ds);
                    com.Connection.Close();
                    ada.Dispose();
                    com.Dispose();
                }
            catch (Exception e)
            {
                Trace.WriteLine("获取数据失败，失败原因：" + e.Message + "::执行sql：" + sql);
            }
            // Trace.WriteLine("执行sql：" + sql + "::执行结果：" + count);
            return ds;
            }
        /// <summary>
        /// 得到数据条目数
        /// </summary>
        /// <param name="sql">查询数据脚本</param>
        /// <returns></returns>
        public static int GetDataCount(string sql)
        {
            // MessageBox.Show(sql);
            int count = 0;
            DataSet ds = new DataSet();
            SqlCommand com = new SqlCommand(sql, DBconn.Conn);
            SqlDataAdapter ada = new SqlDataAdapter(com);  //这是SqlDataAdapter的一个重载方法，参数可以使SqlCommnd类型，也可以是两个参数  前一个是sql语句，后一个是SqlConnection对象
            try
            {
                com.Connection.Open();
                count = ada.Fill(ds);
                com.Connection.Close();
                ada.Dispose();
                com.Dispose();
            }
            catch(Exception e) {
                Trace.WriteLine("获取数据行数失败，失败原因：" + e.Message + "::执行sql：" + sql);
            }
          //  Trace.WriteLine("执行sql：" + sql + "::执行结果：" + count);
            return count;
        }
        public static DataSet GetDataSet(string sql,string tbname)
        {
            // MessageBox.Show(sql);
            int count = 0;
            DataSet ds = new DataSet();
            SqlCommand com = new SqlCommand(sql, DBconn.Conn);
            SqlDataAdapter ada = new SqlDataAdapter(com);  //这是SqlDataAdapter的一个重载方法，参数可以使SqlCommnd类型，也可以是两个参数  前一个是sql语句，后一个是SqlConnection对象
            try
            {
                com.Connection.Open();
                count=ada.Fill(ds,tbname);
                com.Connection.Close();
                ada.Dispose();
                com.Dispose();
            }
            catch (Exception e)
            {
                Trace.WriteLine("获取数据失败，失败原因：" + e.Message + "::执行sql：" + sql);
            }
          //  Trace.WriteLine("执行sql：" + sql + "::执行结果：" + count);
            return ds;
        }
        /// <summary>
        /// 分页显示数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="tbname"></param>
        /// <param name="CurrentPage"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public static DataSet GetDataSet(string sql, string tbname,int CurrentPage, int pagesize)
        {
            DataSet ds = new DataSet();
            int count = 0;
            SqlCommand cmd = new SqlCommand("DF_SelForPager", DBconn.Conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@sql", sql);  //给输入参数赋值
            cmd.Parameters.AddWithValue("@Order", "");  //给输入参数赋值
            cmd.Parameters.AddWithValue("@CurrentPage", CurrentPage);  //给输入参数赋值
            cmd.Parameters.AddWithValue("@PageSize", pagesize);  //给输入参数赋值
            SqlDataAdapter ada = new SqlDataAdapter(cmd);
            try
            {
                cmd.Connection.Open();
                count=ada.Fill(ds, tbname);
                cmd.Connection.Close();
                ada.Dispose();
                cmd.Dispose();
                //移除列
                RemoveColumn("r",ds.Tables[tbname]);
            }
            catch(Exception e)
            { Trace.TraceError("执行sql分错误，错误原因："+e.Message); }
        //    Trace.WriteLine("执行sql分页，SQL:"+sql+"::页码"+ CurrentPage + "::每页条数：" + pagesize + "::执行结果：" + count);
            return ds;
        }

        /// <summary>
        /// 移除列
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="table"></param>
        private static void RemoveColumn(string columnName, DataTable table)
        {
            DataColumnCollection columns = table.Columns;
            if (columns.Contains(columnName))
                if (columns.CanRemove(columns[columnName]))
                    columns.Remove(columnName);
        }

        #endregion
        #region 把DataSet中的数据显示在DataGridViewX中的方法
        /*     public static void ShowData(DataGridView view, DataSet ds, int index)
             {
                     try
                     {
                         view.DataSource = ds.Tables[index];
                     }
                     catch { }
             }
             */
        #endregion
        #region 执行简单非查询SQL语句的方法
        public static int DeleteData(string sql)
            {
                int id = -1;
                SqlConnection con =DBconn.Conn;
                SqlCommand com = new SqlCommand(sql, con);
                try
                {
                    com.Connection.Open();
                    id = com.ExecuteNonQuery();
                    com.Connection.Close();
                }
                catch { }
                return id;
            }
            #endregion
        /// <summary>
        /// 返回第一行第一列
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
            public static object ExecuteSca(string sql)
            {
            object retval = null;
            try
                {
                SqlConnection con = DBconn.Conn;
                SqlCommand com = new SqlCommand(sql, con);
                com.Connection.Open();
                retval = com.ExecuteScalar();
                com.Connection.Close();
            }       
                catch(Exception e)
            {
                Trace.WriteLine("执行取第一列第一行值sql失败，SQL:" + sql + "原因为：" + e.Message);
            }
            Trace.WriteLine("执行取第一列第一行值sql，SQL:" + sql + "结果为：" + retval);
            return retval;
            }
        public static string getmaxylsh(string sql) {
            SqlParameter parOutput = null;
            try
            {
                SqlCommand cmd = new SqlCommand("getMaxybh", DBconn.Conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@sktid", ConfigurationManager.AppSettings["sktid"].ToString());  //给输入参数赋值
                parOutput = cmd.Parameters.Add("@djbh", SqlDbType.VarChar, 14);  //定义输出参数
                parOutput.Direction = ParameterDirection.Output;  //参数类型为Output
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch
            { }
            return parOutput.Value.ToString();
        }
            /// <summary>
            ///  执行存储过程返回DataSet数据集的方法
            /// </summary>
            /// <param name="proc">存储过程名字</param>
            /// <param name="par">参数列表</param>
            /// <param name="vis">隐藏字段列表</param>
            /// <returns> </returns>

        public static DataSet ProcGetDataSet(string proc,IDataParameter[] par,string [] vis)
            {
                // MessageBox.Show(sql);
                DataSet ds = new DataSet();
                SqlCommand cmd =  BuildQueryCommand(DBconn.Conn, proc, par);
               // cmd.CommandType = CommandType.StoredProcedure;
                //添加参数
               // cmd.Parameters.AddWithValue("@sktid", ConfigurationManager.AppSettings["sktid"].ToString());
                SqlDataAdapter ada = new SqlDataAdapter(cmd);
                try
                {
                    cmd.Connection.Open();
                    ada.Fill(ds);
                    cmd.Connection.Close();
                    ada.Dispose();
                    cmd.Dispose();
                }
                catch { }
                return ds;
            }

            /// <summary>
            /// 构建 SqlCommand 对象(用来返回一个结果集，而不是一个整数值)
            /// </summary>
            /// <param name="connection">数据库连接</param>
            /// <param name="storedProcName">存储过程名</param>
            /// <param name="parameters">存储过程参数</param>
            /// <returns>SqlCommand</returns>
            private static SqlCommand BuildQueryCommand(SqlConnection connection, string storedProcName, 
                IDataParameter[] parameters)
            {
                SqlCommand command = new SqlCommand(storedProcName, connection);
                command.CommandType = CommandType.StoredProcedure;
                foreach (SqlParameter parameter in parameters)
                {
                    if (parameter != null)
                    {
                        // 检查未分配值的输出参数,将其分配以DBNull.Value.
                        if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                            (parameter.Value == null))
                        {
                            parameter.Value = DBNull.Value;
                        }
                        command.Parameters.Add(parameter);
                    }
                }

                return command;
            }


        //添加汇总
        /*       public static int skhz_ins(Skhz hz)
               {  //32298922
                   string sql = "insert into ybsk_hz(rq,ontime,sktid,djbh,sbjgbh,yltclb,grbh,xm,xb,jbbm,fyrq,kh,ysbm,jylb,jyyybm,"
              +"zylsh,jshid,fyid,brfdje,yyfdje,ybfdje,grzhzf,tjrylb,brjsrq,zje,zhye,bczcqzhye,bczcqzhjqje,status,username,dwmc,jbmch)"
             +" values(@rq,@ontime,@sktid,@djbh,@sbjgbh,@yltclb,@grbh,@xm,@xb,@jbbm,@fyrq,@kh,@ysbm,@jylb,@jyyybm,"
            +"@zylsh,@jshid,@fyid,@brfdje,@yyfdje,@ybfdje,@grzhzf,@tjrylb,@brjsrq,@zje,@zhye,@bczcqzhye,@bczcqzhjqje,@status,@username,@dwmc,@jbmch)";
                   SqlCommand com = new SqlCommand(sql,DBconn.Conn);
                   com.Parameters.Add("@rq", SqlDbType.VarChar, 10).Value = hz.rq;
                   com.Parameters.Add("@ontime", SqlDbType.VarChar, 10).Value = hz.ontime;
                   com.Parameters.Add("@sktid", SqlDbType.VarChar, 2).Value = hz.Sktid;
                   com.Parameters.Add("@djbh", SqlDbType.VarChar, 16).Value = hz.Djbh;
                   com.Parameters.Add("@sbjgbh", SqlDbType.VarChar, 20).Value = hz.Sbjgbh;
                    com.Parameters.Add("@yltclb",SqlDbType.VarChar, 10).Value =hz.Yltclb;
                   com.Parameters.Add("@grbh",SqlDbType.VarChar, 18).Value =hz.Grbh;
                   com.Parameters.Add("@xm",SqlDbType.VarChar, 10).Value =hz.Xm;
                   com.Parameters.Add("@xb",SqlDbType.VarChar, 10).Value =hz.Xb;
                   com.Parameters.Add("@jbbm", SqlDbType.VarChar, 20).Value = hz.Jbbm;
                   com.Parameters.Add("@fyrq", SqlDbType.VarChar, 20).Value = hz.Fyrq;
                   com.Parameters.Add("@kh", SqlDbType.VarChar, 30).Value = hz.Kh;
                   com.Parameters.Add("@ysbm", SqlDbType.VarChar, 10).Value = hz.Ysbm;
                   com.Parameters.Add("@jylb", SqlDbType.VarChar, 10).Value = hz.Jylb;
                   com.Parameters.Add("@jyyybm", SqlDbType.VarChar, 10).Value = hz.Jyyybm;
                   com.Parameters.Add("@zylsh", SqlDbType.VarChar, 20).Value = hz.Zylsh;
                   com.Parameters.Add("@fyid", SqlDbType.VarChar, 40).Value = hz.Fyid;
                   com.Parameters.Add("@jshid", SqlDbType.VarChar, 40).Value = hz.Jshid;
                   SqlParameter brfdje = com.Parameters.Add("@brfdje", SqlDbType.Decimal); 
                     brfdje.Precision = 12; brfdje.Scale = 2;
                     brfdje.Value = hz.Brfdje;
                   SqlParameter yyfdje = com.Parameters.Add("@yyfdje", SqlDbType.Decimal); 
                     yyfdje.Precision = 12; yyfdje.Scale = 2;
                     yyfdje.Value = hz.Yyfdje;
                   SqlParameter ybfdje = com.Parameters.Add("@ybfdje", SqlDbType.Decimal);
                     ybfdje.Precision = 12; ybfdje.Scale = 2;
                     ybfdje.Value = hz.Ybfdje;
                     SqlParameter grzhzf = com.Parameters.Add("@grzhzf", SqlDbType.Decimal);
                     grzhzf.Precision = 12; grzhzf.Scale = 2;
                     grzhzf.Value = hz.Grzhzf;
                     SqlParameter zje = com.Parameters.Add("@zje", SqlDbType.Decimal);
                     zje.Precision = 12; zje.Scale = 2;
                     zje.Value = hz.Zje;
                     SqlParameter zhye = com.Parameters.Add("@zhye", SqlDbType.Decimal);
                     zhye.Precision = 16; zhye.Scale = 4;
                     zhye.Value = hz.Zhye;
                     SqlParameter bczcqzhye = com.Parameters.Add("@bczcqzhye", SqlDbType.Decimal);
                     bczcqzhye.Precision = 16; bczcqzhye.Scale = 4;
                     bczcqzhye.Value = hz.Bczcqzhye;
                     SqlParameter bczcqzhjqje = com.Parameters.Add("@bczcqzhjqje", SqlDbType.Decimal);
                     bczcqzhjqje.Precision = 16; bczcqzhjqje.Scale = 4;
                     bczcqzhjqje.Value = hz.Bczcqzhjqje;
                    com.Parameters.Add("@username",SqlDbType.VarChar, 10).Value =hz.Username;
                    com.Parameters.Add("@tjrylb", SqlDbType.VarChar, 10).Value = hz.Tjrylb;
                    com.Parameters.Add("@brjsrq", SqlDbType.VarChar, 20).Value = hz.Brjsrq;
                    com.Parameters.Add("@status", SqlDbType.VarChar, 2).Value = hz.Status;
                    com.Parameters.Add("@dwmc", SqlDbType.VarChar, 255).Value = hz.Dwmc;
                    com.Parameters.Add("@jbmch", SqlDbType.VarChar, 255).Value = hz.Jbmch;
                   int id = -1;
                   try
                   {
                       com.Connection.Open();
                       id = com.ExecuteNonQuery();
                       com.Connection.Close();
                   }
                   catch
                   { }
                   return id;
               }

           //添加明细
               public static int skmx_ins(Skmx mx) {
                   string sqlmx="insert into ybsk_mx(djbh,dj_sn,spid,yyxmbm,yyxmmc,dj,sl,bzsl,zje,gg,sxzfbl,fyfssj,zxksbm,kdksbm,sm,sfryxm,status)"
       +" values (@djbh,@dj_sn,@spid,@yyxmbm,@yyxmmc,@dj,@sl,@bzsl,@zje,@gg,@sxzfbl,@fyfssj,@zxksbm,@kdksbm,@sm,@sfryxm,@status)";

                   SqlCommand com = new SqlCommand(sqlmx, DBconn.Conn);
                   com.Parameters.Add("@djbh", SqlDbType.VarChar, 14).Value = mx.Djbh;
                   com.Parameters.Add("@dj_sn", SqlDbType.Int).Value =mx.Dj_sn;
                   com.Parameters.Add("@spid", SqlDbType.VarChar, 11).Value = mx.Spid;
                   com.Parameters.Add("@yyxmbm", SqlDbType.VarChar,60).Value = mx.Yyxmbm;
                   com.Parameters.Add("@yyxmmc", SqlDbType.VarChar, 200).Value = mx.Yyxmmc.Trim();
                   com.Parameters.Add("@gg", SqlDbType.VarChar, 50).Value = mx.Gg.Trim();
                   com.Parameters.Add("@fyfssj", SqlDbType.VarChar, 20).Value = mx.Fyfssj;
                   com.Parameters.Add("@zxksbm", SqlDbType.VarChar, 20).Value = mx.Zxksbm;
                   com.Parameters.Add("@kdksbm", SqlDbType.VarChar, 20).Value = mx.Kdksbm;
                   com.Parameters.Add("@sm", SqlDbType.VarChar, 100).Value = mx.Sm;
                   com.Parameters.Add("@sfryxm", SqlDbType.VarChar, 20).Value = mx.Sfryxm;
                   com.Parameters.Add("@status", SqlDbType.VarChar, 2).Value = mx.Status;
                   SqlParameter dj = com.Parameters.Add("@dj", SqlDbType.Decimal);
                   dj.Precision = 16; dj.Scale = 6;
                   dj.Value = mx.Dj;
                   SqlParameter sl = com.Parameters.Add("@sl", SqlDbType.Decimal);
                   sl.Precision = 12; sl.Scale = 4;
                   sl.Value = mx.Sl;
                   SqlParameter bzsl = com.Parameters.Add("@bzsl", SqlDbType.Decimal);
                   bzsl.Precision = 12; bzsl.Scale = 4;
                   bzsl.Value = mx.Bzsl;
                   SqlParameter zje = com.Parameters.Add("@zje", SqlDbType.Decimal);
                   zje.Precision = 16; zje.Scale = 4;
                   zje.Value = mx.Zje;
                   SqlParameter sxzfbl = com.Parameters.Add("@sxzfbl", SqlDbType.Decimal);
                   sxzfbl.Precision = 16; sxzfbl.Scale = 4;
                   sxzfbl.Value = mx.Sxzfbl;
                   int id = -1;
                   try
                   {
                       com.Connection.Open();
                       id = com.ExecuteNonQuery();
                       com.Connection.Close();
                       Yblog.log("明细数据存盘", "存盘成功djbh=" + mx.Djbh);
                   }
                   catch(SqlException e)
                   {
                      MessageBox.Show(e.ToString());
                       Yblog.log("明细数据存盘", "存盘不成功djbh=" + mx.Djbh);
                       Yblog.log("明细数据存盘", "失败原因" + e.ToString());
                   }
                   return id;
               }
               */
               /// <summary>
               /// 设置储值卡操作
               /// </summary>
               /// <param name="mx"></param>
               /// <returns></returns>
        public static int SetICCard(MyDic<string,string> dic)
        {
            string upsql = "update ret_cuxiaoka set leijck=@leijck,ye=@ye,ljxfe=@ljxfe,ljzpjz=@ljzpjz, "
      + " ljtuih = @ljtuih,ljgongxian = @ljgongxian,ljjifen = @ljjifen,touzxe = @touzxe, "
      + " isdj = @isdj,beactive = @beactive,yuexiugbaohu = @yuexiugbaohu "
      + " where cardid = @cardid ";
            SqlCommand com = new SqlCommand(upsql, DBconn.Conn);
            com.Parameters.Add("@isdj", SqlDbType.Char,2).Value =dic["isdj"];
            com.Parameters.Add("@beactive", SqlDbType.Char,2).Value =dic["beactive"];
            com.Parameters.Add("@yuexiugbaohu", SqlDbType.VarChar,400).Value = dic["yuexiugbaohu"];
            com.Parameters.Add("@cardid", SqlDbType.VarChar, 15).Value = dic["cardid"];
            com.Parameters.Add("@leijck", SqlDbType.Decimal).Value = dic["leijck"];
            com.Parameters.Add("@ye", SqlDbType.Decimal).Value = dic["ye"];
            com.Parameters.Add("@ljxfe", SqlDbType.Decimal).Value = dic["ljxfe"];
            com.Parameters.Add("@ljzke", SqlDbType.Decimal).Value = dic["ljzke"];
            com.Parameters.Add("@ljzpjz", SqlDbType.Decimal).Value = dic["ljzpjz"];
            com.Parameters.Add("@ljtuih", SqlDbType.Decimal).Value = dic["ljtuih"];
            com.Parameters.Add("@ljgongxian", SqlDbType.Decimal).Value = dic["ljgongxian"];
            com.Parameters.Add("@ljjifen", SqlDbType.Decimal).Value = dic["ljjifen"];
            com.Parameters.Add("@touzxe", SqlDbType.Decimal).Value = dic["touzxe"];
            int id = -1;
            try
            {      
                com.Connection.Open();
                id = com.ExecuteNonQuery();
                com.Connection.Close();
                Trace.WriteLine("储值卡:"+dic["cardid"] +"刷卡成功。");
            }
            catch (SqlException e)
            {
                Trace.WriteLine("储值卡:" + dic["cardid"] + "刷卡失败。"+e.Message);
            }
            return id;
        }
        /// <summary>
        /// 用一个事务来批量执行sql语句
        /// </summary>
        /// <param name="sqlArr">存放sql语句(s)的字符串数组</param>
        public static int ExecuteSqlTransaction(ArrayList sqlArr)
        {
            int count = 0;
            using (SqlConnection connection =DBconn.Conn)
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                // 开始一个事务
                transaction = connection.BeginTransaction("InsertTransaction");
                //必须给命令对象（command）的连接和事务相应的值
                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    //在一个事务里执行多条sql语句
                    for (int i = 0; i < sqlArr.Count; i++)
                    {
                        command.CommandText = sqlArr[i].ToString();
                        count+=command.ExecuteNonQuery();
                    }
                    // 提交事务
                    transaction.Commit();
                    Trace.WriteLine("上传成功，条目数："+count);
                }
                catch (Exception ex)
                {
                    //执行sql过程中出错，即事务回滚
                    transaction.Rollback();
                    Trace.WriteLine("上传失败，错误原因：" + ex.Message);
                }
            }
            return count;
        }
        public static int Execproc(string procname,string lshh) {
            int ret = 0;
            SqlCommand command = new SqlCommand(procname,DBconn.Conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@lshh",lshh);
            command.Parameters.Add(new SqlParameter("@return", SqlDbType.Int));
            command.Parameters["@return"].Direction = ParameterDirection.ReturnValue;
            try
            {
                command.Connection.Open();
                ret = command.ExecuteNonQuery();
                ret = (int)command.Parameters["@return"].Value;
                if (ret == 0) {
                    Trace.WriteLine("实时日清成功，lshh:" + lshh);
                }
                if (ret != 0)
                {
                    Trace.WriteLine("实时日清失败，失败lshh:" + lshh+ ",失败错误号："+ret);
                }
            }
            catch(Exception e) {
                Trace.WriteLine("实时日清失败，失败原因："+e.Message);
            }
            finally {
                DBconn.Conn.Close();
            }
            return ret;
        }
}
}
