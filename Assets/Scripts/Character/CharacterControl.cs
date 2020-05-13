using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterControl : CharacterMovement {

	[Tooltip("Set the multiplier for the height of the jump e.g: 1.5f if you want the character to jump 1.5 times his height")]
	public float jumpHeigthMultiplier;
	[Tooltip("Set the character speed")]
	public float speed = 10f;
	[Tooltip("Set the character jump strength")]
	public float jumpStrength;

	[Header("Jump parameters")] 
	public float jumpTime;
	float jumpEndTime;
    private bool isGrounded;

    [Header("Ground Check")] 
    public Transform topLeft;
	public Transform bottomRight;
	[SerializeField] private LayerMask groundCheckLayerMask;
    
    
    [Header("Components")]
	public Rigidbody2D rb;
	public Animator CharacterAnimator;
	SpriteRenderer sprite;

	private Vector3 respawnPosition;
    public float WalkingSpeed = 1.5f;
    public float RunningSpeed = 2f;


    #region Input vars

    private float movH;
	private bool isJumping = false;

	#endregion
    


    public override Rigidbody2D CharacterRigidBody
	{
		get
		{
			return rb;
		}
	}

	// Use this for initialization
	void Start () {
		sprite = this.GetComponentInChildren<SpriteRenderer> ();
		respawnPosition = transform.position;
		isJumping = false;
	}

	void Update()
	{
/*
        if (Input.GetKey(KeyCode.R))
        {
            Respawn();
        }
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            jumpStartTime = Time.time;
            isJumping = true;
        }
        if ((Input.GetButton("Jump") && (Time.time < jumpStartTime + jumpHeigthMultiplier)))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpStrength);
        }
        if (Input.GetButtonDown("Run"))
        {
            CharacterAnimator.SetBool("Running", true);
            speed = RunningSpeed;
        }
        else if (Input.GetButtonUp("Run"))
        {
            CharacterAnimator.SetBool("Running", false);
            speed = WalkingSpeed;
        }

        if (Mathf.Abs(rb.velocity.y) > 0)
		{
			CharacterAnimator.SetBool("Jumping", true);
		}
		else
		{
			CharacterAnimator.SetBool("Jumping", false);
			isJumping = false;
		}

		if (Mathf.Abs(rb.velocity.x) > 0 && rb.velocity.y == 0)
		{
			CharacterAnimator.SetBool("Moving", true);
		}
		else
		{
			CharacterAnimator.SetBool("Moving", false);
		}
			
		if (rb.velocity.x < 0) {
			sprite.flipX = true;
		} else {
			sprite.flipX = false;
		}
		
		*/	
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		DoMovement();
	}

	private void DoMovement()
	{
		Vector2 mov = new Vector2();

		
		if (isJumping)
		{
			Debug.Log("Started Jump");
			mov.y += jumpStrength;
			if (isGrounded)
			{
				jumpEndTime = Time.time + jumpTime;
				isGrounded = false;
			}
			else if (Time.time > jumpEndTime)
			{
				Debug.Log("Ended jump");
				isJumping = false;
			}
		}
		else
		{
			mov.y += rb.velocity.y;
		}
		
		CheckGrounded();
		
		mov.x += speed * movH;

		rb.velocity = mov;
	}

	private void CheckGrounded()
	{
		isGrounded = Physics2D.OverlapArea(topLeft.position, bottomRight.position, groundCheckLayerMask);
	}

	private void HandleJumping(Vector2 mov)
	{
		
	}

	void OnCollisionEnter2D (Collision2D coll){
		if (coll.collider == coll.gameObject.GetComponent<EdgeCollider2D> ()) {
			//jumping = false;
		}
	}

    public void Respawn()
    {
        rb.Sleep();
        this.transform.position = respawnPosition;
        isJumping = false;
    }


    #region Receive Input

    public void ReceiveMovInput(float mov)
    {
	    movH = mov;
    }

    public void ReceiveJumpInput(bool b)
    {
	    if (b)
	    {
		    if (isGrounded)
			    isJumping = true;
	    }
	    else
	    {
		    isJumping = false;
	    }
    }

    #endregion

    private void OnDrawGizmos()
    {
	    Gizmos.color = Color.red;
	    Gizmos.DrawSphere(topLeft.position, 0.01f);
	    Gizmos.DrawSphere(bottomRight.position, 0.01f);
	    Gizmos.color = Color.green;
	    Gizmos.DrawLine(topLeft.position, new Vector3(bottomRight.position.x, topLeft.position.y));
	    Gizmos.DrawLine(topLeft.position, new Vector3(topLeft.position.x, bottomRight.position.y));
	    Gizmos.DrawLine(bottomRight.position, new Vector3(topLeft.position.x, bottomRight.position.y));
	    Gizmos.DrawLine(bottomRight.position, new Vector3(bottomRight.position.x, topLeft.position.y));
	    
    }
}
