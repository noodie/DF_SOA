using Microsoft.VisualStudio.TestTools.UnitTesting;
using DF_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DF_DAL.Tests
{
    [TestClass()]
    public class PubmethodTests
    {
        [TestMethod()]
        public void GetDataCountTest()
        {
            int a = 0;
            a=DF_DAL.Pubmethod.GetDataCount("select * from poszl");
            Assert.AreEqual(a,0);
        }
    }
}