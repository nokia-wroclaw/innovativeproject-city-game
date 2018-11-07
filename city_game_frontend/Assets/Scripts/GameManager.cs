using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    //this is controlled by GPSManager
    bool positionChanged = true;

    public ServerSocket server;


    float time_to_wait = 1;

	// Use this for initialization
	void Start () {
 }
	
	// Update is called once per frame
	void Update () {

        time_to_wait -= Time.deltaTime;

        if(time_to_wait < 0) { 
        server.updateChunks(17.1011F, 51.106F);
            time_to_wait = float.PositiveInfinity;
        }
        else
        {
            if(time_to_wait < 9999)
                Debug.Log(time_to_wait);
        }

        /*
                if(positionChanged)
                {
                    server.updateChunks(17.1011F, 51.106F);
                    //TODO reset here positionChanged
                }*/
    }
}
