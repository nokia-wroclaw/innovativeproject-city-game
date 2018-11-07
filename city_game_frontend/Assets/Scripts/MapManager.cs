using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {

    public ServerSocket server;
   // public ChunkManager chunkManager;
   public static List<Assets.ChunkData> chunksToDraw = new List<Assets.ChunkData>();

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (chunksToDraw.Count > 0)
        {
            Debug.Log("Can draw");
            redrawMap(chunksToDraw[0]); //TODO AFRICA
            chunksToDraw.RemoveAt(0);
        }
	}



    public void redrawMap(Assets.ChunkData chunkData)
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
                    (road.nodes[i].lat - chunkData.latitude_lower_bound)*2000, 
                    0, 
                    (road.nodes[i].lon - chunkData.longitude_lower_bound)*2000));
            }
        }

    }
}
