using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forwardMovement : MonoBehaviour {

    public float speed;

	// Use this for initialization
	void Start () {
}
	
	// Update is called once per frame
	void Update () {

        transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z + speed * Time.deltaTime
            );

    }

    [ContextMenu("omg")]
    void loadTrack()
    {

        for (float i = 0.00F; i < 0.3F; i += 0.001F)
            MapManager.Instance.sendChunkRequest(17.1F + i, 51.1F);

    }
}
