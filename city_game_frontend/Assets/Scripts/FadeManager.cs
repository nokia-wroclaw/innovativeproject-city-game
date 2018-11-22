using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Fadable : MonoBehaviour {

    public void startFade()
    {
        int gameTime = 0; // some fancy time getter
        Debug.Log("Fadin");
    }

}


public class FadeManager : Fadable {

    public bool STOP = false;

    public class AnimateObject
    {
        public AnimateObject(GameObject o) {
            obj = o;
            status = 1;
            direction = -1;
            done = false;
        }
        public GameObject obj;
        public float status; //actual animation _Level
        public int direction; //-1 hiding 1 showing
        public bool done; //1 if animation is done 
    }

    public GameObject[] _InitObjects; //list of objects passed by inspector
    public int animationDuration = 1000; //animation duration time in ms
    private Assets.TimerSync timer = new Assets.TimerSync();

    //only for testing. Be careful no index checking
    public int _ShowIndex = -1; //index to be shown in next frame
    public int _HideIndex = -1; //index to be hide in next frame

    private List<AnimateObject> objects = new List<AnimateObject>();
    
    // Use this for initialization
    void Start () {

        startFade();
        //rewrite gameobject array into list object with animation status params
        foreach (GameObject o in _InitObjects)
        {
            Renderer renderer = o.GetComponent<Renderer>();
            renderer.material.SetFloat("_FadeFromTime", 0.0f);
            renderer.material.SetFloat("_Level", 1.0f); //hide object

            AnimateObject newObj = new AnimateObject(o);
            objects.Add(newObj);
        }
    }

    // Update is called once per frame
    void Update() {
        //calculate time from last frame
        double delta = timer.deltaTime();

        //only for testing 
        if (_ShowIndex != -1)
        {
            show(_ShowIndex);
            _ShowIndex = -1;
        }

        if(_HideIndex != -1)
        {
            hide(_HideIndex);
            _HideIndex = -1;
        }

        foreach(AnimateObject a in objects)
        {
            //play animation
            if(a.done == false)
            {

                a.status += (float)delta / animationDuration * a.direction;

                Renderer renderer = a.obj.GetComponent<Renderer>();
                if (!STOP)
                    renderer.material.SetFloat("_Level", a.status); //set level in shader

                //animation done
                if (a.status < -0.20f) a.done = true; 
                if (a.status > 1.20f) a.done = true;
            }
        }

        timer.update();
    }

    //1 if showing or shown, -1 if hiding or hidden
    public int status(int index)
    {
        return objects[index].direction;
    }


    //in order to show object run this method
    public void show(int index)
    {
        objects[index].done = false;
        objects[index].direction = 1;
    }

    //in order to hide object run this method
    public void hide(int index)
    {
        objects[index].done = false;
        objects[index].direction = -1;
    }
}
