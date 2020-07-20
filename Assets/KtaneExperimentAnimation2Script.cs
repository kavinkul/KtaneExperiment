using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KtaneExperimentAnimation2Script : MonoBehaviour
{

	public Animator ModuleAnimator;
	public GameObject[] UncutWires;
	public GameObject[] CutWires;
	public KMSelectable ActivatorCube;

	void Awake()
    {
		ActivatorCube.OnInteract += delegate
		{
			Animation();
			return false;
		};
	}

	void Start()
	{
		foreach (GameObject o in UncutWires)
        {
			o.SetActive(true);
        }

		foreach (GameObject o in CutWires)
		{
			o.SetActive(false);
        }
	}

	private void Animation()
	{
		AnimatorStateInfo currentAnimationState = ModuleAnimator.GetCurrentAnimatorStateInfo(0);
		if (currentAnimationState.IsName("Door Closed"))
			ModuleAnimator.Play("Door Opening");
		else if (currentAnimationState.IsName("Door Opened"))
			ModuleAnimator.Play("Door Closing");
	}
}
