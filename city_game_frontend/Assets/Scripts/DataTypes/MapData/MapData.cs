﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets
{
    /**
     * Chanks menager class. it stores chanks and has functions to menage them
     */
    class MapData
    {
        public int CountChunks { get
            {
                return chunks.Count;
            }
        }

        private List<ChunkData> chunks = new List<ChunkData>();

        /**
         * Function add Chunk to the chunks list
         * @param[in] c - initialized chunk
         * 
         * @pre c has to be initialized. 
         */
        public void addChunk(ChunkData item)
        {
            chunks.Add(item);
        }

        /**
         * Function return reference to a selected ChunkData object
         * 
         * @param[in] index - index of chunk - debug function
         */
        public ChunkData getChunk(int index)
        {
            if(index >= chunks.Count)
                throw new IndexOutOfRangeException();

            return chunks[index];
        }


    }
}
