using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockInstantiator : TorreBomba {

    public GameObject rock;
    public float velocidadDisparo;

    private bool preparades = true, preparantRoques = false;

    void Update()
    {
        if (preparades)
        {
            crearRoques();
        }
        else if (!preparantRoques)
        {
            Invoke("prepararRoques", velocidadDisparo);
            preparantRoques = true;
        }
    }

    void prepararRoques()
    {
        preparades = true;
        preparantRoques = false;
    }

    void crearRoques()
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
                        preparades = false;
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
