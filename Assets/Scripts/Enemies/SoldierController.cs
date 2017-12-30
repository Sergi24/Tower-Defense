using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierController : TroopGeneralControl, HealthInterface
{
    private bool soldatMort = false;
    private bool objectiveReached = false;

    // Use this for initialization
    void Start()
    {
        agent = this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.speed = velocitatMoviment;
        animator = this.gameObject.GetComponent<Animator>();
        agent.destination = GameObject.Find("Player").transform.position;
        //agent.Move(new Vector3(0f, 0f, 0f));
    }

    void Update()
    {
        if (!soldatMort)
        {
            if (!findClosestTarget("Caballero", maximBusqueda)) destination = GameObject.Find("Player");
            agent.destination = destination.transform.position;

            if (agent.remainingDistance < rangAtac)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(agent.destination - transform.position), Time.deltaTime * rotationSpeed);
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
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("GetHit"))
                {
                    parar();
                    animator.SetBool("Hit", false);
                }
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("GetHit") && !animator.GetBool("Hit"))
                {
                    tornarAMoure();
                }
            }
        }
    }

    void tornarAMoure()
    {
        agent.speed = velocitatMoviment;
        animator.SetBool("Run", true);
    }

    void parar()
    {
        agent.speed = 0;
        animator.SetBool("Run", false);
    }

    public void restarVida()
    {
        health -= 1;
        if (health == 0)
        {
            soldatMort = true;
            animator.SetBool("Death", true);
            agent.speed = 0;
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            agent.enabled = false;
            GameObject.Find("Player").GetComponent<CastleHealth>().sumarDiners(2);
            notifyDeath();
            Destroy(gameObject, 3);
        }
        else
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("GetHit"))
            {
                animator.SetBool("Hit", true);
                parar();
            }
        }
    }

    public int getVida()
    {
        return health;
    }
}