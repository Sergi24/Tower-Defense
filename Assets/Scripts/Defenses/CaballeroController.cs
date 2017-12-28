using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaballeroController : MonoBehaviour, HealthInterface {

    private UnityEngine.AI.NavMeshAgent agent;
    private Animator animator;
    private GameObject destination;
    private GameObject[] objectius;
    private bool objectiveReached = false;
    public float velocitatMoviment;
    public float rangAtac;
    public int vidaCaballer;

    // Use this for initialization
    void Start()
    {
        agent = this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = this.gameObject.GetComponent<Animator>();
        //agent.Move();
    }

    void Update()
    {
        objectius = GameObject.FindGameObjectsWithTag("Enemy");

        if (objectius.Length != 0)
        {
            //Buscar objectiu mes proper
            float minim = 100f;
            destination = objectius[0];
            for (int i = 0; i < objectius.Length; i++)
            {
                if ((objectius[i].transform.position - transform.position).magnitude < minim)
                {
                    minim = (objectius[i].transform.position - transform.position).magnitude;
                    destination = objectius[i];
                }
            }
            agent.destination = destination.transform.position;

            //Saber si atacar ja
            if (agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathComplete && agent.remainingDistance < rangAtac)
            {
                animator.SetBool("Attack", true);
                agent.speed = 0;
                objectiveReached = true;
            }
            else
            {
                animator.SetBool("Attack", false);
                if (objectiveReached)
                {
                    Invoke("tornarAMoure", 0.5f);
                    objectiveReached = false;
                }
            }
        }



    }
    void tornarAMoure()
    {
        agent.speed = velocitatMoviment;
    }

    public void restarVida()
    {
        vidaCaballer -= 1;
        if (vidaCaballer == 0)
        {
            Destroy(gameObject);
        }
    }

    public int getVida()
    {
        return vidaCaballer;
    }
}
