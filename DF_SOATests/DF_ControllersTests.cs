using Microsoft.VisualStudio.TestTools.UnitTesting;
using DF_SOA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DF_SOA.Tests
{
    [TestClass()]
    public class DF_ControllersTests
    {
        [TestMethod()]
        public void GetICCardTest()
        {
            string s = DF_Controllers.GetICCard("0001002");
            Console.WriteLine(s);
            Assert.IsNotNull(s);
        }

        [TestMethod()]
        public void getDownJsonTest()
        {
            string s = DF_Controllers.getDownJson("spkfk","ZDA","2016-10-08 12:00:00");
            Console.WriteLine(s);
            Assert.IsNotNull(s);
        }
    }
}