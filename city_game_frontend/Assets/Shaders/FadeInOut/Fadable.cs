﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Sockets;
using UnityEditor;


public class Fadable : MonoBehaviour
{
    public int intensity = 10;
    public int range = 10;
    
    const float animationTime = 5f; // in seconds

    public Shader fadeInOutShader;// = null;
    public Shader defaultShader;// = null;

    public int _fade = 0; //if 1 _fade in if -1 _fade out
    public int shader = 0;


    public bool visible {  get; private set; }

    public Fadable()
    {
    }

    void Start()
    {
    }

    void Update()
    {

        if (_fade == 1)
        {
            Debug.Log("_fade");

            show();
            _fade = 0;
        }
        else if (_fade == -1)
        {
            hide();
            _fade = 0;
        }

        if (shader != 0)
        {
            swichShaderDefault();
            shader = 0;
        }

        //Renderer renderer = GetComponent<Renderer>();

        //Vector4 time = Shader.GetGlobalVector("_Time");
    }

    //because Shader.Find can not be run from constructor
    public void init()
    {
        //_fadeInOutShader = Shader.Find("Custom/_fadeInOut");
        //defaultShader = Shader.Find("Custom/_fadeInOutNoTrans");
    }

    //Use it to remove drawing order issue
    public void swichShaderDefault()
    {
        init();

        Renderer renderer = GetComponent<Renderer>();
        renderer.material.shader = defaultShader;

        Light light = GetComponentInChildren<Light>();
        light.intensity = intensity;
        light.range = range;
        light.enabled = true;

        ParticleSystem emiter = GetComponentInChildren<ParticleSystem>();

        if(emiter != null)
            emiter.enableEmission = true;
    }

    public void swichShader_fadeInOut()
    {
        init();

        Renderer renderer = GetComponent<Renderer>();
        renderer.material.shader = fadeInOutShader;

        Light light = GetComponentInChildren<Light>();
        light.intensity = 0;
        light.range = 0;
        light.enabled = false;


        ParticleSystem emiter = GetComponentInChildren<ParticleSystem>();
        if(emiter != null)
            emiter.enableEmission = false;
    }



    [ContextMenu("Show")]
    public void show(float delay = 0)
    {
        init();

        Renderer renderer = GetComponent<Renderer>();



        renderer.material.shader = fadeInOutShader;
        //get time vector
        Vector4 time = Shader.GetGlobalVector("_Time");

        //set params in shader
        renderer.material.SetFloat("_StartTime", time.y+delay);
        renderer.material.SetFloat("_Duration", animationTime);
        renderer.material.SetFloat("_Direction", 1);
        visible = true;

        //switch shader to default
        Invoke("swichShaderDefault", animationTime *(float)0.9);
    }

    [ContextMenu("Hide")]
    public void hide(float delay = 0)
    {
        init();

        Renderer renderer = GetComponent<Renderer>();
        renderer.material.shader = fadeInOutShader;
        //same as above
        Vector4 time = Shader.GetGlobalVector("_Time");

        renderer.material.SetFloat("_StartTime", time.y + delay);
        renderer.material.SetFloat("_Duration", animationTime);
        renderer.material.SetFloat("_Direction", 0);
        visible = false;



        //switch shader to 
        Invoke("swichShader_fadeInOut", animationTime);
    }

    public void destroyAfterTime(float time = animationTime) // time in seconds
    {
        Invoke("destroyMe", time);
    }

    private void destroyMe()
    {
        Destroy(this.gameObject);
    }

}
