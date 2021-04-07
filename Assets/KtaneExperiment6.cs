using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KtaneExperiment6 : MonoBehaviour {

	public Transform Module;
	public Transform CubeParentTransform;
	public Transform CubeFrameTransform;
	public KMSelectable GreenCube;

	private int _currentRotation = 0;
	private float _angle = 90f;

	private Vector3[] _allAxes;

	// Use this for initialization
	void Start () {
		_allAxes = new Vector3[]
		{
			new Vector3(1, 0, 0),
			new Vector3(1, 0, 1),
			new Vector3(0, 0, 1),
			new Vector3(-1, 0, 1),
			new Vector3(-1, 0, 0),
			new Vector3(-1, 0, -1),
			new Vector3(0, 0, -1),
			new Vector3(1, 0, -1),
		};
		GreenCube.OnInteract += delegate { _currentRotation = (_currentRotation + 1) % 8; return false; };
	}

	void Rotate()
	{
		CubeParentTransform.localEulerAngles = new Vector3(0, 0, 0);
		CubeFrameTransform.SetParent(CubeParentTransform);
		CubeParentTransform.localRotation *= Quaternion.AngleAxis(_angle * Time.deltaTime, _allAxes[_currentRotation]);
		CubeFrameTransform.SetParent(Module);
	}
	
	// Update is called once per frame
	void Update () {
		Rotate();
	}
}
