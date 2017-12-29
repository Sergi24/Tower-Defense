using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichController : EnemyGeneralControl, HealthInterface
{
    private bool lichMort = false;
    public int rangAtac;
    public GameObject instantiatorBall;
    public float rotationSpeed; //Velocidad de rotación 

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
    }

    void Update()
    {
        if (!lichMort)
        {
            findClosestTarget("Caballero", maximBusqueda);
            if (destination.Equals(GameObject.Find("Player"))) findClosestTarget("Defensa", maximBusqueda);
            agent.destination = destination.transform.position;

            if (agent.remainingDistance < rangAtac)
            {
                animator.SetBool("Attack", true);
                agent.speed = 0;

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(agent.destination - transform.position), Time.deltaTime * rotationSpeed);

                if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack02"))
                {
                    if (contador > velocitatAtac)
                    {
                        gameObject.GetComponentInChildren<BallInstantiator>().crearBola();
                        contador = 0;
                    }
                    else contador++;
                }

                if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack01"))
                {
                    contador = 0;
                }
            }
            else
            {
                animator.SetBool("Attack", false);

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

    public void restarVida()
    {
        animator.SetBool("Hit", true);
        agent.speed = 0;
        Invoke("tornarAMoure", 2);
        health -= 1;
        if (health == 0)
        {
            lichMort = true;
            animator.SetBool("Death", true);
            agent.speed = 0;
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            agent.enabled = false;
            GameObject.Find("Player").GetComponent<CastleHealth>().sumarDiners(3);
            Destroy(gameObject, 3);
        } 
    }

    public int getVida()
    {
        return health;
    }
}