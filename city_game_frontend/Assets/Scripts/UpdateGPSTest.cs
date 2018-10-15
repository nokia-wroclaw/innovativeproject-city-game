using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateGPSTest : MonoBehaviour {

    public Text cords;

    private void Update()
    {
        cords.text = "LAT: " + GPSManager.Instance.latitude.ToString() + "  LONG: " + GPSManager.Instance.longitude.ToString();
    }
}
