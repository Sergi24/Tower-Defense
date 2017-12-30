using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopGeneralControl : MonoBehaviour {

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
    public int rangAtac;
    public float rotationSpeed;

    protected bool findClosestTarget(string tag, float maximBusqueda)
    {
        //buscar objectiu mes proper
        objectius = GameObject.FindGameObjectsWithTag(tag);
        minim = maximBusqueda;
        //Tot el mapa
        if (maximBusqueda == -1)
        {
            destination = objectius[0];
            minim = 1000;
        }
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
        if (!destinationNova) return false;
        else return true;
    }

    protected void attackWithTwoAnimations(string tag1, string tag2)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(tag1))
        {
            if (contador > velocitatAtac)
            {
                gameObject.GetComponentInChildren<BallInstantiator>().crearBola();
                contador = 0;
            }
            else contador++;
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName(tag2))
        {
            contador = 0;
        }
    }

    protected void notifyDeath() {
        GameObject.Find("InstantiatorsEnemics").GetComponent<WaveManager>().notifyDeath();
    }

}
