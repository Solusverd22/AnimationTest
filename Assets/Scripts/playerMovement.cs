using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour {

	private Rigidbody myRigid;

	private float mouseSensitivity = 10;
	private float targetAngle;

	// Use this for initialization
	void Start () {
		PlayerPrefs.GetFloat("mouseSensitivity", mouseSensitivity);
		myRigid = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log(mouseSensitivity);
		myRigid.AddRelativeForce(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"),ForceMode.Impulse);
	}
}
