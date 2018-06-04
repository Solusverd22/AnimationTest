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
	private float Smoothing = 0.1f;
	private Vector2 targetAngle;
	private Vector2 smoothAngle;
	Vector3 cv;

	// Use this for initialization
	void Start () {
		PlayerPrefs.GetFloat("mouseSensitivity", mouseSensitivity);
		playerTrans = transform.parent;
		myTrans = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		myTrans.LookAt(playerTrans.position + Vector3.up * 2.5f);

		xMovement();
		yMovement();

		print("target angle: "+targetAngle);
	}

    private void xMovement() {
        targetAngle.x += Input.GetAxis("Mouse X")*mouseSensitivity/360;
		float posX = 3 * Mathf.Sin(targetAngle.x);
		float posZ = 3 * Mathf.Cos(targetAngle.x);

		//SMOOTH DAMP IS THE BEST WOOOOOO!!!
		myTrans.localPosition = Vector3.SmoothDamp(myTrans.localPosition,new Vector3(posX,myTrans.localPosition.y,posZ),ref cv,Smoothing);
    }

	private void yMovement() {
        targetAngle.y += Input.GetAxis("Mouse Y") * mouseSensitivity;
    }
}
