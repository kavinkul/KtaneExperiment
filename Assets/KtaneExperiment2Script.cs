using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KtaneExperiment2Script : MonoBehaviour  
{
	public Animator DoorAnimator;
	public KMSelectable ActivatorCube;

	void Start ()
	{
		ActivatorCube.OnInteract += delegate
		{
			Animation();
			return false;
		};
	}

	private void Animation()
    {
		AnimatorStateInfo currentAnimationState = DoorAnimator.GetCurrentAnimatorStateInfo(0);
		if (currentAnimationState.IsName("Door Closed"))
			DoorAnimator.Play("Door Opening");
		else if (currentAnimationState.IsName("Door Opened"))
			DoorAnimator.Play("Door Closing");
	}
}
