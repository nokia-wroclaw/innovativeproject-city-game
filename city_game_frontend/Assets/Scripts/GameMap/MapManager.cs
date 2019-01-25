using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Sockets;

public class MapManager : MonoBehaviour
{

    public Material roadMaterial;

    public GameObject orePrefab;
    public GameObject minePrefab;
    public GameObject otherPlayerModel;
    public GameObject playerPlacedStructPrefab;


    public static MapManager Instance { set; get; }

    ServerSocket server = ServerSocket.Instance;

    public static int MAP_SCALE_FACTOR = 20000;


    //TODO: SET THEM DYNAMICALLY
    public static float LATITUDE_OFFSET = 51.1F;
    public static float LONGITUDE_OFFSET = 17.09F;

    /*
     * We store all the chunk object references inside this dictionary
     */
    Dictionary<Vector2, GameObject> chunks = new Dictionary<Vector2, GameObject>();


    /*
     * Dynamic struct that will change during runtime are stored separately
     * the dictionary key is the ID of the struct
     */
    Dictionary<int, GameObject> dynamicStructs = new Dictionary<int, GameObject>();


    /*
     * Dictionary with the players displayed on the map.
     * That means - the players which are in the same team as the client
     */
    Dictionary<int, GameObject> guildPlayersDisplayedOnMap = new Dictionary<int, GameObject>();


    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start()
    {
        server = ServerSocket.Instance;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void sendChunkRequest(float longitude, float latitude)
    {
        if (isChunkLoadedOnCoords(longitude, latitude))
        {
            Debug.Log("Chunk already there, not loading!");
        }
        else
        {

            server.send(gameObject, JsonUtility.ToJson(new MapRequestData(longitude, latitude)), mapDataCallbackFunction);
            server.send(gameObject, JsonUtility.ToJson(new DynamicStructsRequestData(longitude, latitude)), structsDataCallbackFunction);

        }


    }

    // retrieve chunk data class depending on given position
    Request.callbackFunc mapDataCallbackFunction = new Request.callbackFunc((GameObject sender, string error, string data) =>
    {

        //Debug.Log(data);
        Debug.Log("EROR MIGHT BE HERE");
        var chunkData = JsonUtility.FromJson<Assets.ChunkData>(data);

        Debug.Log("???????");
        //Debug.Log(chunkData.latitude_lower_bound);

        sender.GetComponent<MapManager>().drawChunk(chunkData);

    });

    /*
     * Retrieve dynamic structs that are located on a given chunk
     */
    public Request.callbackFunc structsDataCallbackFunction = new Request.callbackFunc((GameObject sender, string error, string data) =>
    {
        //Debug.LogWarning("DYNAMIC STRUCTS UPDATE");
        //Debug.Log(data);
        DynamicStructsResponseData structsData = JsonUtility.FromJson<DynamicStructsResponseData>(data);

        foreach (var structureData in structsData.structures)
        {

            MapManager.Instance.addOrUpdateStruct(structureData);
        }
    });

    void addOrUpdateStruct(DynamicStructData structData)
    {
        if (dynamicStructs.ContainsKey(structData.id))
        {

            //dynamicStructs[structData.id].GetComponent<Fadable>().hide();
            //dynamicStructs[structData.id].GetComponent<Fadable>().destroyAfterTime();
            //Destroy(dynamicStructs[structData.id]);
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

        if(structData.resource_type == Const.RESOURCE_TYPE_4) // aoe buff
        {
            structureObject = Instantiate(playerPlacedStructPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        }
        else if (structData.taken_over)
        {
            structureObject = Instantiate(minePrefab, new Vector3(0, 0, 0), Quaternion.identity); //GameObject.CreatePrimitive(PrimitiveType.Cube);
            //Debug.Log("Creating a taken over object!");

        }
        else
        {
            Debug.Log("ore object created");
            structureObject = Instantiate(orePrefab, new Vector3(0, 0, 0), Quaternion.identity); //GameObject.CreatePrimitive(PrimitiveType.Cube);
            //Debug.Log("Creating a free object!");
        }


        // Save the object's data as one of the objects components
        DynamicStruct structureObjectScript = structureObject.AddComponent(typeof(DynamicStruct)) as DynamicStruct;
        structureObjectScript.data = structData;

        
        structureObject.transform.Rotate(new Vector3(-95.905F, 0,0));

        Utils.rotationThatWorks(structureObject, new Vector3(0, structData.rotation, 0));

        //structureObject.GetComponent<Fadable>().show();


        structureObject.transform.position = new Vector3(
            Utils.LatitudeToGameCoordinate(structData.lat),
            0.09580898F,
            Utils.LongitudeToGameCoordinate(structData.lon)
        );

        //TODO: FIX THE SCALING
        //structureObject.transform.localScale = new Vector3(100, 100, 100);

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
            lines.startWidth = 2F;
            lines.endWidth = 2F;

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
                    Utils.LatitudeToGameCoordinate(road.nodes[i].lat),
                    0,
                    Utils.LongitudeToGameCoordinate(road.nodes[i].lon)
                    ));
            }
        }

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
    public static DynamicStructData GetDataOfObject(GameObject target)
    {
        try
        {

            DynamicStruct targetData = target.GetComponent<DynamicStruct>();
            Debug.Log(targetData.data.tier);
            return targetData.data;

        }
        catch (System.Exception)
        {
            return null;
        }
    }

    public void handleGuildMemberLocationUpdate(string data)
    {
        Assets.GuildMemberLocationData newData = JsonUtility.FromJson<Assets.GuildMemberLocationData>(data);

        Debug.Log("update on " + newData.nick);

        if (guildPlayersDisplayedOnMap.ContainsKey(newData.id))
        {
            GameObject playerToUpdate = guildPlayersDisplayedOnMap[newData.id];

            playerToUpdate.GetComponent<SmoothMovement>().setTargetPosition(
                Utils.LatitudeToGameCoordinate(newData.lat),
                Utils.LongitudeToGameCoordinate(newData.lon)
            );

            playerToUpdate.GetComponent<SmoothMovement>().setTargetRotation(
                newData.rotation
            );
        }
        else
        {
            GameObject newDisplayedGuildMember = Instantiate(otherPlayerModel, new Vector3(
                  Utils.LatitudeToGameCoordinate(newData.lat),
                4.659256F,
                Utils.LongitudeToGameCoordinate(newData.lon)
            ), Quaternion.Euler(-89.98F, 0, 0)
                );

            guildPlayersDisplayedOnMap[newData.id] = newDisplayedGuildMember;
        }
    }
}
