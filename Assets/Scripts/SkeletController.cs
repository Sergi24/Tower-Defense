using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletController : MonoBehaviour {

	private UnityEngine.AI.NavMeshAgent agent;
	private Animator animator;
	public GameObject destination;
	private bool objectiveReached = false;

	// Use this for initialization
	void Start () {
		agent = this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
		animator = this.gameObject.GetComponent<Animator>();
		agent.destination = destination.transform.position;
	//	agent.Move();
	}
	
	void Update () {
		if (agent.pathStatus==UnityEngine.AI.NavMeshPathStatus.PathComplete&&agent.remainingDistance<3) {
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
		agent.speed=3.5f;
	}

}
