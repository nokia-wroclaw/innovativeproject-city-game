using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets
{
    /**
     * Class contains chanks data
     */
    class ChunkData
    {
        public float latitude { private set; get; } //!< position of the beginnig of chunk
        public float longitude { private set; get; }

        private List<PathData> paths; //!< paths list

        ChunkData(float latitude, float longitude)
        {
            this.latitude = latitude;
            this.longitude = longitude;
        }
        

        public void addPath(PathData item)
        {
            paths.Add(item);
        }
    }
}
