using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallInstantiator : MonoBehaviour {

    public GameObject ball;
    public float velocitatDisparo;
    public int rangAvast;

    private GameObject[] objectius;

    // Use this for initialization
    void Start()
    {
      //  InvokeRepeating("crearBola", velocitatDisparo, velocitatDisparo);
    }

    public void crearBola()
    {
        objectius = GameObject.FindGameObjectsWithTag("Defensa");
        if (objectius.Length != 0)
        {
            int i = 0;
            bool trobat = false;
            while (i < objectius.Length && !trobat)
            {
            //    if (objectius[i].GetComponent<EnemyInterface>().getVida() > 0)
            //    {
                    if ((objectius[i].transform.position - transform.position).magnitude < rangAvast)
                   {
                        Instantiate(ball, transform.position, Quaternion.LookRotation(objectius[i].transform.position - transform.position));
                        trobat = true;
                    }
           //     }
                i++;
            }
        }
    }
}
