using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonController : MonoBehaviour {

    MeshRenderer r;
    TextMesh t;

    public void setColorBlue()
    {
        r.material = hexagonSpawner.Instance.blue;
    }

    public void setColorRed()
    {
        r.material = hexagonSpawner.Instance.red;
    }

    public void setColorPurple()
    {
        r.material = hexagonSpawner.Instance.purple;
    }

    [ContextMenu("t")]
    public void setText()
    {
        t.text = "Gildia zjebów";
    }

	// Use this for initialization
	void Start () {


        Invoke("appearOnMap", Random.Range(0.0F, 1.0F));
        Destroy(this.gameObject, 7 + Random.Range(0.0F, 1.0F));

        r = GetComponentInChildren<MeshRenderer>();
        t = GetComponentInChildren<TextMesh>();
        //Destroy(this.gameObject, 10);

        float latitude = Utils.GameCoordinateXToLatitude(transform.position.x);
        float longitude = Utils.GameCoordinateZToLongitude(transform.position.z);

        latitude = roundDownToChunkCords(latitude);
        longitude = roundDownToChunkCords(longitude);

        var newKey = new Vector2(longitude, latitude);

        if(MapManager.Instance.chunkOwners.ContainsKey(newKey))
        {
            var owner = MapManager.Instance.chunkOwners[newKey];

            if (owner.color == "" || owner.color == null)
                return;


            t.text = owner.owner_guild;

            switch(owner.color[0])
            {
                case 'r':
                    setColorRed();
                    break;
                case 'b':
                    setColorBlue();
                    break;
                case 'p':
                    setColorPurple();
                    break;
                default:
                    Debug.LogError("No color!!");
                    break;
            }
        }

    }

    void appearOnMap()
    {
        transform.Translate(Vector3.up);
    }

    // TODO: Move into utils ore something
    float roundDownToChunkCords(float x)
    {
        return Mathf.Floor(x * 100) / 100;
    }




    // Update is called once per frame
    void Update () {

    }
}
