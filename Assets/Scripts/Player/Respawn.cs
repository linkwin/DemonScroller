using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour {

    public PlayerProgression PlayerProgression;

    private Rigidbody2D rigidbody;
    private Animator anim;

	void Start () {
        anim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        DoRespawn();
    }

    public void DoRespawn()
    {
        StartCoroutine(WaitForAnimation(4));
        GameObject[] checkPoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        int findId = PlayerProgression.CurrentCheckPoint;
        bool found = false;
        for (int i = 0; i < checkPoints.Length && !found; i++)
        {
            if (checkPoints[i].GetComponent<CheckPoint>().id == findId)
            {
                found = true;
                this.gameObject.SetActive(false);
                this.transform.position = checkPoints[i].transform.position;
                this.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
                this.gameObject.SetActive(true);
            }
        }
        Health h = GetComponent<Health>();
        h.currentHealth.RuntimeValue = h.currentHealth.InitialValue;
        h.EnableCollision();//reenables collision after death animation
    }
    private IEnumerator WaitForAnimation(float sec)
    {

        yield return new WaitForSeconds(sec);

    }
}
