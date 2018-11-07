using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets
{
    /**
     * Class contains chanks data
     */
    [System.Serializable]
    class ChunkData
    {
        public float latitude_lower_bound;//!< position of the beginnig of chunk
        public float longitude_lower_bound; // sorry for using the Python styled variable names, we need to discuss the naming rules
        // Maybe we should use the pythonic names for all the variables that are deserialized from server requests..?
        

       public List<Road> roads; //!< paths list

    }
}
