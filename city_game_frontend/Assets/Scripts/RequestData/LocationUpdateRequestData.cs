using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LocationUpdateRequestData
{
    public int type = Const.MESSAGE_TYPE_LOCATION_EVENT;
    public float lon;
    public float lat;

    public LocationUpdateRequestData(float longitude, float latitude)
    {
        this.lon = longitude;
        this.lat = latitude;
    }
}
