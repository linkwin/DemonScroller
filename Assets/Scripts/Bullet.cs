using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	private float speed = 10f;
	private Rigidbody2D rgd;

	// Use this for initialization
	void Start () {
		rgd = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		rgd.AddForce (Vector3.forward * speed);
	}
}
