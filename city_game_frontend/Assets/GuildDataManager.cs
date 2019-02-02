using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuildDataManager : MonoBehaviour {

    public GuildData guildData;
    public static GuildDataManager Instance;

    private void Awake()
    {
        Instance = this;
    }

}
