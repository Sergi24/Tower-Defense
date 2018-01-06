using UnityEngine;
using System.Collections;

public class dragonController : TroopGeneralControl, HealthInterface
{
    private GameObject dragonDestination;

    int flyForward;
    int flyGlide;
    int die;

    private bool dracMort = false;
    private Rigidbody rb;
    private bool haTiratFoc = true;
    private bool isFlyForward = true;
    private bool atacantTorre = false;
    private AudioSource fire;

    private bool estatAtacant = false;

    void Awake()
    {
        agent = this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponent<Animator>();
        flyForward = Animator.StringToHash("Fly Forward");
        flyGlide = Animator.StringToHash("Fly Glide");
        die = Animator.StringToHash("Die");
    }

    private void Start() {
        destination = GameObject.Find("ObjectiuDrac");
        agent.SetDestination(destination.transform.position);
    //    rb = gameObject.GetComponent<Rigidbody>();
    //    rb.mass = 10000f;
        agent.speed = velocitatMoviment;
        assignarVidaARestar();

        fire = gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!dracMort)
        {
            if (!findClosestTarget("Defensa", rangAtac) || atacantTorre) destination = GameObject.Find("ObjectiuDrac");

            if ((destination.transform.position - transform.position).magnitude < rangAtac || atacantTorre)
            {
                agent.speed = 0;
                if (destination.tag == "ObjectiuDrac") atacantTorre = true;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(destination.transform.position - transform.position), Time.deltaTime * rotationSpeed);

                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Fly Forward") && (haTiratFoc || !estatAtacant))
                {
                    FlyGlide();
                    haTiratFoc = false;
                    fire.Stop();
                }
                else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Fly Glide") && (!haTiratFoc || !estatAtacant))
                {
                    FlyForward();
                    haTiratFoc = true;
                    gameObject.GetComponentInChildren<FireInstantiator>().instantiateFire();
                    fire.Play();
                    Invoke("makeDamage", 1.5f);
                }
                estatAtacant = true;

            }
            else
            {
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Fly Forward") && (isFlyForward || estatAtacant))
                {
                    FlyGlide();
                    agent.speed = velocitatMoviment;
                    isFlyForward = false;
                    fire.Stop();
                }
                else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Fly Glide") && (!isFlyForward || estatAtacant))
                {
                    FlyForward();
                    agent.speed = velocitatMoviment + 2;
                    isFlyForward = true;
                }
                estatAtacant = false;
            }

        }//MORT
        else if (transform.position.y > 1f)
        {
            //    rb.useGravity = false;
            //    rb.isKinematic = true;
            transform.Translate(0f, -0.1f, 0f);
        }
    }

    public void makeDamage()
    {
        if (destination.tag=="ObjectiuDrac") GameObject.Find("Player").GetComponent<HealthInterface>().restarVida(damage);
        else destination.GetComponent<HealthInterface>().restarVida(damage);
    }

	public void FlyForward ()
	{
        animator.SetTrigger(flyForward);
	}

	public void FlyGlide ()
	{
        animator.SetTrigger(flyGlide);
	}

	public void Die ()
	{
        animator.SetTrigger(die);
	}

    public void restarVida(int vidaARestar)
    {
        health -= vidaARestar;
        restarVidaBarra(vidaARestar);
        if (health <= 0&&!dracMort)
        {
            dracMort = true;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            agent.enabled = false;
      //      rb.drag = 1f;
      //      rb.useGravity = true;
            Die();
            GameObject.Find("Player").GetComponent<CastleHealth>().sumarDiners(dinersASumar);
            notifyDeath();
            Destroy(gameObject, 7f);
        }
    }

    public int getVida()
    {
        return health;
    }
}
