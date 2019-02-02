using UnityEngine;

public class Utils {

    /*
    * Because Unity's rotation system was way too sophisticated for our small needs
    */
    public static void rotationThatWorks(GameObject objectToRotate, Vector3 rotation)
    {
        objectToRotate.transform.eulerAngles = objectToRotate.transform.eulerAngles + rotation;
    }

    public static float LatitudeToGameCoordinate(float lat)
    {
        return -(lat - MapManager.LATITUDE_OFFSET) * MapManager.MAP_SCALE_FACTOR;
    }

    public static float LongitudeToGameCoordinate(float lon)
    {
        return (lon - MapManager.LONGITUDE_OFFSET) * MapManager.MAP_SCALE_FACTOR;
    }

    public static float GameCoordinateXToLatitude(float x)
    {
        return MapManager.LATITUDE_OFFSET - x / MapManager.MAP_SCALE_FACTOR;
    }

    public static float GameCoordinateZToLongitude(float z)
    {
        return z / MapManager.MAP_SCALE_FACTOR + MapManager.LONGITUDE_OFFSET;
    }
}
