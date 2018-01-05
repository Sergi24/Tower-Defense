using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichController : TroopGeneralControl, HealthInterface
{
    private bool lichMort = false;
    public GameObject instantiatorBall;

    private AudioSource asource;

    // Use this for initialization
    void Start()
    {
        agent = this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.destination = GameObject.Find("Player").transform.position;
        agent.speed = velocitatMoviment;
        animator = this.gameObject.GetComponent<Animator>();
        animator.SetBool("Attack", false);
        animator.SetBool("Death", false);
        animator.SetBool("Hit", false);
        // agent.Move(new Vector3(0f, 0f, 0f));
        agent.destination = GameObject.Find("Player").transform.position;
        asource = GetComponent<AudioSource>();
        assignarVidaARestar();
    }

    void Update()
    {
        if (!lichMort)
        {
            if (!findClosestTarget("Player", rangAtac) || transform.position.z>-70)
                if (!findClosestTarget("Caballero", rangAtac))
                    if (!findClosestTarget("Defensa", rangAtac)) destination = GameObject.Find("Player"); 

            if ((destination.transform.position-transform.position).magnitude < rangAtac)
            {
                animator.SetBool("Attack", true);
                animator.SetBool("Hit", false);
                agent.speed = 0;
                Debug.Log("Contador: "+contador);

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(destination.transform.position - transform.position), Time.deltaTime * rotationSpeed);

                if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack02"))
                {
                    if (contador == -1)
                    {
                        Invoke("sonidoBallCreation", 0.5f);
                    }
                    if (contador > velocitatAtac)
                    {
                        instantiatorBall.GetComponent<BallInstantiator>().crearBola();
                        contador = 0;
                    } else contador++;
                }

                if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack01"))
                {
                    contador = -1;
                }
            }
            else
            {
                Debug.Log("NO ATACANT");
                animator.SetBool("Attack", false);
               // contador = -1;

                if (animator.GetCurrentAnimatorStateInfo(0).IsName("GetHit"))
                {
                    animator.SetBool("Hit", false);
                    agent.speed = 0;
                }
                else
                {
                    agent.speed = velocitatMoviment;
                }
            }
        }
    }

    void tornarAMoure()
    {
        agent.speed = velocitatMoviment;
    }

    void sonidoBallCreation()
    {
        asource.Play();
    }

    public void restarVida(int vidaARestar)
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("attack01") && !animator.GetCurrentAnimatorStateInfo(0).IsName("attack02"))
        {
            animator.SetBool("Hit", true);
        }
        health -= vidaARestar;
        if (health <= 0&&!lichMort)
        {
            barraVida.GetComponent<MeshRenderer>().enabled = false;
            lichMort = true;
            animator.SetBool("Death", true);
            agent.speed = 0;
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            agent.enabled = false;
            GameObject.Find("Player").GetComponent<CastleHealth>().sumarDiners(dinersASumar);
            notifyDeath();
            Destroy(gameObject, 3);
        } else
        {
            restarVidaBarra(vidaARestar);
        }
    }

    public int getVida()
    {
        return health;
    }
}