using Microsoft.VisualStudio.TestTools.UnitTesting;
using DF_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using DF_DAL;

namespace DF_DAL.Tests
{
    [TestClass()]
    public class JsonModelTests
    {
        [TestMethod()]
        public void SetDataTest()
        {
            string jsons = File.ReadAllText("D:\\0au.txt", Encoding.Default);
            JsonModel JM = JsonTools.JsonToDicList(jsons);
            SqlDmo sd = new SqlDmo();
            Dictionary<string, string> dsql = sd.GetSqlCol("pos_mast", "lshh");
            JM.Tb_dic = JM.JsonDic("pos_mast")[0];
            Console.WriteLine(JM.Tb_dic["lshh"]);
            JM.SetData(JM,dsql);
            Assert.IsNotNull(dsql);
        }
    }
}