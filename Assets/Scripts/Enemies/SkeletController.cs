using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletController : EnemyGeneralControl, HealthInterface {

    public int rotationSpeed;
    public int attackRange;
    public GameObject explosion;
    private bool skeletDie = false;

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
            findClosestTarget("Caballero", maximBusqueda);
            agent.destination = destination.transform.position;

            if (agent.remainingDistance < attackRange)
            {
                agent.speed = 0;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(agent.destination - transform.position), Time.deltaTime * rotationSpeed);
                animator.SetBool("Attack", true);

                if (contador > velocitatAtac)
                {
                    destination.GetComponent<HealthInterface>().restarVida();
                    contador = 0;
                } else contador++;
            }
            else
            {
                animator.SetBool("Attack", false);
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
                {
                    moure();
                }
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

    public void restarVida()
    {
        health -= 1;
        if (health == 0)
        {
            skeletDie = true;
            Instantiate(explosion, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), explosion.transform.rotation);
            GameObject.Find("Player").GetComponent<CastleHealth>().sumarDiners(1);
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
