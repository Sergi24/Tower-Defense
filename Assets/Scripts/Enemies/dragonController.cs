using UnityEngine;
using System.Collections;

public class dragonController : MonoBehaviour, HealthInterface 
{
	public Animator anim;
    private UnityEngine.AI.NavMeshAgent agent;
    private GameObject destination;
    public int vidaDragon;
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
    public float velocitatMoviment;


    void Awake () 
	{
        agent = this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();
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
        destination = GameObject.Find("Player");
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!dracMort)
        {
            agent.SetDestination(destination.transform.position);
            
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Fly Glide"))
            {
                agent.speed = velocitatMoviment + 1;
            }else agent.speed = velocitatMoviment;
            //   transform.Translate(transform.up*5);
            //     FlyForward();
        }//MORT
        else if (transform.position.y < 0.9f)
        {
            rb.useGravity = false;
            rb.isKinematic = true;
        }
    }

    public void Scream ()
	{
		anim.SetTrigger(scream);
	}

	public void BasicAttack ()
	{
		anim.SetTrigger(basicAttack);
	}

	public void ClawAttack ()
	{
		anim.SetTrigger(clawAttack);
	}

	public void FlameAttack ()
	{
		anim.SetTrigger(flameAttack);
	}

	public void Defend ()
	{
		anim.SetTrigger(defend);
	}

	public void GetHit ()
	{
		anim.SetTrigger(getHit);
	}

	public void Sleep ()
	{
		anim.SetTrigger(sleep);
	}

	public void Walk ()
	{
		anim.SetTrigger(walk);
	}

	public void Run ()
	{
		anim.SetTrigger(run);
	}

	public void TakeOff ()
	{
		anim.SetTrigger(takeOff);
	}

	public void FlyFlameAttack ()
	{
		anim.SetTrigger(flyFlameAttack);
	}

	public void FlyForward ()
	{
		anim.SetTrigger(flyForward);
	}

	public void FlyGlide ()
	{
		anim.SetTrigger(flyGlide);
	}

	public void Land ()
	{
		anim.SetTrigger(land);
	}

	public void Die ()
	{
		anim.SetTrigger(die);
	}

	public void Idle02 ()
	{
		anim.SetTrigger(idle02);
	}

    public void restarVida()
    {
        vidaDragon -= 1;
        if (vidaDragon == 0)
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
        return vidaDragon;
    }
}
