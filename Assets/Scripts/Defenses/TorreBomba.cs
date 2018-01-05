using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorreBomba : TowerHealth {

    protected GameObject[] objectius;
    protected GameObject destination = null;
    public int rangoTorreMinimo;
    public int rangoTorreMaximo;
    public int rotationSpeed;

    // Use this for initialization
    void Start () {
        assignarVidaARestar();
        assignarSo();
    }
	
	// Update is called once per frame
	void Update () {
        if (!destroyed) {
            if (objectiuMesProper()) {
                Vector3 puntoDeChoque = new Vector3(destination.transform.position.x, transform.position.y, destination.transform.position.z);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(puntoDeChoque - transform.position), rotationSpeed * Time.deltaTime);
            }
        }
        else
        {
            enterrarTorre();
        }
    }

    protected bool objectiuMesProper()
    {

        objectius = GameObject.FindGameObjectsWithTag("Enemy");
        //buscar objectiu mes proper
        bool trobat = false;
        float minimEnemic = 10000f;
        for (int i = 0; i < objectius.Length; i++)
        {
            float distancia = (objectius[i].transform.position - transform.position).magnitude;
            if (distancia < minimEnemic && distancia > rangoTorreMinimo && objectius[i].transform.position.x > -12)
            {
                if (objectius[i].GetComponent<HealthInterface>().getVida() > 0)
                {
                    minimEnemic = (transform.position - objectius[i].transform.position).magnitude;
                    destination = objectius[i];
                    trobat = true;
                }
            }
        }

        if (!trobat)
        {
            objectius = GameObject.FindGameObjectsWithTag("Dragon");
            //buscar objectiu mes proper
            float minimDrac = 10000f;
            for (int i = 0; i < objectius.Length; i++)
            {
                float distancia = (objectius[i].transform.position - transform.position).magnitude;
                if (distancia < minimDrac && distancia > rangoTorreMinimo && objectius[i].transform.position.x > -12)
                {
                    if (objectius[i].GetComponent<HealthInterface>().getVida() > 0)
                    {
                        minimDrac = (transform.position - objectius[i].transform.position).magnitude;
                        destination = objectius[i];
                        trobat = true;
                    }
                }
            }
        }

     //   if (minimDrac < minimEnemic) destination = destinationDrac;
     //   else destination = destinationEnemic;
        return trobat;
    }
}
