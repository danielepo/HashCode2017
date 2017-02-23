using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Program;
using static Program.Solver;
using System.Collections.Generic;

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


        [TestMethod]
        public void TestMethod2()
        {

            LoadFile load = new LoadFile();


            Data d = load.LoadFrom("D:\\datas.txt");


            Solver solver = new Solver(null, null);


            List<Server> ser = solver.Map(d);

        }
    }
}
