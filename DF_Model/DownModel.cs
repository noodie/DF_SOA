using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DF_DAL
{
    public class DownModel
    {
        /// <summary>
        /// 下载表配置
        /// </summary>
        private string T_name;//表名
        private string T_sql;//传输sql
        private string T_action;//传输执行动作
        private int T_count; //表值条目数
        private string T_sort;//排序字段名 预留字段 暂时不用

        public string T_name1
        {
            get
            {
                if (this.T_name == null)
                {
                    return "";
                }
                else
                return T_name.ToLower();
            }

            set
            {
                T_name = value;
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

        public string T_sort1
        {
            get
            {
                return T_sort;
            }

            set
            {
                T_sort = value;
            }
        }
    }
}
