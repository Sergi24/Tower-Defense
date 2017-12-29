using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletController : MonoBehaviour, HealthInterface {

	private UnityEngine.AI.NavMeshAgent agent;
	private Animator animator;
	public GameObject destination;
    public int vidaEsquelet;
	private bool objectiveReached = false;
    public float velocitatMoviment;

    // Use this for initialization
    void Start () {
		agent = this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
		animator = this.gameObject.GetComponent<Animator>();
        destination = GameObject.Find("Player");
		agent.destination = destination.transform.position;
	//	agent.Move();
	}
	
	void Update () {
		if (agent.remainingDistance<3) {
			animator.SetBool("Attack", true);
			agent.speed=0;
			objectiveReached=true;
		}
		else {
			animator.SetBool("Attack", false);
			if(objectiveReached) {
				Invoke("tornarAMoure", 1);
				objectiveReached=false;
			}
		}
		agent.destination = destination.transform.position;
	
	}
	void tornarAMoure(){
		agent.speed=velocitatMoviment;
	}

    public void restarVida()
    {
        vidaEsquelet -= 1;
        if (vidaEsquelet == 0)
        {
            GameObject.Find("Player").GetComponent<CastleHealth>().sumarDiners(1);
            Destroy(gameObject);
        }
    }

    public int getVida()
    {
        return vidaEsquelet;
    }
}
