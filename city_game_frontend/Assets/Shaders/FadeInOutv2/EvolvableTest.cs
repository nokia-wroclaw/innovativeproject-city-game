using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Sockets;
using UnityEditor;


class EvolvableTest : MonoBehaviour
{
    public int evolve = 0;

    public Fadable basic;
    public Fadable extend;

    void Update()
    {
        if (evolve != 0)
        {
            if (evolve == 1)
            {
                Evolvable.evolve(basic, extend);
            }
            else if (evolve == -1)
            {
                Evolvable.evolve(extend, basic);
            }

            evolve = 0;
        }
    }

}