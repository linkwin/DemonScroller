using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public int defaultHealth;
    private int currentHealth;
    private Animator anim;

    void Start() {
        currentHealth = defaultHealth;
        anim = GetComponent<Animator>();
        anim.SetInteger("Health", currentHealth);
    }

    public void Damage(int amm)
    {
        currentHealth -= amm;
        if (currentHealth <= 0)
        {
            //death
        }
        anim.SetInteger("Health", currentHealth);
    }

    //called at end of death animation
    public void Death()
    {
        Destroy(this.gameObject);
    }
}
