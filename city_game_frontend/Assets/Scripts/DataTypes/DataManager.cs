using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets
{
    /**
     * Singleton. Class used to communicate user interface module 
     * and server communication module. 
     * 
     * 
     * 
     */
    class DataManager
    {
        public int i { set; get; } 
        private static DataManager c_instance; //!< instance of the Menager class

        public MapData map { private set; get; }

        private DataManager()
        {
            i = 0;
            map = new MapData();
        }

        public static DataManager instance()
        {
            if (c_instance == null)
                c_instance = new DataManager();

            return c_instance;
        }

    }
}
