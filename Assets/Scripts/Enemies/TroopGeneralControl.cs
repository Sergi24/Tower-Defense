﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopGeneralControl : MonoBehaviour {

    private float minim;
    private GameObject[] objectius;
    private bool destinationNova;

    protected GameObject destination = null;
    protected UnityEngine.AI.NavMeshAgent agent;
    protected Animator animator;
    protected float vidaARestarDeBarra;
    
    public int health;
    public float maximBusqueda;
    public float velocitatMoviment;
    public int rangAtac;
    public float rotationSpeed;
    public int damage;
    public int dinersASumar;
    public GameObject barraVida;

    protected void assignarVidaARestar()
    {
        barraVida.GetComponent<MeshRenderer>().enabled = false;
        vidaARestarDeBarra = barraVida.transform.localScale.y/health;
    }

    protected void restarVidaBarra(int vidaARestar)
    {
        if (health>0) barraVida.GetComponent<MeshRenderer>().enabled = true;
        barraVida.transform.localScale = new Vector3(barraVida.transform.localScale.x, barraVida.transform.localScale.y - (vidaARestarDeBarra * vidaARestar), barraVida.transform.localScale.z);
    } 

    protected bool findClosestTarget(string tag, float maximBusqueda)
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
        if (!destinationNova) return false;
        else return true;
    }

    protected bool findClosestTargetWithoutRange(string tag)
    {
        //buscar objectiu mes proper
        objectius = GameObject.FindGameObjectsWithTag(tag);
        minim = 100000;
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

    protected void notifyDeath() {
        GameObject.Find("InstantiatorsEnemics").GetComponent<WaveManager>().notifyDeath();
    }

}
