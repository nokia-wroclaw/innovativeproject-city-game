using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    /**
     * Timer sync class. Allow to run some code in time interval.
     * 
     * @Example 
     * 
     * TimerSync t;
     * while(true) { 
     * ...
     *      if(t.isTimeEx(1000[ms]) {
     *          ...
     *          t.update() - to start a new countdown
     *      }
     * }
     * 
     * if statement will run every 1000ms 
     */
    class TimerSync
    {
        private double d = new double(); //!< last update time

        public TimerSync()
        {
            update();    
        }

        public void update()
        {
            //get current time in millisecs
            d = DateTime.Now.TimeOfDay.TotalMilliseconds;
        }

        public bool isTimeEx(int timeInMilliseconds)
        {
            //get current time in millisecs
            double now = DateTime.Now.TimeOfDay.TotalMilliseconds;

            return ((now - d) > timeInMilliseconds); //calc time diff
        }
    }

}
