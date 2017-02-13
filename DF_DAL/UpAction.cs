using DF_DAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DF_DAL
{
    public  class UpAction
    {
        /// <summary>
        /// 执行数据插入操作
        /// </summary>
        /// <param name="jsons">插入数据</param>
        /// <param name="LUM">插入表实例集合</param>
        /// <returns></returns>
        public int Insert(string jsons, List<UpModel> LUM) {
            int count = 0;
            JsonModel JM=JsonTools.JsonToDicList(jsons);
            JM.Lshh_list = new List<string>();
            SqlDmo sd = new SqlDmo();
            if (sd.CheckJson(JM,LUM)) {
                ArrayList s = sd.InsSqlArr(JM,LUM);
               count=Pubmethod.ExecuteSqlTransaction(s);
            }

            if (count > 0 && JM.Lshh_list.Count>0) {
                foreach (string lshh in JM.Lshh_list) {
                    if (Pubmethod.Execproc("df_lsjz", lshh) > 0)
                    {
                        count = 0; //如果日清不成功 则判定上传条目数为0 
                    }
                }
            }
            return count;
        }

        /// <summary>
        /// 测试插入使用
        /// </summary>
        /// <param name="jsons"></param>
        /// <returns></returns>
        public int Insert(string jsons)
        {
            int count = 0;
            JsonModel JM = JsonTools.JsonToDicList(jsons);
            JM.Lshh_list = new List<string>();
            List<UpModel> UM = DfConfigTools.GetUpObj();
            SqlDmo sd = new SqlDmo();
            if (sd.CheckJson(JM, UM))
            {
                ArrayList s = sd.InsSqlArr(JM, UM);
                count = Pubmethod.ExecuteSqlTransaction(s);
            }
            //    Console.WriteLine("上传条目数："+count+"....日清流水："+JM.Lshh_list.Count);
            if (count > 0 && JM.Lshh_list.Count > 0)
            {
                foreach (string lshh in JM.Lshh_list)
                {
                    if (Pubmethod.Execproc("df_lsjz", lshh) > 0)
                    {
                        count = 0; //如果日清不成功 则判定上传条目数为0 
                    }
                }
            } 
            return count;
        }
        public JsonModel Test1() {
            string jsons = File.ReadAllText("D:\\0au.txt", Encoding.Default); ;
            return JsonTools.JsonToDicList(jsons);
        }
    
    }
}
