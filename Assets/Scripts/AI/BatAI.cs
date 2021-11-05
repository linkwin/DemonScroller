using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAI : MonoBehaviour {

    public float force;
    public float flapFreq;

    private Rigidbody2D rigidbody;
    private float lastFlapTime;
    private Animator anim;

    private bool facingRight;
    private Vector3 theScale;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Chamber")
            Destroy(this.gameObject);
    }
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        lastFlapTime = Time.time;
        anim = GetComponent<Animator>();
        facingRight = false;
    }

    void Update () {
        anim.ResetTrigger("Flap");
        float noise = Random.Range(-0.25f, 0.25f);
        if (Time.time - (lastFlapTime + noise) >= flapFreq)
            Flap();
	}

    private void Flap()
    {
        Vector2 noise = new Vector2(Random.Range(-0.5f,0.5f), 0);
        Vector3 dir = noise + Vector2.up;
        rigidbody.AddForce(dir * force, ForceMode2D.Impulse);
        anim.SetTrigger("Flap");
        lastFlapTime = Time.time;
        CalcFlip(dir);
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
