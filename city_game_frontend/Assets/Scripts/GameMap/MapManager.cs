using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Sockets;

public class MapManager : MonoBehaviour {

    public Material roadMaterial;

    public static MapManager Instance { set; get; }

    ServerSocket server = ServerSocket.Instance;
    
    const int MAP_SCALE_FACTOR = 20000;


    //TODO: SET THEM DYNAMICALLY
    const float LATITUDE_OFFSET = 51.1F;        
    const float LONGITUDE_OFFSET = 17.09F;

    /*
     * We store all the chunk object references inside this dictionary
     */
    Dictionary<Vector2, GameObject> chunks = new Dictionary<Vector2, GameObject>();


    /*
     * Dynamic struct that will change during ruyntime are stored separately
     * the dictionary key is the ID of the struct
     */
    Dictionary<int, GameObject> dynamicStructs = new Dictionary<int, GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start () {
        server = ServerSocket.Instance;
    }
	
	// Update is called once per frame
	void Update () {

	}

    public void sendChunkRequest(float longitude, float latitude)
    {
        if(isChunkLoadedOnCoords(longitude, latitude))
        {
            Debug.Log("Chunk already there, not loading!");
        } else {

            server.send(gameObject, JsonUtility.ToJson(new MapRequestData(longitude, latitude)), mapDataCallbackFunction);
            server.send(gameObject, JsonUtility.ToJson(new DynamicStructsRequestData(longitude, latitude)), structsDataCallbackFunction);

        }


    }

    // retrieve chunk data class depending on given position
    Request.callbackFunc mapDataCallbackFunction = new Request.callbackFunc((GameObject sender, string error, string data) =>
    {

        //Debug.Log(data);
        var chunkData = JsonUtility.FromJson<Assets.ChunkData>(data);

        //Debug.Log(chunkData.latitude_lower_bound);

        sender.GetComponent<MapManager>().drawChunk(chunkData);

    });

    /*
     * Retrieve dynamic chunks that are located on a given chunk
     */
    public Request.callbackFunc structsDataCallbackFunction = new Request.callbackFunc((GameObject sender, string error, string data) =>
    {
        Debug.Log(data);
        DynamicStructsResponseData structsData = JsonUtility.FromJson<DynamicStructsResponseData>(data);

        foreach( var structureData in structsData.structures)
        {

            MapManager.Instance.addOrUpdateStruct(structureData);
        }
    });

    void addOrUpdateStruct(DynamicStructData structData)
    {
        if(dynamicStructs.ContainsKey(structData.id)) {

            Destroy(dynamicStructs[structData.id]);
            dynamicStructs.Remove(structData.id);

        }

        // We have no actual game models so I won't spawn any random models
        // I'll use primitives

        /*
        GameObject structureObject = Instantiate(element, new Vector3(
            LatitudeToGameCoordinate(structData.lat),
            0,
            LongitudeToGameCoordinate(structData.lon)), new Quaternion(0,0,0,0));
            */

        GameObject structureObject = null;

        if (structData.taken_over)
        {
            structureObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Debug.Log("Creating a taken over object!");
        } else
        {
            structureObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            Debug.Log("Creating a free object!");
        }

        // Save the object's data as one of the objects components
        DynamicStruct structureObjectScript = structureObject.AddComponent(typeof(DynamicStruct)) as DynamicStruct;
        structureObjectScript.data = structData;


        structureObject.transform.position = new Vector3(
            LatitudeToGameCoordinate(structData.lat),
            3,
            LongitudeToGameCoordinate(structData.lon)
        );

        //TODO: FIX THE SCALING
        structureObject.transform.localScale = new Vector3(5, 5, 5);

        dynamicStructs.Add(structData.id, structureObject);
    }

    public void drawChunk(Assets.ChunkData chunkData)
    {
        Vector2 key = new Vector2(chunkData.longitude_lower_bound, chunkData.latitude_lower_bound);
        if (chunks.ContainsKey(key))
        {
            Debug.Log("Recreating chunk at " + key);
            Destroy(chunks[key]);
            chunks.Remove(key);
            
        }


        var emptyChunk = new GameObject("Chunk");
        chunks.Add(key, emptyChunk);

        //TODO: ENABLE THIS BACK AFTER WE HAVE TEXTURES
        //GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        //plane.transform.parent = emptyChunk.transform;

        foreach (var road in chunkData.roads)
        {
            var roadObject = new GameObject();
            roadObject.transform.parent = emptyChunk.transform;

            roadObject.AddComponent<LineRenderer>();

            var lines = roadObject.GetComponent<LineRenderer>();
            lines.transform.Rotate(new Vector3(90, 0, 0));
            lines.alignment = LineAlignment.Local;
            lines.startWidth= 1F;
            lines.endWidth = 1F;

            // A simple 2 color gradient with a fixed alpha of 1.0f.
            float alpha = 1.0f;
            Gradient gradient = new Gradient();
            gradient.SetKeys(
                new GradientColorKey[] { new GradientColorKey(Color.green, 0.0f), new GradientColorKey(Color.red, 1.0f) },
                new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
                );
            lines.colorGradient = gradient;

            /*
            Material whiteDiffuseMat = roadMaterial;
            lines.material = whiteDiffuseMat;
            */

            //Material whiteDiffuseMat = new Material(Shader.Find("Unlit/Texture"));
            //lines.material = whiteDiffuseMat;
            lines.material = roadMaterial;
            lines.textureMode = LineTextureMode.RepeatPerSegment;
            lines.material.mainTextureScale = new Vector2(1.0f, 1.0f);
            

            lines.useWorldSpace = true;

            lines.positionCount = road.nodes.Count;

            for (int i = 0; i < road.nodes.Count; i++)
            {
                lines.SetPosition(i, new Vector3(
                    LatitudeToGameCoordinate(road.nodes[i].lat),
                    0,
                    LongitudeToGameCoordinate(road.nodes[i].lon)
                    ));
            }
        }

    }

    public static float LatitudeToGameCoordinate(float lat)
    {
        return (lat - LATITUDE_OFFSET) * MAP_SCALE_FACTOR;
    }

    public static float LongitudeToGameCoordinate(float lon)
    {
        return (lon - LONGITUDE_OFFSET) * MAP_SCALE_FACTOR;
    }

    bool isChunkLoadedOnCoords(float lon, float lat)
    {
        if (chunks.ContainsKey(new Vector2(
            roundDownToChunkCords(lon),
            roundDownToChunkCords(lat)
            )))
            return true;

        return false;
    }

    // TODO: Move into utils ore something
    float roundDownToChunkCords(float x)
    {
        return Mathf.Floor(x * 100) / 100;
    }


    // Used by to get the object's data when the user clicks on it
    DynamicStructData GetDataOfObject(GameObject target)
    {
        try {

            DynamicStruct targetData = target.GetComponent<DynamicStruct>();
            return targetData.data;

        } catch (System.Exception e)
        {
            Debug.LogError(e);
            return null;
        }
    }
}
