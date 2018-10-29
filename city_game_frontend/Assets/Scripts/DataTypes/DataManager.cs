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
        private static DataManager c_instance; //!< instance of the Manager class

        public MapData map { private set; get; }

        public float latitude { get { return GPSManager.Instance.latitude; } }
        public float longitude { get { return GPSManager.Instance.longitude; } }

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
