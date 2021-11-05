using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float speed = 3f;
	public float runningSpeed = 9.5f;
	public float jump = 9.5f;
	private bool isFacingRight = true;
	private Animator anim;
	private bool isMoving = false;
	private bool isRunning = false;
	private bool isJumping = false;
	private bool isGrounded = false;
	private bool isTouchingWall = false;
    private bool onPlatform = false;
	
	private bool isDead = false;
	private bool justJumped = false;
	private bool isTouchingOtherObject = false;
	private bool canMove = true;
	private Rigidbody2D rgd;

	private bool isWallSliding = false;

	private float oldGravity;

	public Transform groundCheck;
	public Transform wallCheck;
	public LayerMask layerGround;

    public bool JustJumped
    {
        get
        {
            return justJumped;
        }
    }

    public bool OnPlatform
    {
        get
        {
            return onPlatform;
        }
    }

    // Use this for initialization
    void Start () {
		anim = gameObject.GetComponent<Animator>();
		rgd = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		isGrounded = Physics2D.Linecast(transform.position, groundCheck.position, layerGround);
		isTouchingWall = Physics2D.Linecast(transform.position, wallCheck.position, layerGround);
        anim.SetBool("onWall", isTouchingWall);
        if (isMoving)
            anim.SetFloat("movement", 0.25f);
        else
            anim.SetFloat("movement", 0f);
        if (isRunning && isMoving)
            anim.SetFloat("movement", 0.6f);
        if (isGrounded)
            anim.SetBool("jump", false);

        anim.ResetTrigger("Attack");
		if (!isDead && canMove) {
            
            if (Input.GetAxis("Fire1") > 0)
                anim.SetTrigger("Attack");

			if(Input.GetKey(KeyCode.D))
			{
                Debug.Log("right");
				if (isFacingRight == false) Flip();
				isMoving = true;
				MoveRight();
			}
			else if(Input.GetKey(KeyCode.A))
			{
				if (isFacingRight == true) Flip();
				isMoving = true;
				MoveLeft();
			}
			else {
				isMoving = false;
			}
			
			if((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && isMoving && !isWallSliding)
			{
				isRunning = false;
			}
			else {
				isRunning = true;
			}

			if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
			{
				isGrounded = false;
				Jump();
			}

			if (!isGrounded && isTouchingWall && !isWallSliding) {
				startSliding();
		 	}
			else if (isWallSliding && (!isTouchingWall || isGrounded)) {
				stopSliding();
			}

			
			if (!isMoving && !isRunning && isGrounded) {
			}
            Collider2D collision = Physics2D.OverlapCircle(groundCheck.position, 1);
            if (collision.tag == "MovingPlatform")
                onPlatform = true;
            else
                onPlatform = false;
		}
	}

	private void startSliding() {
		isWallSliding = true;
        anim.SetBool("onWall", true);
		rgd.velocity = new Vector2(0, 0);
		oldGravity = rgd.gravityScale;
		rgd.gravityScale = 0.1f;
	}
	private void stopSliding() {
		isWallSliding = false;
		rgd.gravityScale = oldGravity;
        anim.SetBool("onWall", false);
		if (!isGrounded) {
			isJumping = true;
			anim.Play("Jumping");
		}
	}

	public void toggleMoving(bool move){
		canMove = move;
	}

	 void OnCollisionEnter2D (Collision2D other) 
     {
		// if (other.gameObject.tag == "Ground") {
		// 	isGrounded = true;
		// }
        // isTouchingOtherObject = true;
     }
	 void OnCollisionExit2D (Collision2D other)
    {
		// if (other.gameObject.tag == "Ground") {
		// 	isGrounded = false;
		// }
        // isTouchingOtherObject = false;
		// isGrounded = false;
		//  if (isRunning) anim.Play("RunningJump");
		// else anim.Play("Jumping");
    }

	 void OnTriggerEnter2D(Collider2D other)
 	{
		 if (other.gameObject.tag == "Obstacle") {
            GetComponent<Health>().Damage(1);
			 //isDead = true;
			 //anim.Play("Dying");
		 }
		//  else if ((other.gameObject.tag == "Ground" || other.gameObject.tag == "Obstacle") && !isGrounded && !justJumped) {
        //  	isGrounded = true;
		// 	rgd.velocity = new Vector2(0, 0);
		//  }
 	}

	private void Jump(){

		rgd.velocity = new Vector2(0, 0);
        // rgd.angularVelocity = new Vector2(0, 0); 
        anim.SetBool("jump", true);
		justJumped = true;
		Invoke("setJustJumpedToFalse", 0.1f);
		//rgd.AddForce(new Vector2(0, jump), ForceMode2D.Impulse);
		rgd.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
	}

	private void setJustJumpedToFalse(){
		justJumped = false;
	}

	private void MoveRight () {

		if (isFacingRight && isTouchingWall) return;

		float currSpeed = isRunning ? runningSpeed : speed;
		if (isGrounded) {
			//if (isRunning) anim.Play("Running");
			//else anim.Play("Walking");
		}
        transform.position += Vector3.right * currSpeed * Time.deltaTime;
	}
	private void MoveLeft () {

		if (!isFacingRight && isTouchingWall) return;

		float currSpeed = isRunning ? runningSpeed : speed;
		if (isGrounded) {
			//if (isRunning) anim.Play("Running");
			//else anim.Play("Walking");
		}
        transform.position += Vector3.right * -currSpeed * Time.deltaTime;
	}

	protected void Flip()    
      {
          isFacingRight = !isFacingRight;
  
          Vector3 theScale = transform.localScale;
          theScale.x *= -1;
          transform.localScale = theScale;
  
          // Flip collider over the x-axis
          // center.x = -center.x;
      }
}
