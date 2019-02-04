using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonController : MonoBehaviour {

    MeshRenderer r;
    TextMesh t;
    SpriteRenderer sr;

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

    void setIconSquare()
    {
        sr.sprite = hexagonSpawner.Instance.s2;
    }

    void setIconCircle()
    {
        sr.sprite = hexagonSpawner.Instance.s1;
    }

	// Use this for initialization
	void Start () {
        float timeToDie = /* insert Blade Runner reference */
            Vector3.Distance(
                GameManager.Instance.locationIndicator.transform.position,
                transform.position
                ) / 250;

        Invoke("appearOnMap", timeToDie);
        Destroy(this.gameObject, 14 - timeToDie);

        r = GetComponentInChildren<MeshRenderer>();
        t = GetComponentInChildren<TextMesh>();
        sr = GetComponentInChildren<SpriteRenderer>();

        t.fontSize = 0;


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

            switch(owner.icon[0])
            {
                case 'c':
                    setIconCircle();
                    break;
                default:
                    setIconSquare();
                    break;
            }


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
        t.fontSize = 128;
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
