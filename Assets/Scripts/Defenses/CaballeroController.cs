using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaballeroController : TroopGeneralControl, HealthInterface {

    private AudioSource asource;
    private bool caballerMort=false;
    private bool isAttack01 = false;
    private bool canviAtac;

    private int attack01 = Animator.StringToHash("Attack01");
    private int attack02 = Animator.StringToHash("Attack02");

    public int areaDany;
    public GameObject giantSoldier;

    // Use this for initialization
    void Start()
    {
        agent = this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        asource = gameObject.GetComponent<AudioSource>();
        animator = this.gameObject.GetComponent<Animator>();
        animator.SetBool("Attack", false);
        animator.SetBool("Death", false);
        animator.SetBool("Walk", false);
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
                animator.SetBool("Attack", false);
                destination = null;
                agent.speed = 0;
            }

            if (destination != null)
            {
                //Si s'ataca ja
                if ((destination.transform.position-transform.position).magnitude < rangAtac) estatAtacar();
                else if (destination==giantSoldier && (destination.transform.position - transform.position).magnitude < rangAtac+3) estatAtacar(); 
                else //si no s'ataca
                {
                    animator.SetBool("Attack", false);
                    isAttack01 = false;
                    animator.SetTrigger(attack01);
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

    void estatAtacar()
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

    void atacar()
    {
        GameObject[] objectius = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < objectius.Length; i++)
        {
            if ((objectius[i].transform.position - destination.transform.position).magnitude < areaDany)
            {
                objectius[i].GetComponent<HealthInterface>().restarVida(damage);
            }
        }
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
