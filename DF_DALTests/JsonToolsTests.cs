using Microsoft.VisualStudio.TestTools.UnitTesting;
using DF_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DF_DAL.Tests
{
    [TestClass()]
    public class JsonToolsTests
    {
        [TestMethod()]
        public void JsonToDicListTest()
        {
            //0au
            string jsons = File.ReadAllText("D:\\0au.txt", Encoding.Default);
            JsonModel JM = JsonTools.JsonToDicList(jsons);
            Console.WriteLine(JM.Jsons.Count);
            Assert.IsNotNull(JM);
        }

        [TestMethod()]
        public void JsonToObjectTest()
        {
            string jsons = File.ReadAllText("D:\\csjson.txt", Encoding.Default);
            MyDic<string, string> cardic = new MyDic<string, string>();
            cardic =JsonTools.JsonToObject(jsons);
            Console.WriteLine(cardic["lshh"]);
            Assert.AreEqual(cardic["lshh"], "P01A03000000532");
        }
    }
}