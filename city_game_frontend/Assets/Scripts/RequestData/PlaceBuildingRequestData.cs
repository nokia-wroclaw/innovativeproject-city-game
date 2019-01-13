using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlaceBuildingRequestData
{
    public int type = Const.MESSAGE_TYPE_STRUCT_PLACEMENT_REQUEST;
    public float lon;
    public float lat;
    public float rotation;
    public int tier;

    public PlaceBuildingRequestData(float latitude, float longitude, float rotation, int tier)
    {
        this.lon = longitude;
        this.lat = latitude;
        this.rotation = rotation;
        this.tier = tier;
    }
}
