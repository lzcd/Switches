using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Switches;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var args = new string[] { 
                @"-d", @"My Database",
                @"-q",
                @"-a",
                @"-L", @"C:\log.txt",
                @"-s", "." };


            dynamic switchSet = new SwitchSet(args);
            Assert.AreEqual(true, switchSet.dSettingExists);
            Assert.AreEqual(@"My Database", switchSet.dSetting);
            Assert.AreEqual(true, switchSet.qSettingExists);
            Assert.AreEqual(false, switchSet.xSettingExists);
        }
    }
}
