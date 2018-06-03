using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour {

	public Rigidbody myRigid;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		myRigid.AddRelativeForce(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"),ForceMode.Impulse);
	}
}
