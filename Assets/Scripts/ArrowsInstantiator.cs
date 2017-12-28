using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowsInstantiator : MonoBehaviour {

    public GameObject fletxa;
    public float velocitatDisparo;
    public int rangTorre;

    private GameObject[] objectius;

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("crearFletxa", velocitatDisparo, velocitatDisparo);
    }

    void crearFletxa()
    {
        objectius = GameObject.FindGameObjectsWithTag("Enemy");
        if (objectius.Length != 0)
        {
            int i = 0;
            bool trobat = false;
            while (i < objectius.Length&&!trobat)
            {
                if (objectius[i].GetComponent<EnemyInterface>().getVida() > 0)
                {
                    if ((objectius[i].transform.position - transform.position).magnitude < rangTorre)
                    {
                        Instantiate(fletxa, transform.position, Quaternion.LookRotation(objectius[i].transform.position - transform.position));
                        trobat = true;
                    }
                }
                i++;
            }
        }
    }
}
