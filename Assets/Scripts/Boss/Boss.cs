using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BossTierController))]
public class Boss : MonoBehaviour {

    public enum BossState {BeginIdle, Idle, ExecutingMove};

    private BossState currentState;
    private Vector3 facing;

    private BossTierController tierController;
    private Animator anim;

    private Transform player;

    private float timeStamp;
    private float delayTime;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            tierController.Activate();//starts tier1
    }

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        tierController = GetComponent<BossTierController>();
        anim = GetComponent<Animator>();
        facing = Vector2.left;
        currentState = BossState.BeginIdle;
        //anim.setTrigger("idle")
	}
	
	// Update is called once per frame
	void Update () {
        if (currentState == BossState.BeginIdle)
        {
            timeStamp = Time.time;
            delayTime = Random.Range(1f, 3f);
            currentState = BossState.Idle;
            //anim.setTrigger("idle")
        }

        if (Time.time - timeStamp >= delayTime && currentState == BossState.Idle && tierController.CurrentTier != 0)
        {
            tierController.TriggerRandomMove();
            currentState = BossState.ExecutingMove;
        }
	}

    public void Flip()
    {
        if (GetComponent<SpriteRenderer>().flipX)
        {
            facing = Vector2.left;
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            facing = Vector2.right;
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public Vector3 Facing
    {
        get
        {
            return facing;
        }

        set
        {
            facing = value;
        }
    }

    public BossState CurrentState
    {
        get
        {
            return currentState;
        }

        set
        {
            currentState = value;
        }
    }
}
