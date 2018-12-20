using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FrogIK : MonoBehaviour {

	#region Declaration
	[SerializeField]
	Transform _RFootTrans;

	Animator animator;
	#endregion

	void Awake() {
		animator = GetComponent<Animator>();
	}

	void OnAnimatorIK() {
		animator.SetIKPosition(AvatarIKGoal.LeftFoot,Vector3.zero);
		animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
	}
}
