using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {

	public position Position;
	public bool CanTeleport = true;
	public float rotate = 40f;
	public float transitionTime = 0.1f;
	public float growFactorBetweenTransitionTime = 0.2f;
	private float scaleStep = 0.2f;

	private Transform trns = null;
	private float waitTime = 0;
	private float growFactor = 0;
	private float initialScale = 0;
	private float targetScaleX = 0;
	private float targetScaleY = 0;
	private float targetScaleZ = 0;
	private float times = 0;
	private bool isTeleporting = false;

	public delegate void myFunction();
 	protected myFunction callbackFct;

	[System.Serializable]
	public class position{
		public float x;
		public float y;
	}

	void OnTriggerEnter2D(Collider2D other)
 	{
		 if (Position == null || !CanTeleport || other.gameObject.tag != "Player" || isTeleporting) return;

		 isTeleporting = true;

		//  other.gameObject.transform.position = new Vector2(Position.x, Position.y);
		 var rgd = other.gameObject.GetComponent<Rigidbody2D>();
		 rgd.velocity = new Vector2(0, 0);
		 
		 var oldGravity = rgd.gravityScale;

		 var oldScaleY = other.transform.localScale.y;
		 var oldScaleX = other.transform.localScale.x;
		 var oldScaleZ = other.transform.localScale.z;

		 other.gameObject.GetComponent<Player>().toggleMoving(false);
		 rgd.gravityScale = 0;

		trns = other.gameObject.transform;
		waitTime = transitionTime;
		growFactor = growFactorBetweenTransitionTime;
		initialScale = oldScaleY;
		targetScaleX = 0;
		targetScaleY = 0;
		targetScaleZ = 0;
		times = initialScale / growFactor;

		callbackFct = () => {
			initialScale = 0;
			targetScaleX = oldScaleX;
			targetScaleY = oldScaleY;
			targetScaleZ = oldScaleZ;
			rotate = -rotate;
			other.gameObject.transform.position = transform.position + (new Vector3(Position.x, Position.y));
			callbackFct = null;
			StartCoroutine("Scale");

			callbackFct = () => {
				isTeleporting = false;
				rgd.gravityScale = oldGravity;
				other.gameObject.GetComponent<Player>().toggleMoving(true);
			};


		};

		StartCoroutine("Scale");

 	}

	 IEnumerator Scale()
     {
		//  bool mustScale = true;
		bool isIncreasing = targetScaleY > initialScale;
		for (int i = 0; i < times; i++)
		{
			float newX = trns.localScale.x > targetScaleX ? trns.localScale.x - growFactor : trns.localScale.x + growFactor;
			float newY = trns.localScale.y > targetScaleY ? trns.localScale.y - growFactor : trns.localScale.y + growFactor;
			float newZ = trns.localScale.z > targetScaleZ ? trns.localScale.z - growFactor : trns.localScale.z + growFactor;
			trns.Rotate(0, 0, rotate);
			trns.localScale = new Vector3(newX, newY, newZ);
             
            yield return new WaitForSeconds(waitTime);
		}

		if (callbackFct != null) callbackFct();

        //  while(mustScale)
        //  {
		// 	 float newX = trns.localScale.x > targetScaleX ? trns.localScale.x - growFactor : trns.localScale.x + growFactor;
		// 	 float newY = trns.localScale.y > targetScaleY ? trns.localScale.y - growFactor : trns.localScale.y + growFactor;
		// 	 float newZ = trns.localScale.z > targetScaleZ ? trns.localScale.z - growFactor : trns.localScale.z + growFactor;
		// 	trns.Rotate(0, 0, rotate);
		// 	Debug.Log(rotate);
		// 	trns.localScale = new Vector3(newX, newY, newZ);

		// 	if (isIncreasing && targetScaleY <= trns.localScale.y) {
		// 		mustScale = false;
		// 		if (callbackFct != null) callbackFct();
		// 		yield return null;
		// 	}
		// 	else if (!isIncreasing && targetScaleY >= trns.localScale.y) {
		// 		mustScale = false;
		// 		if (callbackFct != null) callbackFct();
		// 		yield return null;
		// 	}
             
        //     yield return new WaitForSeconds(waitTime);
        //  }
     }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, new Vector3(Position.x, Position.y, 0f));
    }
}
