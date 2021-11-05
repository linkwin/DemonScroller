using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

	public Transform objectToFollow;
	public float transitionSpeed = 4f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (objectToFollow != null) {

			float interpolation = transitionSpeed * Time.deltaTime;
			
			Vector3 position = transform.position;
			position.y = Mathf.Lerp(transform.position.y, objectToFollow.position.y + 3, interpolation);
			position.x = Mathf.Lerp(transform.position.x, objectToFollow.position.x, interpolation);
			
			this.transform.position = position;
		}
	}
}
