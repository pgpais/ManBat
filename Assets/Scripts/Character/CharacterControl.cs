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

	float jumpStartTime;
	bool jumping = false;
	public Rigidbody2D rb;
	public Animator CharacterAnimator;
	SpriteRenderer sprite;

	private Vector3 respawnPosition;
    public float WalkingSpeed = 1.5f;
    public float RunningSpeed = 2f;



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
	}

	void Update()
	{

        if (Input.GetKey(KeyCode.R))
        {
            Respawn();
        }
        if (Input.GetButtonDown("Jump") && !jumping)
        {
            jumpStartTime = Time.time;
            jumping = true;
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
			jumping = false;
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
			
			
	}

	// Update is called once per frame
	void FixedUpdate () {

		
        rb.velocity = new Vector2(speed * Input.GetAxis ("Horizontal"), rb.velocity.y);
        

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
        jumping = false;
    }
}
