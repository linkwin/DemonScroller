using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour {

	public GameObject Player;
	private Player playerScript;

	void Start() {
		playerScript = Player.GetComponent<Player>();
	}
	void OnTriggerEnter2D(Collider2D other)
 	{
		//  if (other.gameObject.tag == "Ground" && !playerScript.isGrounded && !playerScript.justJumped) {
        //  	playerScript.isGrounded = true;
		// 	playerScript.isWallSliding = false;
		// 	Player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
		//  }
 	}

}
