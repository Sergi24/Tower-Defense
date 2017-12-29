using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierController : MonoBehaviour
{

    private UnityEngine.AI.NavMeshAgent agent;
    private Animator animator;
    private GameObject destination;
    public int vidaSoldat;
    private bool soldatMort = false;
    private bool objectiveReached = false;
    public float velocitatMoviment;

    // Use this for initialization
    void Start()
    {
        agent = this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.speed = velocitatMoviment;
        animator = this.gameObject.GetComponent<Animator>();
        destination = GameObject.Find("Player");
        agent.destination = destination.transform.position;
        agent.Move(new Vector3(0f, 0f, 0f));
    }

    void Update()
    {
        if (!soldatMort)
        {
            if (agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathComplete && agent.remainingDistance < 4)
            {
                animator.SetBool("Attack", true);
                agent.speed = 0;
                objectiveReached = true;
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
            destination = GameObject.Find("Player");
            agent.destination = destination.transform.position;
        }

    }
    void tornarAMoure()
    {
        agent.speed = velocitatMoviment;
        animator.SetBool("Run", true);
        animator.SetBool("Hit", false);
    }

    public void restarVida()
    {
        vidaSoldat -= 1;
        if (vidaSoldat == 0)
        {
            animator.SetBool("Death", true);
            agent.speed = 0;
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            agent.enabled = false;
            Destroy(gameObject, 3);
        }
        else
        {
            if (!animator.GetBool("Hit"))
            {
                animator.SetBool("Hit", true);
                animator.SetBool("Run", false);
                agent.speed = 0;
                Invoke("tornarAMoure", 1f);
            }
        }
    }

    public int getVida()
    {
        return vidaSoldat;
    }
}