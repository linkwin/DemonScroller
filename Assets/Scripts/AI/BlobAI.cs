using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobAI : MonoBehaviour {

    public float timeStepInSec;
    public float movementSpeed;
    public float patrollTime;
    public float trackTime;
    public float attackRadius;
    public CatmullRomSpline spline;
    public float trackDistance;

    public float radius;//used for fix in transition from tracking to patrolling

    private enum BlobState { Patrolling, Tracking};
    private BlobState blobState;
    private Transform player;
    private Vector2 vel;
    private float currentPatrolTime;
    private float currentTrackTime;
    private List<Vector3> path;

    private int indexInPath;
    private Vector3 lastPos;
    private Vector3 nextPos;
    private float startTime;
    private float journeyLength;

    private bool facingRight;
    private Vector3 theScale;

    private Animator anim;

	// Use this for initialization
	void Start () {
        blobState = BlobState.Patrolling;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        vel = Vector2.right;
        currentPatrolTime = 0f;
        currentTrackTime = 0f;
        path = new List<Vector3>();
        spline = GetComponent<CatmullRomSpline>();
        for (int i = 0; i < spline.controlPointsList.Length; i++)
        {
            //Cant draw between the endpoints
            //Neither do we need to draw from the second to the last endpoint
            //...if we are not making a looping line
            if ((i == 0 || i == spline.controlPointsList.Length - 2 || i == spline.controlPointsList.Length - 1) && !spline.isLooping)
            {
                continue;
            }

            Vector3[] newPath = spline.DisplayCatmullRomSpline(i);
            foreach (Vector3 point in newPath)
                path.Add(point);
        }
        indexInPath = 0;
        lastPos = path[indexInPath];
        nextPos = path[indexInPath + 1];
        indexInPath++;
        startTime = Time.time;
        journeyLength = Vector3.Distance(lastPos, nextPos);
        facingRight = false;
        anim = GetComponent<Animator>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            player = collision.transform;
    }

    // Update is called once per frame
    void Update () {

        anim.ResetTrigger("Attack");
        if (currentPatrolTime >= patrollTime)
        {
            blobState = BlobState.Tracking;
            anim.SetBool("Chasing", true);
            currentTrackTime = 0f;
            currentPatrolTime = 0f;
        }
        if (currentTrackTime >= trackTime)
        {
            blobState = BlobState.Patrolling;
            anim.SetBool("Chasing", false);
            currentPatrolTime = 0f;
            currentTrackTime = 0f;
        }

        if (blobState == BlobState.Patrolling)
        {
            HandlePatrolling();
            currentPatrolTime += timeStepInSec;
        }
        else if (blobState == BlobState.Tracking)
        {
            HandleTracking();
            currentTrackTime += timeStepInSec;
        }
        if (Vector3.Distance(transform.position, player.position) <= attackRadius)
        {
            anim.SetTrigger("Attack");
        }
	}

    void HandlePatrolling()
    {
        Vector3 vec = lastPos;
        Vector3 flipDir = nextPos - lastPos;
        if (transform.position.x >= vec.x - radius &&
        transform.position.x <= vec.x + radius &&
        transform.position.y >= vec.y - radius &&
        transform.position.y <= vec.y + radius)
        {
            float distCovered = (Time.time - startTime) * movementSpeed;
            float fracJourney = distCovered / journeyLength;

            CalcFlip(flipDir);

            this.transform.position = Vector3.Lerp(lastPos, nextPos, fracJourney);

            if (fracJourney >= 1)
            {
                if (indexInPath == path.ToArray().Length - 1)
                {
                    lastPos = nextPos;
                    indexInPath = 0;
                    nextPos = path[indexInPath];
                }
                else
                {
                    lastPos = nextPos;
                    nextPos = path[indexInPath + 1];
                }
                indexInPath++;
                startTime = Time.time;
                journeyLength = Vector3.Distance(lastPos, nextPos);
            }
        }
        else
        {
            Vector3 dir = (lastPos - transform.position).normalized;
            CalcFlip(dir);
            dir *= movementSpeed * Time.deltaTime;
            transform.Translate(dir);
        }
    }

    void HandleTracking()
    {
        if (Vector3.Distance(transform.position, player.position) > trackDistance) {
            Vector3 dir = (player.position - transform.position).normalized;
            CalcFlip(dir);
            dir *= movementSpeed * Time.deltaTime;
            transform.Translate(dir);
        }
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
}
