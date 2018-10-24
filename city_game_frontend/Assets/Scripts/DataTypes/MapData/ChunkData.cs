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

        public int id;

        private List<PathData> paths; //!< paths list

    }
}
