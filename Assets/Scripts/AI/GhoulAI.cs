using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulAI : MonoBehaviour {

    public float movementSpeed;
    public float movementRadius;
    public float attackRadius;

    public Transform groundCheck;
    public LayerMask groundLayer;

    private enum GhoulState {Sleeping, Pacing, Tracking};

    private float paceStartTime;
    private float paceExecTime;
    private GhoulState ghoulState;
    private Transform player;
    private Vector2 vel;
    private Vector3 startPos;

    private bool facingRight;
    private Vector3 theScale;

    private Animator anim;

    private bool playerInAttackRange;
    private bool playerInRadius;

    // Use this for initialization
    void Start () {
        ghoulState = GhoulState.Sleeping;
        player = GameObject.FindGameObjectWithTag("Player").transform;

        startPos = transform.position;
        facingRight = false;
        anim = GetComponent<Animator>();

        //calculate the velocity based on normal vector of ground
        RaycastHit2D hit = Physics2D.Raycast(transform.position, groundCheck.position, 5f, groundLayer.value, 0);
        vel = Vector3.Cross(hit.normal, new Vector3(0, 0, 1)).normalized;
        vel *= movementSpeed;

        //used for changing states
        playerInAttackRange = false;
        playerInRadius = false;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            playerInRadius = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            playerInRadius = false;
    }

    // Update is called once per frame
    void Update () {

        //if player gets close enough to the ghoul to attack, attempt attack and start tracking player
        playerInAttackRange = Vector3.Distance(transform.position, player.position) <= attackRadius;

        anim.ResetTrigger("Attack");

        if (playerInRadius)
        {
            paceStartTime = Time.time;
            paceExecTime = Random.Range(2f, 4f);
            ghoulState = GhoulState.Pacing;
        }
        else if (Time.time - paceStartTime >= paceExecTime)
        {
            ghoulState = GhoulState.Sleeping;
            anim.SetBool("Awake", false);
        }

        if (playerInAttackRange)
        {
            ghoulState = GhoulState.Tracking;
            anim.SetTrigger("Attack");
        }

        if (playerInRadius && !playerInAttackRange)
            ghoulState = GhoulState.Pacing;

        if (ghoulState == GhoulState.Pacing)
        {
            anim.SetBool("Awake", true);
            HandlePatrolling();
        }
        else if (ghoulState == GhoulState.Tracking)
        {
            HandleTracking();
        }
        else if (ghoulState == GhoulState.Sleeping)
        {
            anim.SetBool("Awake", false);
        }
	}

    void HandlePatrolling()
    {
        if (Vector2.Distance(transform.position, startPos) >= movementRadius)
        {
            Debug.Log("VELCHANGE");
            vel = (transform.position - startPos).normalized;
            vel *= movementSpeed;
        }
        transform.Translate(vel * Time.deltaTime);
        CalcFlip(vel);
    }

    void HandleTracking()
    {
    }

    /*
     * Determine if this object needs flipped based on traveling direction
     */
    void CalcFlip(Vector3 dir)
    {
        if (Vector3.Dot(dir, Vector3.right) < 0 && facingRight)
            Flip();
        if (Vector3.Dot(dir, Vector3.right) > 0 && !facingRight)
            Flip();
    }

    void Flip()
	{
		facingRight = !facingRight;
		theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

    public Vector3 TheScale
    {
        get
        {
            return theScale;
        }

        set
        {
            theScale = value;
        }
    }

}
