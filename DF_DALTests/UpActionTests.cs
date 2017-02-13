using Microsoft.VisualStudio.TestTools.UnitTesting;
using DF_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.IO;

namespace DF_DAL.Tests
{
    [TestClass()]
    public class UpActionTests
    {
        [TestMethod()]
        public void InsertTest()
        {
            UpAction UA = new UpAction();
            string jsons = File.ReadAllText("D:\\csjson.txt", Encoding.Default);
            int i=UA.Insert(jsons);
            Assert.AreEqual(i,1);
        }
        //[TestMethod()]
        //public void TestJsonTest()
        //{
        //    UpAction u = new UpAction();
        //    DataTable dt = u.TestJson();
        //    Console.WriteLine("---json数据");
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        for (int j = 0; j < dt.Columns.Count; j++)
        //        {
        //            Console.WriteLine("列{0}行{1}", dt.Columns[j].ColumnName, dt.Rows[i][j]);
        //        }
        //    }
        //    Assert.IsNotNull(dt);
        //}

        //[TestMethod()]
        //public void Test1Test()
        //{
        //    UpAction u = new UpAction();
        //   JsonModel d = u.Test1();
        //    Console.WriteLine("---json数据");
        //    if (d.Jsons.ContainsKey("pos_mast"))
        //    {
        //        foreach (var a in d.Jsons["pos_mast"])
        //        {
        //            foreach (KeyValuePair<string, string> pair in a)
        //            {
        //                Console.WriteLine("Key:{0}, Value:{1}", pair.Key, pair.Value);
        //            }
        //        }
        //    }
        //    Assert.IsNotNull(d);
        //}
    }
}