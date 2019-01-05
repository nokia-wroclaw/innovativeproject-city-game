using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/***
 * Script creates bob shadow using colider size
 */
public class bobShadowComponent : MonoBehaviour {
    public GameObject shadow;

    void Start () {
        BoxCollider collider = GetComponent<BoxCollider>();
        
        Projector projector = shadow.GetComponent<Projector>();
        shadow.transform.localPosition = new Vector3(0, 0, collider.size.z);
        shadow.transform.rotation = Quaternion.Euler(90, 0, 0);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
