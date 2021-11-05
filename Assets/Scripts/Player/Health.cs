using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * Relies on PC being tagged as "Player" for death handling.
 */
public class Health : MonoBehaviour {

    [Tooltip("Should health regenerate over time?")]
    public bool regenerateHealth = false;
    [Tooltip("Time in seconds to regenerate 1 health.")]
    public int regenRate = 5;

    public IntVariable maxHealth;//maximum possible health

    /*Value is updated at runtime to reflect this entities current health*/
    public IntVariable currentHealth;

    [Tooltip("Reference to number of restart tokens the player has")]
    public IntVariable RestartCounter;

    public GameEvent RestartTokensDepleted;
    public GameEvent PlayerDeathEvent;
    public PlayerProgression PlayerProgression;
    
    /*The timestamp of the last time 1 health was generated*/
    private float lastRegenTime;

    private Vector2 boxSize;
    void Start () {
        lastRegenTime = Time.time;
        boxSize = GetComponent<BoxCollider2D>().size;
	}

    public void RegenerateHealth()
    {
        regenerateHealth = true;
    }

    public void StopRegeneration()
    {
        regenerateHealth = false;
    }
	
	// Update is called once per frame
	void Update () {
		if (regenerateHealth)
            HandleRegen();
	}

    void HandleRegen()
    {
        if (Time.time >= lastRegenTime + regenRate)
        {
            if (currentHealth.RuntimeValue < maxHealth.InitialValue)
            {
                lastRegenTime = Time.time;
                currentHealth.RuntimeValue++;
            }
        }
    }

    /*
     * Deal ammount of damage to player hp. triggers player death event if hp drops to zero 
     * and restart tokens are depleted
     */
    public void Damage(int ammount)
    {
        if (currentHealth.RuntimeValue > 0)
            currentHealth.RuntimeValue -= ammount;
        if (currentHealth.RuntimeValue <= 0 && this.gameObject.tag == "Player")
        {
            if (RestartCounter.RuntimeValue == 0)
            {
                PlayerProgression.CurrentCheckPoint = 0;
                RestartTokensDepleted.Raise();
            }
            if (RestartCounter.RuntimeValue > 0)
                RestartCounter.RuntimeValue--;

            PlayerDeathEvent.Raise();

            DisableCollision();
            GetComponent<Animator>().SetTrigger("death");
            Debug.Log("PlayerDeathEvent");
            currentHealth.RuntimeValue = currentHealth.InitialValue;
        }
    }

    private void DisableCollision()
    {
        this.gameObject.layer = 9;
    }

    public void EnableCollision()
    {
        this.gameObject.layer = 0;
    }
}
