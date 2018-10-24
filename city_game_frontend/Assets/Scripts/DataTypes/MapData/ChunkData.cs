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
        public float latitude;//!< position of the beginnig of chunk
        public float longitude;

        public int id;

        public List<PathData> road_nodes; //!< paths list

    }
}
