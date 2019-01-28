using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour {

    public static EffectsManager Instance;

    public GameObject wizardTakeoverEffect;
    public GameObject thiefTakeoverEffect;
    public GameObject banditTakeoverEffect;

    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void spawnEffect(Vector3 coords, float seconds_to_destroy)
    {
        GameObject effect = Instantiate(wizardTakeoverEffect);
        effect.transform.position = coords + Vector3.up * 3;


     
        Destroy(effect, seconds_to_destroy + 2);
    }
}
