using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObstacle : MonoBehaviour {

	public float rotateTime = 4f;
	public float rotateDegrees = 90f;
	public Direction direction = Direction.Right;
	public enum Direction 
	{
  		Left, Right
	}

	// Use this for initialization
	void Start () {
		if (direction == Direction.Right) {
			rotateDegrees = -rotateDegrees;
		}
		InvokeRepeating("Rotate", rotateTime, rotateTime);
	}
	
	void Rotate(){

		transform.Rotate(0, 0, rotateDegrees);
    	// transform.RotateAround (position, Vector3.up, Time.deltaTime * 3f);
    	// rotationTime = rotationTime- Time.deltaTime;
		// 	if(rotationTime==0) {
		// 		rotate = false;
		// 		rotationTime = 192;
		// 	}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
