using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichController : MonoBehaviour, HealthInterface
{

    private UnityEngine.AI.NavMeshAgent agent;
    private Animator animator;
    public int vidaLich;
    private bool lichMort = false;
    public float velocitatMoviment;
    public float maximBusqueda;
    private float minim;
    public int rangAtac;
    public int velocitatAtac;
    public GameObject instantiatorBall;
    private GameObject[] objectius;
    public float rotationSpeed; //Velocidad de rotación 
    private int contador = 0;
    private bool destinationNova;
    private Vector3 destination;
    private bool objectiveReached = false;

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
            destinationNova = false;
            objectius = GameObject.FindGameObjectsWithTag("Defensa");
            if (objectius.Length != 0)
            {
                //buscar objectiu mes proper
                minim = maximBusqueda;
                for (int i = 0; i < objectius.Length; i++)
                {
                    if ((objectius[i].transform.position - transform.position).magnitude < minim)
                    {
                        if (objectius[i].GetComponent<HealthInterface>().getVida() > 0)
                        {
                            destinationNova = true;
                            minim = (transform.position - objectius[i].transform.position).magnitude;
                            destination = objectius[i].transform.position;
                        }
                    }
                }
                if (destinationNova) agent.SetDestination(destination);
                else agent.SetDestination(GameObject.Find("Player").transform.position);

                if (agent.remainingDistance < rangAtac)
                {
                    animator.SetBool("Attack", true);
                    agent.speed = 0;
                    objectiveReached = true;

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
            else
            {
                animator.SetBool("Attack", false);
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
        vidaLich -= 1;
        if (vidaLich == 0)
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
        return vidaLich;
    }
}