using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Sockets;
using UnityEditor;


class Evolvable : MonoBehaviour
{

    public Fadable[] states; //all stades that the object could be
    public int state = -1; //actual state the object is. 

    public int _Evolve = 0;

    void Start()
    {
        
    }


    public void evolveUp()
    { 
        if (state >= states.Length -1) return;

        state++;

        if (state >= 1)
            states[state - 1].hide();

        states[state].show();

        Debug.Log("evolve");
    }

    public void evolveDown()
    {
        if (state < 0) return;

        state--;
        states[state + 1].hide();

        if(state >= 0)
            states[state].show();

    }

    void Update()
    {
        if(_Evolve != 0)
        {
            if(_Evolve == -1)
            {
                evolveDown();
            }else
            {
                evolveUp();
            }

            _Evolve = 0;
        }
    }

}
