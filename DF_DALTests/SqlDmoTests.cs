using DF_DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DF_DAL.Tests
{
    [TestClass()]
    public class SqlDmoTests
    {
        [TestMethod()]
        public void GetSqlColTest()
        {
            SqlDmo sd = new SqlDmo();
            Dictionary<string, string> di = sd.GetSqlCol("pos_mast", "yhje");
            foreach (KeyValuePair<string, string> pair in di)
            {
                Console.WriteLine(pair.Key+pair.Value);
            }
            Assert.IsNotNull(di);
        }

        [TestMethod()]
        public void CheckJsonTest()
        {
            string jsons = File.ReadAllText("D:\\csjson.txt", Encoding.Default);
            JsonModel JM = JsonTools.JsonToDicList(jsons);
            List<UpModel> UM = DfConfigTools.GetUpObj();
            SqlDmo sd = new SqlDmo();
            Assert.IsTrue(sd.CheckJson(JM, UM));
        }
        [TestMethod()]
        public void SetDataTest1()
        {
            string jsons = File.ReadAllText("D:\\csjson.txt", Encoding.Default);
            JsonModel JM = JsonTools.JsonToDicList(jsons);
            SqlDmo sd = new SqlDmo();
            Dictionary<string, string> dsql = sd.GetSqlCol("pos_mast", "lshh");
            JM.Tb_dic = JM.JsonDic("pos_mast")[0];
            Console.WriteLine(JM.Tb_dic["lshh"]);
            JM.Tb_dic[dsql["D_name"]] = JM.Tb_dic[dsql["D_name"]].Trim().Length >
                int.Parse(dsql["D_len"]) ?
                JM.Tb_dic[dsql["D_name"]].Trim().Substring(0, int.Parse(dsql["D_len"])) :
                JM.Tb_dic[dsql["D_name"]].Trim();
            JM.SetData(JM, dsql);
            Console.WriteLine(JM.Tb_dic[dsql["D_name"]]);
            Assert.AreEqual(JM.Tb_dic[dsql["D_name"]], "P01A03000000532");
        }

        [TestMethod()]
        public void InsSqlTest()
        {
            string jsons = File.ReadAllText("D:\\csjson.txt", Encoding.Default);
            JsonModel JM = JsonTools.JsonToDicList(jsons);
            SqlDmo sd = new SqlDmo();
            List<UpModel> UM = DfConfigTools.GetUpObj();
            string i = "";
           if (sd.CheckJson(JM, UM))
             i = sd.InsSql(JM, UM);
           // Console.WriteLine(i);
            Assert.IsNotNull(i);
        }
    }
}