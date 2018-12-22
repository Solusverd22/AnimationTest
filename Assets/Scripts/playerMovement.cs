using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour {

	private Rigidbody myRigid;
	private Transform myTrans;

	public Transform playerModel;
	
	private bool grounded;
	private float g;

	[SerializeField]
	private float moveAccel, maxSpeed;

	[SerializeField]
	Animator animator;

	private Vector2 playerInput;
	private float movementAngle;

	// Use this for initialization
	void Start () {
		myRigid = GetComponent<Rigidbody>();
		myTrans = GetComponent<Transform>();
		g = Physics.gravity.y;
	}
	
	// Update is called once per frame
	void Update () {
		Move();
		Drag();
		Jump(6.5f);
		Gravity();
		Rotate();
		animator.SetFloat("speed",myRigid.velocity.sqrMagnitude);
	}

	void input(float angle) {
		float vY = -Mathf.Cos(angle/360) * Input.GetAxis("Vertical");
		float vX = -Mathf.Sin(angle/360) * Input.GetAxis("Vertical");

		float hY = Mathf.Sin(angle/360) * Input.GetAxis("Horizontal");
		float hX = -Mathf.Cos(angle/360) * Input.GetAxis("Horizontal");

		playerInput.y = vY + hY;
		playerInput.x = vX + hX;

	}

    void Rotate() {
        //make player model rotate towards direction of travel
		if(myRigid.velocity.sqrMagnitude > 0.5){
			movementAngle = Mathf.Atan2(myRigid.velocity.x,myRigid.velocity.z);
			playerModel.localRotation = Quaternion.Euler(0,movementAngle * Mathf.Rad2Deg,0); //-----------MAKE THIS A SMOOTH TURN-------------
			print("angle: "+ movementAngle * Mathf.Rad2Deg);	
		}
		
    }

    void Move() {
		float xMove = playerInput.x*moveAccel;
		float zMove = playerInput.y*moveAccel;
		myRigid.AddRelativeForce(xMove,0,zMove,ForceMode.Acceleration);

		//stops the player if travelling slower than
		if(playerInput.x == 0 && Mathf.Abs(myRigid.velocity.x) < 1 && Mathf.Abs(myRigid.velocity.x) > 0){
			myRigid.AddRelativeForce(-myRigid.velocity.x,0,0,ForceMode.Impulse);
		}
		if(playerInput.y == 0 && Mathf.Abs(myRigid.velocity.z) < 1 && Mathf.Abs(myRigid.velocity.z) > 0){
			myRigid.AddRelativeForce(0,0,-myRigid.velocity.z,ForceMode.Impulse);
		}
	}

	void Drag (){
		//gets the absolute values (no signs) so it doesnt make the lerping all fucky
		float xVel = Mathf.Abs(myRigid.velocity.x);
		float zVel = Mathf.Abs(myRigid.velocity.z);
		//------------------------------------TO DO-----------------------------------------------
		//gets maxspeed for x and z axis (google "cos sin animation" to remember)
		// float xmaxSpeed = Mathf.Cos(myRigid.velocity.magnitude/myRigid.velocity.z);
		// float zmaxSpeed = Mathf.Cos(myRigid.velocity.x);
		//----------------------------------------------------------------------------------------
		//lerps to the level of force needed to counteract the Input given by the player
		float xDrag = Mathf.Pow(xVel /maxSpeed,0.5f) * moveAccel;
		float zDrag = Mathf.Pow(zVel /maxSpeed,0.5f) * moveAccel;
		//makes the drag sign inverse to the current direction of travel for each axis
		xDrag = -xDrag * Mathf.Sign(myRigid.velocity.x);
		zDrag = -zDrag * Mathf.Sign(myRigid.velocity.z);

		//Debug.Log("Speed: " + myRigid.velocity.magnitude);

		//applies drag
		myRigid.AddRelativeForce(xDrag,0,zDrag,ForceMode.Acceleration);
	}

	void Jump(float jumpVelocity) {
		if(Input.GetButtonDown("Jump") && grounded){
			myRigid.AddRelativeForce(0,jumpVelocity,0,ForceMode.Impulse);
		}
	}

	void Gravity() {
		if(!grounded){
			myRigid.AddForce(Vector3.up*g);
		}
	}

	void OnTriggerStay() {
		grounded = true;
	}

	void OnTriggerExit() {
		grounded = false;
	}
}
