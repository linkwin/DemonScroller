using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour {

    public CountdownTimer ability1Counter;
    public CountdownTimer ability2Counter;
    public CountdownTimer ability3Counter;
    public CountdownTimer ability4Counter;
    public CountdownTimer ability5Counter;

	void Start () {
		
	}

    void Update() {

        if (Time.time >= ability1Counter.Timestamp + ability1Counter.AmmountOfTime)
        {
            if (Input.GetButton("Ability1"))
            {
                HandleAbility1();
                ability1Counter.Timestamp = Time.time;
            }
        }
        if (Time.time >= ability2Counter.Timestamp + ability2Counter.AmmountOfTime)
        {
            if (Input.GetButton("Ability2"))
            {
                HandleAbility2();
                ability2Counter.Timestamp = Time.time;
            }
        }
        if (Time.time >= ability3Counter.Timestamp + ability3Counter.AmmountOfTime)
        {
            if (Input.GetButton("Ability3"))
            {
                HandleAbility3();
                ability3Counter.Timestamp = Time.time;
            }
        }
        if (Time.time >= ability4Counter.Timestamp + ability4Counter.AmmountOfTime)
        {
            if (Input.GetButton("Ability4"))
            {
                HandleAbility4();
                ability4Counter.Timestamp = Time.time;
            }
        }
        if (Time.time >= ability5Counter.Timestamp + ability5Counter.AmmountOfTime)
        {
            if (Input.GetButton("Ability5"))
            {
                HandleAbility5();
                ability5Counter.Timestamp = Time.time;
            }
        }
	}

    void HandleAbility1()
    {
        
    }

    //Shield
    void HandleAbility2()
    {

    }

    //Sprint
    void HandleAbility3()
    {

    }
    
    void HandleAbility4()
    {

    }

    void HandleAbility5()
    {

    }
}
