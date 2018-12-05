using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StructureTakeoverRequestData
{
    public int type = Const.MESSAGE_TYPE_STRUCT_TAKEOVER_REQUEST;
    public int id;

    // TODO
    public StructureTakeoverRequestData(int structToTakeOverID)
    {
        this.id = structToTakeOverID;
    }
}
