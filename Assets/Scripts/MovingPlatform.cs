using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    public float force = 2f;
    public float timeStepInSec = 0.01f;
    public float movementSpeed = 2f;
    public CatmullRomSpline spline;

    private Vector2 vel;
    private List<Vector3> path;

    private int indexInPath;
    private float currentPatrolTime;
    private Vector3 lastPos;
    private Vector3 nextPos;
    private float startTime;
    private float journeyLength;

    private bool facingRight;
    private Vector3 theScale;

    private Transform player;
    private Player playerScript;
    private Vector3 playerOffset;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject.transform;
            playerOffset = player.position - this.transform.position;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject.transform;
            playerOffset = player.position - this.transform.position;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && player.GetComponent<Player>().JustJumped)
        {
            Debug.Log("exit");
            player = null;
        }
    }

    // Use this for initialization
    void Start () {
        vel = Vector2.right;
        path = new List<Vector3>();
        spline = GetComponent<CatmullRomSpline>();
        currentPatrolTime = 0f;
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
        facingRight = true;
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
        currentPatrolTime += timeStepInSec;
        if (!playerScript.OnPlatform)
            player = null;
        HandlePatrolling();
	}

    void HandlePatrolling()
    {
        Vector3 vec = lastPos;
        Vector3 flipDir = nextPos - lastPos;
        float distCovered = (Time.time - startTime) * movementSpeed;
        float fracJourney = distCovered / journeyLength;

        Vector3 pos = Vector3.Lerp(lastPos, nextPos, fracJourney);
        float deltax = (pos - this.transform.position).x;
        
        if (player != null)
        {
            //player.GetComponent<Rigidbody2D>().velocity = (pos - this.transform.position) * movementSpeed;
            //player.position = new Vector3(player.position.x + deltax, player.position.y , player.position.z);
            player.position = player.position + (pos - this.transform.position);
        }
        this.transform.position = pos;


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
}
