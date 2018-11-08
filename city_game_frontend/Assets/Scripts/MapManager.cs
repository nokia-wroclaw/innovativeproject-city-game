using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {

    public ServerSocket server;

    const float LATITUDE_OFFSET = 51.1F;
    const float LONGITUDE_OFFSET = 17.09F;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}



    public void drawChunk(Assets.ChunkData chunkData)
    {
        var emptyChunk = new GameObject("Chunk");


        GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);

        plane.transform.parent = emptyChunk.transform;

        foreach (var road in chunkData.roads)
        {
            var roadObject = new GameObject();
            roadObject.transform.parent = emptyChunk.transform;

            roadObject.AddComponent<LineRenderer>();

            var lines = roadObject.GetComponent<LineRenderer>();
            lines.transform.Rotate(new Vector3(90, 0, 0));
            lines.alignment = LineAlignment.Local;
            lines.startWidth= 0.1F;
            lines.endWidth = 0.1F;

            // A simple 2 color gradient with a fixed alpha of 1.0f.
            float alpha = 1.0f;
            Gradient gradient = new Gradient();
            gradient.SetKeys(
                new GradientColorKey[] { new GradientColorKey(Color.green, 0.0f), new GradientColorKey(Color.red, 1.0f) },
                new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
                );
            lines.colorGradient = gradient;

            Material whiteDiffuseMat = new Material(Shader.Find("Unlit/Texture"));
            lines.material = whiteDiffuseMat;

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
        return (lat - LATITUDE_OFFSET) * 2000;
    }

    public static float LongitudeToGameCoordinate(float lon)
    {
        return (lon - LONGITUDE_OFFSET) * 2000;
    }
}
