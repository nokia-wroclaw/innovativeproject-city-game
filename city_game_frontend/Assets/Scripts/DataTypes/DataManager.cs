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
        private int i;
        private static DataManager c_instance; //!< instance of the Menager class

        private DataManager()
        {
            i = 0;
        }

        public static DataManager instance()
        {
            if (c_instance == null)
                c_instance = new DataManager();

            return c_instance;
        }

        public void setI(int i)
        {
            this.i = i;
        }

        public int getI()
        {
            return i;
        }

    }
}
