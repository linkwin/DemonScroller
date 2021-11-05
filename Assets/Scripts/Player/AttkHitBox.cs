using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttkHitBox : MonoBehaviour {

    public int attackDamage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
            collision.gameObject.GetComponent<EnemyHealth>().Damage(attackDamage);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
            collision.gameObject.GetComponent<EnemyHealth>().Damage(attackDamage);
    }
}
