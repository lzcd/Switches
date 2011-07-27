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
        public void SwitchPathWorkBestest()
        {
            var message ="";
            var db = "";
            var quack = false;

            var switchPath = new SwitchPath()
                    .On("d").Do(name => { db = name; })
                    .On("q").Do(() => { quack = true; })
                    .On("L").Ignore()
                    .On("s").Ignore()
                    .OnRemainderDo(name => { message = "Why you " + name + "?"; });

            var args = new string[] { 
                @"-d", @"My Database",
                @"-q",
                @"-a",
                @"-L", @"C:\log.txt",
                @"-s", "." };

            switchPath.Parse(args);

            Assert.AreEqual(@"My Database", db);
            Assert.IsTrue(quack);
            Assert.AreEqual(@"Why you -L?", message);
        }

        [TestMethod]
        public void SwitchSetsDoesWorkGoodest()
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
