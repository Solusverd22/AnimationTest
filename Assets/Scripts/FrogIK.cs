using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FrogIK : MonoBehaviour {

	#region Declaration
	[Header("Target Transforms")]
	[SerializeField]
	Transform _RFoot;
	[SerializeField]
	Transform _LFoot, _RHand, _LHand, Core, CoreTarget;
	Animator animator;

	Vector3 CoreVelocity = Vector3.zero;
	#endregion

	void Awake() {
		animator = GetComponent<Animator>();
	}

	void OnAnimatorIK() {
		animator.SetIKPosition(AvatarIKGoal.RightHand,_RHand.position);
		animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);

		animator.SetIKPosition(AvatarIKGoal.LeftHand,_LHand.position);
		animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);

		animator.SetIKPosition(AvatarIKGoal.RightFoot,_RFoot.position);
		animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);

		animator.SetIKPosition(AvatarIKGoal.LeftFoot,_LFoot.position);
		animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);


		// CoreVelocity = CoreTarget.position - Core.position;
		// Core.position = Vector3.SmoothDamp(Core.position,CoreTarget.position,ref CoreVelocity, 10000,100000000);
		Core.position = Vector3.Lerp(Core.position,CoreTarget.position,1);
		Core.rotation = Quaternion.Euler(Vector3.RotateTowards(Core.rotation.eulerAngles, CoreTarget.rotation.eulerAngles,1000,1000));
	}
}
