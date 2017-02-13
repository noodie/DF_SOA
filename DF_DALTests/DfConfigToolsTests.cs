using DF_DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DF_SOA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DF_DAL.Tests
{
    [TestClass()]
    public class DfConfigToolsTests
    {
        [TestMethod()]
        public void GetColTest()
        {
            List<UpModel> DownL = DfConfigTools.GetUpObj();
            UpModel up = new UpModel();
            foreach (UpModel d in DownL)
            {
                if (d.T_name1 == "pos_mast")
                {
                    up = d;
                }
            }
            List<string> col = DBCheck.GetCol(up.T_sql1);
            foreach(string s in col)
            Console.WriteLine(s);
            Assert.IsNotNull(col);
        }

        [TestMethod()]
        public void GetUpObjTest()
        {
            Assert.AreEqual(3,DfConfigTools.GetUpObj().Count);
        }
    }
}