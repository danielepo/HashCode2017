using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Program;

namespace TestMiei
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {


            LoadFile load = new LoadFile();

    
        Data data = load.LoadFrom(@"c:\Utenti\piv\Desktop\datas.txt");


        }
    }
}
