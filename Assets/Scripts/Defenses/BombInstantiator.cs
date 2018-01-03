using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombInstantiator : MonoBehaviour {

    public GameObject bomba;
    public float velocidadDisparo;
    public int rangoTorre;

    private bool preparat = true, preparantBomba = false;

    private GameObject[] objectius;

    void Update()
    {
        if (preparat)
        {
            crearBomba();
        }
        else if (!preparantBomba)
        {
            Invoke("prepararBomba", velocidadDisparo);
            preparantBomba = true;
        }
    }

    void prepararBomba()
    {
        preparat = true;
        preparantBomba = false;
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
                        preparat = false;
                        trobat = true;
                    }
                }
                i++;
            }
        }
    }
}
