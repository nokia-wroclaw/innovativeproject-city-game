﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Sockets;
using UnityEditor;


class FadableComponent : Fadable
{

    public int fade = 0; //if 1 fade in if -1 fade out

    void Update()
    { 
        if(fade == 1)
        {
            show();
            fade = 0;
        }
        else if(fade == -1)
        {
            hide();
            fade = 0;
        }

        Renderer renderer = GetComponent<Renderer>();

        Vector4 time = Shader.GetGlobalVector("_Time");
        Debug.Log(time);
    }
}
