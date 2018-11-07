using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapRequestData
{
    public int type = 2;
    public float lon;
    public float lat;

    public MapRequestData(float longitude, float latitude)
    {
        this.lon = longitude;
        this.lat = latitude;
    }
}
