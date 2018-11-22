using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DynamicStructsRequestData
{
    public int type = Const.MESSAGE_TYPE_DYNAMIC_CHUNK_DATA_REQUEST;
    public float lon;
    public float lat;

    public DynamicStructsRequestData(float longitude, float latitude)
    {
        this.lon = longitude;
        this.lat = latitude;
    }
}
