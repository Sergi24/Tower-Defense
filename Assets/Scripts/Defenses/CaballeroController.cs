using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaballeroController : TroopGeneralControl, HealthInterface {

    private AudioSource asource;
    private bool caballerMort=false;
    private bool attack01 = false;
    private bool canviAtac;
    public int damage;

    // Use this for initialization
    void Start()
    {
        agent = this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        asource = gameObject.GetComponent<AudioSource>();
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
            if (!findClosestTarget("Enemy", maximBusqueda)) destination = GameObject.Find("Player");
            agent.destination = destination.transform.position;
            animator.SetBool("Walk", true);

            //Si s'ataca ja
            if (agent.remainingDistance < rangAtac)
            {
                animator.SetBool("Attack", true);
                agent.speed = 0;

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(destination.transform.position - transform.position), Time.deltaTime * rotationSpeed);

                if (canviAtac)
                {
                    if (attack01)
                    {
                        if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack01"))
                        {
                            contador = 0;
                        }
                        else
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
                        destination.GetComponent<HealthInterface>().restarVida(damage);
                        asource.Play();
                        attack01 = true;
                        canviAtac = true;
                    }
                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack02") && attack01)
                    {
                        destination.GetComponent<HealthInterface>().restarVida(damage);
                        asource.Play();
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
                Invoke("tornarAMoure", 0f);
            }
        }
        else
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Attack", false);
            agent.speed = 0;
        }
    }

    void tornarAMoure()
    {
        agent.speed = velocitatMoviment;
    }

    public void restarVida(int vidaARestar)
    {
        health -= vidaARestar;
        if (health < 0)
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
        return health;
    }

}
