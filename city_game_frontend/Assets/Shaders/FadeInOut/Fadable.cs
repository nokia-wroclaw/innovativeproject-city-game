﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Sockets;
using UnityEditor;


public class Fadable : MonoBehaviour
{
    public float animationTime = 1.5f; // in seconds

    public bool visible {  get; private set; }

    public Fadable()
    {
    }
    

    public void show(float delay = 0)
    {
        Renderer renderer = GetComponent<Renderer>();

        //get time vector
        Vector4 time = Shader.GetGlobalVector("_Time");

        //set params in shader
        renderer.material.SetFloat("_StartTime", time.y+delay);
        renderer.material.SetFloat("_Duration", animationTime);
        renderer.material.SetFloat("_Direction", 1);
        visible = true;
    }

    public void hide(float delay = 0)
    {
        Renderer renderer = GetComponent<Renderer>();
        
        //same as above
        Vector4 time = Shader.GetGlobalVector("_Time");

        renderer.material.SetFloat("_StartTime", time.y + delay);
        renderer.material.SetFloat("_Duration", animationTime);
        renderer.material.SetFloat("_Direction", 0);
        visible = false;
    }

}
