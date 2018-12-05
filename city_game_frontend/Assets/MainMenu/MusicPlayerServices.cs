using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayerServices : MonoBehaviour {

    private static MusicPlayerServices instance = null;
    public static MusicPlayerServices Instance
    {
        get { return instance; }
    }
    
    void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }   
}
