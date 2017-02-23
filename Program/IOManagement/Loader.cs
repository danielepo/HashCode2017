using System;

namespace Program.IOManagement
{
    public class Loader : ILoader
    {
        public Data Load()
        {
            LoadFile loadFile = new LoadFile();


            return loadFile.LoadFrom("C:\\datas.txt");


        }
    }
}
