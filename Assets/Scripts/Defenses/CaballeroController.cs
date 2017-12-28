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
    public int velocitatAtac;
    public float rangAtac;
    public int vidaCaballer;
    public int rotationSpeed;
    private bool caballerMort=false;
    private int contador = 0;
    private bool attack01 = false;
    private bool canviAtac;

    // Use this for initialization
    void Start()
    {
        agent = this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = this.gameObject.GetComponent<Animator>();
        animator.SetBool("Attack", false);
        animator.SetBool("Death", false);
        animator.SetBool("Walk", false);
        //agent.Move();
    }

    void Update()
    {
        if (!caballerMort)
        {
            objectius = GameObject.FindGameObjectsWithTag("Enemy");

            if (objectius.Length != 0)
            {
                animator.SetBool("Walk", true);
                //Buscar objectiu mes proper
                float minim = 100f;
                destination = objectius[0];
                for (int i = 0; i < objectius.Length; i++)
                {
                    if ((objectius[i].transform.position - transform.position).magnitude < minim)
                    {
                        if (objectius[i].GetComponent<HealthInterface>().getVida() > 0)
                        {
                            minim = (objectius[i].transform.position - transform.position).magnitude;
                            destination = objectius[i];
                        }
                    }
                }
                agent.destination = destination.transform.position;

                //Si s'ataca ja
                if (agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathComplete && agent.remainingDistance < rangAtac)
                {
                    if (!objectiveReached)
                    {
                        animator.SetBool("Attack", true);
                        agent.speed = 0;
                        objectiveReached = true;
                    }

                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(destination.transform.position - transform.position), Time.deltaTime * rotationSpeed);

                    if (canviAtac)
                    {
                        if (attack01)
                        {
                            if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack01"))
                            {
                                contador = 0;
                            } else
                            {
                                canviAtac = false;
                            }
                        }
                        else
                        {
                            if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack02"))
                            {
                                contador = 0;
                            }
                            else
                            {
                                canviAtac = false;
                            }
                        }
                    }
                    if (contador > velocitatAtac)
                    {
                        if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack01") && !attack01)
                        {
                            destination.GetComponent<HealthInterface>().restarVida();
                            attack01 = true;
                            canviAtac = true;
                        }
                        if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack02") && attack01) {
                            destination.GetComponent<HealthInterface>().restarVida();
                            attack01 = false;
                            canviAtac = true;
                        }
                        contador = 0;
                    }
                    else contador++;
                }
                else //si no s'ataca
                {
                    animator.SetBool("Attack", false);
                    if (objectiveReached)
                    {
                        Invoke("tornarAMoure", 0.5f);
                        objectiveReached = false;
                    }
                }
            }
            else
            {
                animator.SetBool("Walk", false);
                animator.SetBool("Attack", false);
                agent.speed = 0;
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
            caballerMort = true;
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            agent.enabled = false;
            animator.SetBool("Death", true);
            Destroy(gameObject, 6f);
        }
    }

    public int getVida()
    {
        return vidaCaballer;
    }

}
