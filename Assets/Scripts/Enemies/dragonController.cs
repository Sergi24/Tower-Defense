using UnityEngine;
using System.Collections;

public class dragonController : TroopGeneralControl, HealthInterface
{
    private GameObject dragonDestination;

    int scream;
    int basicAttack;
    int clawAttack;
    int flameAttack;
    int defend;
    int getHit;
    int sleep;
    int walk;
    int run;
    int takeOff;
    int flyFlameAttack;
    int flyForward;
    int flyGlide;
    int land;
    int die;
    int idle02;

    private bool dracMort = false;
    private Rigidbody rb;
    private bool haTiratFoc = true;
    private bool isFlyForward = true;
    public int damage;



    void Awake()
    {
        agent = this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponent<Animator>();
        scream = Animator.StringToHash("Scream");
        basicAttack = Animator.StringToHash("Basic Attack");
        clawAttack = Animator.StringToHash("Claw Attack");
        flameAttack = Animator.StringToHash("Flame Attack");
        defend = Animator.StringToHash("Defend");
        getHit = Animator.StringToHash("Get Hit");
        sleep = Animator.StringToHash("Sleep");
        walk = Animator.StringToHash("Walk");
        run = Animator.StringToHash("Run");
        takeOff = Animator.StringToHash("Take Off");
        flyFlameAttack = Animator.StringToHash("Fly Flame Attack");
        flyForward = Animator.StringToHash("Fly Forward");
        flyGlide = Animator.StringToHash("Fly Glide");
        land = Animator.StringToHash("Land");
        die = Animator.StringToHash("Die");
        idle02 = Animator.StringToHash("Idle02");
    }

    private void Start() {
        dragonDestination = GameObject.Find("ObjectiuDrac");
        rb = gameObject.GetComponent<Rigidbody>();
        agent.speed = velocitatMoviment;
    }

    private void Update()
    {
        if (!dracMort)
        {
            agent.SetDestination(dragonDestination.transform.position);

            AudioSource asource = gameObject.GetComponent<AudioSource>();
            /*   if (animator.GetCurrentAnimatorStateInfo(0).IsName("Fly Glide"))
               {
                   if (asource.clip.name == "Roar")
                   {
                       asource.enabled = false;
                   }
               }*/

            if (agent.remainingDistance < rangAtac)
            {
                agent.speed = 0;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(agent.destination - transform.position), Time.deltaTime * rotationSpeed);

                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Fly Forward") && haTiratFoc)
                {
                    FlyGlide();
                    haTiratFoc = false;
                }
                else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Fly Glide") && !haTiratFoc)
                {
                    FlyForward();
                    haTiratFoc = true;
                    gameObject.GetComponentInChildren<FireInstantiator>().instantiateFire();
                    Invoke("makeDamage", 2);
                }

            } else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Fly Forward") && isFlyForward)
            {
                FlyGlide();
                agent.speed = velocitatMoviment;
                isFlyForward = false;
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Fly Glide") && !isFlyForward)
            {
                FlyForward();
                agent.speed = velocitatMoviment + 1;
                isFlyForward = true;
            }

        }//MORT
        else if (transform.position.y < 1f)
        {
            rb.useGravity = false;
            rb.isKinematic = true;
        }
    }

    public void makeDamage()
    {
        destination.GetComponent<HealthInterface>().restarVida(damage);
    }

    public void Scream ()
	{
        animator.SetTrigger(scream);
	}

	public void BasicAttack ()
	{
        animator.SetTrigger(basicAttack);
	}

	public void ClawAttack ()
	{
        animator.SetTrigger(clawAttack);
	}

	public void FlameAttack ()
	{
        animator.SetTrigger(flameAttack);
	}

	public void Defend ()
	{
        animator.SetTrigger(defend);
	}

	public void GetHit ()
	{
        animator.SetTrigger(getHit);
	}

	public void Sleep ()
	{
        animator.SetTrigger(sleep);
	}

	public void Walk ()
	{
        animator.SetTrigger(walk);
	}

	public void Run ()
	{
        animator.SetTrigger(run);
	}

	public void TakeOff ()
	{
        animator.SetTrigger(takeOff);
	}

	public void FlyFlameAttack ()
	{
        animator.SetTrigger(flyFlameAttack);
	}

	public void FlyForward ()
	{
        animator.SetTrigger(flyForward);
	}

	public void FlyGlide ()
	{
        animator.SetTrigger(flyGlide);
	}

	public void Land ()
	{
        animator.SetTrigger(land);
	}

	public void Die ()
	{
        animator.SetTrigger(die);
	}

	public void Idle02 ()
	{
        animator.SetTrigger(idle02);
	}

    public void restarVida(int vidaARestar)
    {
        health -= vidaARestar;
        if (health < 0)
        {
            dracMort = true;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            agent.enabled = false;
            rb.useGravity = true;
            Die();
            GameObject.Find("Player").GetComponent<CastleHealth>().sumarDiners(15);
            notifyDeath();
            Destroy(gameObject, 7f);
        }
    }

    public int getVida()
    {
        return health;
    }
}
