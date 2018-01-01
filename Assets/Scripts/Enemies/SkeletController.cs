using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletController : TroopGeneralControl, HealthInterface {

    public GameObject explosion;
    private bool skeletDie = false;
    bool isAttacking = false;

    // Use this for initialization
    void Start () {
		agent = this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
		animator = this.gameObject.GetComponent<Animator>();
        destination = GameObject.Find("Player");
		agent.destination = destination.transform.position;
	}
	
	void Update () {

        if (!skeletDie)
        {
            if (!findClosestTarget("Caballero", maximBusqueda)) destination = GameObject.Find("Player");
            agent.destination = destination.transform.position;

            if (agent.remainingDistance < rangAtac)
            {
                agent.speed = 0;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(agent.destination - transform.position), Time.deltaTime * rotationSpeed);
                animator.SetBool("Attack", true);

                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")&&!isAttacking)
                {
                    isAttacking = true;
                    destination.GetComponent<HealthInterface>().restarVida(damage);
                } else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") && isAttacking)
                {
                    isAttacking = false;
                }
            }
            else
            {
                animator.SetBool("Attack", false);
                moure();
                isAttacking = false;
            }
        }
        else
        {
            transform.Translate(Vector3.down*0.03f);
        }
	}

	void moure(){
		agent.speed=velocitatMoviment;
	}

    public void restarVida(int vidaARestar)
    {
        health -= vidaARestar;
        if (health <= 0&&!skeletDie)
        {
            skeletDie = true;
            Instantiate(explosion, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), explosion.transform.rotation);
            GameObject.Find("Player").GetComponent<CastleHealth>().sumarDiners(dinersASumar);
            agent.enabled = false;
            animator.SetBool("Death", true);
            notifyDeath();
            Destroy(gameObject, 1f);
        }
    }

    public int getVida()
    {
        return health;
    }
}
