using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Sockets;
using UnityEditor;


public class Fadable : MonoBehaviour
{
    public float animationTime = 1.5f; // in seconds

    Shader fadeInOutShader = null;
    Shader defaultShader = null;

    public bool visible {  get; private set; }

    public Fadable()
    {
    }

    //because Shader.Find can not be run from constructor
    public void init()
    {
        fadeInOutShader = Shader.Find("Custom/FadeInOut");
        defaultShader = Shader.Find("Standard");
    }

    //Use it to remove drawing order issue
    public void swichShaderDefault()
    {
        init();

        Renderer renderer = GetComponent<Renderer>();
        renderer.material.shader = defaultShader;
    }

    public void swichShaderFadeInOut()
    {
        init();

        Renderer renderer = GetComponent<Renderer>();
        renderer.material.shader = fadeInOutShader;
    }

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
        Invoke("swichShaderDefault", animationTime);
    }

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
        Invoke("swichShaderFadeInOut", animationTime);
    }

}
