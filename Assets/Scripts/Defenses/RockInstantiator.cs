using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockInstantiator : TorreBomba {

    public GameObject rock;
    public float velocidadDisparo;

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("crearRoca", velocidadDisparo, velocidadDisparo);
    }

    void crearRoca()
    {
        objectius = GameObject.FindGameObjectsWithTag("Enemy");
        bool trobat = false;
        if (objectius.Length != 0)
        {
            int i = 0;
            while (i < objectius.Length && !trobat)
            {
                if (objectius[i].GetComponent<HealthInterface>().getVida() > 0)
                {
                    float distancia = (objectius[i].transform.position - transform.position).magnitude;
                    if (distancia < rangoTorreMaximo&&distancia>rangoTorreMinimo)
                    {
                        Instantiate(rock, transform.position, transform.rotation);
                        trobat = true;
                    }
                }
                i++;
            }
        }

        if (!trobat)
        {
            objectius = GameObject.FindGameObjectsWithTag("Dragon");
            if (objectius.Length != 0)
            {
                int i = 0;
                while (i < objectius.Length && !trobat)
                {
                    if (objectius[i].GetComponent<HealthInterface>().getVida() > 0)
                    {
                        float distancia = (objectius[i].transform.position - transform.position).magnitude;
                        if (distancia < rangoTorreMaximo && distancia > rangoTorreMinimo)
                        {
                            Instantiate(rock, transform.position, transform.rotation);
                            trobat = true;
                        }
                    }
                    i++;
                }
            }
        }
    }
}
