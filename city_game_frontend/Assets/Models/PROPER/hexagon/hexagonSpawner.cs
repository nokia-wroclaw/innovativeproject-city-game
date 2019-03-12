using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class hexagonSpawner : MonoBehaviour {
    

    public GameObject hexagon;
    const int howManyGons = 4;
    public float Xoffseter;
    public float Yoffseter;

    public static hexagonSpawner Instance;

    public Material red, purple, blue, green;
    public Sprite s1, s2, s3;

    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start () {
        
	}
    
    [ContextMenu("spawn")]
    public void spawn()
    {
        Vector3 position = GameManager.Instance.locationIndicator.transform.position;

        spawnGons(position);
    }
	
	public void spawnGons(Vector3 position)
    {
        position += new Vector3(howManyGons * Xoffseter, 0, howManyGons * Yoffseter);

        int name = 0;
        int Zoffset = 0;
        for(int j = -howManyGons; j < howManyGons; j++) { 
            for(int i = -howManyGons *2; i < howManyGons*2; i++)
            {
                GameObject a = Instantiate(hexagon);
                a.transform.position = position + new Vector3(i * 32, -1, Zoffset);
                a.name = "h " + name++;


                GameObject b = Instantiate(hexagon);
                b.transform.position = position + new Vector3(i * 32 + 16, -1, Zoffset+28);
                b.name = "h " + name++;



            }
            Zoffset += 56;
        }

    }
}
