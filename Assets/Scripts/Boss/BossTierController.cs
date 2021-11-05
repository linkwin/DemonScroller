using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTierController : MonoBehaviour {


    public int numOfTiers = 3;
    public Transform tier1MoveSet;
    public Transform tier2MoveSet;
    public Transform tier3MoveSet;

    private int currentTier;
    private Animator anim;
    private Health health;
    private MoveSet moveSet;

    // Use this for initialization
    void Start () {
        currentTier = 0;
        anim = GetComponent<Animator>();
        health = GetComponent<Health>();
	}

	// Update is called once per frame
	void Update () {
        if (health.currentHealth.RuntimeValue <= (0.5f * health.currentHealth.InitialValue) && currentTier == 1)//if current health is less than 50%
            BeginTier(++currentTier);
        else if (health.currentHealth.RuntimeValue <= (0.15f * health.currentHealth.InitialValue) && currentTier == 2)
            BeginTier(++currentTier);
	}

    /**
     * Triggers a random move within current tier
     */
    public void TriggerRandomMove()
    {
        int move = Random.Range(0, 3);
        Debug.Log("move#" + move);
        switch (move)
        {
            case 0:
                moveSet.Move1();
                break;
            case 1:
                moveSet.Move2();
                break;
            case 2:
                moveSet.Move3();
                break;
        }
    }

    public void TriggerMove1()
    {
        moveSet.Move1();
    }
    public void TriggerMove2()
    {
        moveSet.Move2();
    }
    public void TriggerMove3()
    {
        moveSet.Move3();
    }

    public void Activate()
    {
        BeginTier(1);
    }

    private void BeginTier(int tierNo)
    {
        Debug.Log("Tier " + tierNo + " Started.");
        if (tierNo == 1)
            moveSet = tier1MoveSet.GetComponent<MoveSet>();
        else if (tierNo == 2)
            moveSet = tier2MoveSet.GetComponent<MoveSet>();
        else if (tierNo == 3)
            moveSet = tier3MoveSet.GetComponent<MoveSet>();
        currentTier = tierNo;

        //anim.SetTrigger("tier" + tierNo + "idle")
    }

    public int CurrentTier
    {
        get
        {
            return currentTier;
        }

        set
        {
            currentTier = value;
        }
    }

}
