using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DynamicChunkDataRequestData
{
    public int type = Const.MESSAGE_TYPE_CHUNK_OWNER_REQUEST;
    public float lon;
    public float lat;

    public DynamicChunkDataRequestData(float longitude, float latitude)
    {
        this.lon = longitude;
        this.lat = latitude;
    }
}
