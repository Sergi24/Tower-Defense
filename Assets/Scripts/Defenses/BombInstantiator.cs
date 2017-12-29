using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombInstantiator : MonoBehaviour {

    public GameObject bomba;
    public float velocidadDisparo;
    public int rangoTorre;

    private GameObject[] objectius;

    // Use this for initialization
    void Start()
    {
        crearBomba();
        InvokeRepeating("crearBomba", velocidadDisparo, velocidadDisparo);
    }

    void crearBomba()
    {
        objectius = GameObject.FindGameObjectsWithTag("Enemy");
        if (objectius.Length != 0)
        {
            int i = 0;
            bool trobat = false;
            while (i < objectius.Length && !trobat)
            {
                if (objectius[i].GetComponent<HealthInterface>().getVida() > 0)
                {
                    if ((objectius[i].transform.position - transform.position).magnitude < rangoTorre)
                    {
                        Instantiate(bomba, transform.position, transform.rotation);
                        trobat = true;
                    }
                }
                i++;
            }
        }
    }
}
