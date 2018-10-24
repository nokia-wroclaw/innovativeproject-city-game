using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets
{
    class PathData
    {
        //!< it could be Vector2 p1, and Vector2 p2
        public float P1Lat { set; get; }
        public float P1Long { set; get; }
        public float P2Lat { set; get; }
        public float P2Long { set; get; }


        PathData()
        {
            //set default value
            P1Lat  = 0;
            P1Long = 0;
            P2Lat  = 0;
            P2Long = 0;
        }

        PathData(float P1Lat, float P1Long, float P2Lat, float P2Long)
        {
            //set custom value
            this.P1Lat  = P1Lat;
            this.P1Long = P1Long;
            this.P2Lat  = P2Lat;
            this.P2Long = P2Long;
        }

    }
}
