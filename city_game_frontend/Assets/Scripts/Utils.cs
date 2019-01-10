using UnityEngine;

public class Utils {

    /*
    * Because Unity's rotation system was way too sophisticated for our small needs
    */
    public static void rotationThatWorks(GameObject objectToRotate, Vector3 rotation)
    {
        objectToRotate.transform.eulerAngles = objectToRotate.transform.eulerAngles + rotation;
    }
}
