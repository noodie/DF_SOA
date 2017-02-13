using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DF_DAL
{
   public class UpModel
    {
        /// <summary>
        /// 上传表配置
        /// </summary>
        private string T_name;//表名
        private string T_sql;//传输sql
        private string T_action;//传输执行动作
        private int T_count; //表值条目数
                             //       private string Ins_sql;
        private List<string> col_list;  //上传字段列表
        public string T_name1
        {
            get
            {
                return T_name.ToLower();
            }

            set
            {
                T_name = value.ToLower();
            }
        }

        public string T_sql1
        {
            get
            {
                return T_sql;
            }

            set
            {
                T_sql = value;
            }
        }

        public string T_action1
        {
            get
            {
                return T_action;
            }

            set
            {
                T_action = value;
            }
        }

        public int T_count1
        {
            get
            {
                return T_count;
            }

            set
            {
                T_count = value;
            }
        }

        public string Ins_sql1
        {
            get
            {
                string col = T_sql.ToUpper().Substring(0, T_sql.ToUpper().IndexOf("FROM"));
                 col = col.Replace("SELECT", "INSERT INTO "+T_name1+"(") +") VALUES(";
                return col;
            }
        }

        public List<string> Col_list
        {
            get
            {
                if (this.col_list == null)
                    return new List<string>();
                else
                    return col_list;
            }

            set
            {
                col_list = value;
            }
        }
    }
}
