using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;




public class FadeManager : MonoBehaviour{

    public int swap;
    public float delay = -1;
    public Fadable o1;
    public Fadable o2;
    

    /**
     * Funcion replace on actualObj with newObj
     * delay if -1 the delay is equals to actualObject fade time, if is not negative then
     *       is delay
     */
    static public void evolve(Fadable actualObj, Fadable newObj, float delay = -1) {
        if (actualObj == null || newObj == null) return;
        if (delay < 0) delay = actualObj.animationTime;
        //Debug.Log("evolving started: "+ actualObj.animationTime);

        actualObj.hide(delay);
        newObj.show(delay);
    }
  
    void Start()
    {
        o1.show();
        o2.hide();
    }

    void Update()
    {

        if (swap == 0) return;
        swap = 0;

        Debug.Log(o1.visible + ", " + o2.visible);

        if (o1.visible)
        {
            FadeManager.evolve(o1, o2, delay);
        }
        else
        {
            FadeManager.evolve(o2, o1, delay);
        }

    }

}
