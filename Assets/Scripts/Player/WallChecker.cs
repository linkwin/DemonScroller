using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallChecker : MonoBehaviour {

	public GameObject Player;
	private Player playerScript;
	private Animator anim;
	private float oldGravity;
	private Rigidbody2D rgd;

	void Start() {
		// playerScript = Player.GetComponent<Player>();
		// anim = Player.GetComponent<Animator>();
		// rgd = Player.GetComponent<Rigidbody2D>();
	}
	// void OnTriggerEnter2D(Collider2D other)
 	// {
	// 	 if (other.gameObject.tag == "Wall" && !playerScript.isGrounded && !playerScript.isWallSliding) {
    //      	playerScript.isWallSliding = true;
	// 		anim.Play("WallSliding");
	// 		Player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
	// 		oldGravity = rgd.gravityScale;
	// 		rgd.gravityScale = 0.1f;
	// 	 }
 	// }
	//  void OnTriggerExit2D(Collider2D other)
 	// {
	// 	 if (other.gameObject.tag == "Wall") {
    //      	playerScript.isWallSliding = false;
	// 		rgd.gravityScale = oldGravity;
	// 		if (!playerScript.isGrounded) anim.Play("Jumping");

	// 		// Player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
	// 	 }
 	// }
}
