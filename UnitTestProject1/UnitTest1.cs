using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Program;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            LoadFile load = new LoadFile();


            Data d = load.LoadFrom("D:\\datas.txt");
        }
    }
}
