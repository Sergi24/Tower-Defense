using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierController : TroopGeneralControl, HealthInterface
{
    private bool soldatMort = false;
    private bool objectiveReached = false;
    private bool isAttack01 = true;

    private int attack01 = Animator.StringToHash("Attack01");
    private int attack02 = Animator.StringToHash("Attack02");

    private AudioSource asource;

    public GameObject giantSoldier;
    public int areaDany;

    // Use this for initialization
    void Start()
    {
        agent = this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.speed = velocitatMoviment;
        animator = this.gameObject.GetComponent<Animator>();
        agent.destination = GameObject.Find("Player").transform.position;
        asource = gameObject.GetComponent<AudioSource>();
        assignarVidaARestar();
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

                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack01") && !isAttack01)
                {
                    Invoke("atacar", 0.1f);
                    animator.SetTrigger(attack02);
                    isAttack01 = true;
                }
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack02") && isAttack01)
                {
                    Invoke("atacar", 0.1f);
                    animator.SetTrigger(attack01);
                    isAttack01 = false;
                }
            }
            else
            {
                animator.SetBool("Attack", false);
                isAttack01 = false;
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

    void atacar()
    {
        if (gameObject == giantSoldier)
        {
            if (destination.tag == "Caballero")
            {
                GameObject[] objectius = GameObject.FindGameObjectsWithTag("Caballero");
                for (int i = 0; i < objectius.Length; i++)
                {
                    if ((objectius[i].transform.position - destination.transform.position).magnitude < areaDany)
                    {
                        objectius[i].GetComponent<HealthInterface>().restarVida(damage);
                    }
                }
            }
            else
            {
                GameObject.Find("Player").GetComponent<HealthInterface>().restarVida(damage);
            }
        }
        else
        {
            destination.GetComponent<HealthInterface>().restarVida(damage);
        }

        asource.Play();
    }

    void parar()
    {
        agent.speed = 0;
        animator.SetBool("Run", false);
    }

    public void restarVida(int vidaARestar)
    {
        health -= vidaARestar;
        restarVidaBarra(vidaARestar);

        if (health <= 0&&!soldatMort)
        {
            barraVida.GetComponent<MeshRenderer>().enabled = false;
            soldatMort = true;
            animator.SetBool("Death", true);
            agent.speed = 0;
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            agent.enabled = false;
            GameObject.Find("Player").GetComponent<CastleHealth>().sumarDiners(dinersASumar);
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