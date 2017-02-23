using System;

namespace Program.IOManagement
{
    public class Loader : ILoader
    {
        private readonly string path;

        public Loader(string path)
        {
            this.path = path;
        }

        public Data Load()
        {
            LoadFile loadFile = new LoadFile();


            return loadFile.LoadFrom(path);


        }
    }
}
