using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaballeroController : TroopGeneralControl, HealthInterface {

    private AudioSource asource;
    private bool caballerMort=false;
    private bool isAttack01 = false;
    private bool canviAtac;
    public int damage;

    private int attack01 = Animator.StringToHash("Attack01");
    private int attack02 = Animator.StringToHash("Attack02");

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
            if (findClosestTargetWithoutRange("Enemy"))
            {
                agent.destination = destination.transform.position;
                animator.SetBool("Walk", true);
            }
            else
            {
                animator.SetBool("Walk", false);
                destination = null;
            }

            if (destination != null)
            {
                //Si s'ataca ja
                if (agent.remainingDistance < rangAtac)
                {
                    animator.SetBool("Attack", true);
                    agent.speed = 0;

                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(destination.transform.position - transform.position), Time.deltaTime * rotationSpeed);

                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack01") && !isAttack01)
                    {
                        Invoke("atacar", 0.5f);
                        animator.SetTrigger(attack02);
                        isAttack01 = true;
                    }
                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack02") && isAttack01)
                    {
                        Invoke("atacar", 0.3f);
                        animator.SetTrigger(attack01);
                        isAttack01 = false;
                    }
                }
                else //si no s'ataca
                {
                    animator.SetBool("Attack", false);
                    tornarAMoure();
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

    void tornarAMoure()
    {
        agent.speed = velocitatMoviment;
    }

    void atacar()
    {
        destination.GetComponent<HealthInterface>().restarVida(damage);
        asource.Play();
    }

    public void restarVida(int vidaARestar)
    {
        health -= vidaARestar;
        if (health <= 0)
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
