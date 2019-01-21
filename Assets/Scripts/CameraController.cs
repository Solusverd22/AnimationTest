using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	private Transform playerTrans;
	private Transform myTrans;

	[SerializeField]
	[Range(0,100)]
	private float mouseSensitivity = 10;
	private float cameraDistance = 4;
	private float Smoothing = 0.1f;
	public Vector2 targetAngle;
	private Vector3 cv;

	// Use this for initialization
	void Start () {
		PlayerPrefs.GetFloat("mouseSensitivity", mouseSensitivity);
		playerTrans = transform.parent;
		myTrans = GetComponent<Transform>();
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
		myTrans.LookAt(playerTrans.position + Vector3.up * 2.5f);

		xMovement();
		yMovement();

		playerTrans.gameObject.SendMessage("input",targetAngle.x);

		if (Input.GetKeyDown(KeyCode.Escape)) {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
		if (Input.GetMouseButtonDown(0)) {
			Cursor.lockState = CursorLockMode.Locked; 
			Cursor.visible = false;
		}
	}

    private void xMovement() {
        targetAngle.x += Input.GetAxis("Mouse X")*mouseSensitivity;
		float posX = cameraDistance * Mathf.Sin(targetAngle.x/360);
		float posZ = cameraDistance * Mathf.Cos(targetAngle.x/360);

		//SMOOTH DAMP IS THE BEST WOOOOOO!!!
		myTrans.localPosition = Vector3.SmoothDamp(myTrans.localPosition,new Vector3(posX,(targetAngle.y/90)+2.7f,posZ),ref cv,Smoothing);
    }

	private void yMovement() {
		if( (targetAngle.y/90 <= 2 && Mathf.Sign(Input.GetAxis("Mouse Y")) == -1)  (targetAngle.y/90 >= -1 && Mathf.Sign(Input.GetAxis("Mouse Y")) == -1) ){
			targetAngle.y -= Input.GetAxis("Mouse Y") * mouseSensitivity;
		}
    }
}
