using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneralControl : MonoBehaviour {

    private float minim;
    private GameObject[] objectius;
    private bool destinationNova;

    protected GameObject destination;
    protected int contador = 0;
    protected UnityEngine.AI.NavMeshAgent agent;
    protected Animator animator;
    public int health;

    public float maximBusqueda;
    public float velocitatMoviment;
    public int velocitatAtac;

    protected void findClosestTarget(string tag, float maximBusqueda)
    {
        //buscar objectiu mes proper
        objectius = GameObject.FindGameObjectsWithTag(tag);
        minim = maximBusqueda;
        destinationNova = false;
        for (int i = 0; i < objectius.Length; i++)
        {
            if ((objectius[i].transform.position - transform.position).magnitude < minim)
            {
                if (objectius[i].GetComponent<HealthInterface>().getVida() > 0)
                {
                    destinationNova = true;
                    minim = (transform.position - objectius[i].transform.position).magnitude;
                    destination = objectius[i];
                }
            }
        }
        if (!destinationNova) destination = GameObject.Find("Player");
    }

    protected void notifyDeath() {
        GameObject.Find("InstantiatorsEnemics").GetComponent<WaveManager>().notifyDeath();
    }

}
