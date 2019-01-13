using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Dynamic structs are sent in list by default, so we need an object to deserialize the list into
 */
[System.Serializable]
public class DynamicStructsResponseData
{
    public List<DynamicStructData> structures = new List<DynamicStructData>();
}

[System.Serializable]
public class DynamicStructData
{
    public int id;
    public float lat;
    public float lon;
    public float rotation;

    public bool taken_over;
    public string owner;

    public int tier;
    public int resource_type;
    public float resources_left;
}

public class DynamicStruct : MonoBehaviour {

    public DynamicStructData data;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
