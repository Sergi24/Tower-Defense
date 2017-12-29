using UnityEngine;
using System.Collections;

public class dragonController : EnemyGeneralControl, HealthInterface 
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


    void Awake () 
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
        dragonDestination = GameObject.Find("Player");
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!dracMort)
        {
            agent.SetDestination(dragonDestination.transform.position);

            AudioSource asource = gameObject.GetComponent<AudioSource>();
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Fly Glide"))
            {
                if (asource.clip.name == "Roar")
                {
                    asource.enabled = false;
                }
            }

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Fly Glide"))
            {
                agent.speed = velocitatMoviment + 1;
            }else agent.speed = velocitatMoviment;
            //   transform.Translate(transform.up*5);
            //     FlyForward();
        }//MORT
        else if (transform.position.y < 1f)
        {
            rb.useGravity = false;
            rb.isKinematic = true;
        }
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

    public void restarVida()
    {
        health -= 1;
        if (health == 0)
        {
            dracMort = true;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            agent.enabled = false;
            rb.useGravity = true;
            Die();
            GameObject.Find("Player").GetComponent<CastleHealth>().sumarDiners(15);
            Destroy(gameObject, 7f);
        }
    }

    public int getVida()
    {
        return health;
    }
}
