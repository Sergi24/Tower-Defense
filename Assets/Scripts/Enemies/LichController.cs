using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichController : MonoBehaviour, HealthInterface
{

    private UnityEngine.AI.NavMeshAgent agent;
    private Animator animator;
    private GameObject destination;
    public int vidaLich;
    private bool lichMort = false;
    private bool objectiveReached = false;
    public float velocitatMoviment;
    public int rangAtac;
    public int velocitatAtac;
    public GameObject instantiatorBall;
    private GameObject[] objectius;
    public float rotationSpeed; //Velocidad de rotación 
    private int contador = 0;

    // Use this for initialization
    void Start()
    {
        agent = this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.speed = velocitatMoviment;
        animator = this.gameObject.GetComponent<Animator>();
        agent.destination = GameObject.Find("Player").transform.position;
        agent.Move(new Vector3(0f, 0f, 0f));
    }
   
    void Update()
    {
        if (!lichMort)
        {
            objectius = GameObject.FindGameObjectsWithTag("Defensa");

            if (objectius.Length != 0)
            {
                //buscar objectiu mes proper
                float minim = 100f;
                for (int i = 0; i < objectius.Length; i++)
                {
                    if ((transform.position - objectius[i].transform.position).magnitude < minim)
                    {
                        minim = (transform.position - objectius[i].transform.position).magnitude;
                        destination = objectius[i];
                    }
                }

                if (agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathComplete && agent.remainingDistance < rangAtac)
                {
                    if (!objectiveReached) {
                        animator.SetBool("Attack", true);
                        agent.speed = 0;
                        objectiveReached = true;
                    }
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(destination.transform.position - transform.position), Time.deltaTime * rotationSpeed);
                    if (contador > velocitatAtac)
                    {
                        gameObject.GetComponentInChildren<BallInstantiator>().crearBola();
                        contador = 0;
                    }
                    else contador++;
                   
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
                agent.destination = destination.transform.position;
            }
        }
    }
    void tornarAMoure()
    {
        agent.speed = velocitatMoviment;
    }

    public void restarVida()
    {
        vidaLich -= 1;
        if (vidaLich == 0)
        {
            lichMort = true;
            animator.SetBool("Death", true);
            agent.speed = 0;
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            agent.enabled = false;
            Destroy(gameObject, 3);
        }
    }

    public int getVida()
    {
        return vidaLich;
    }
}