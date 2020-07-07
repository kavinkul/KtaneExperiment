using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KtaneExperimentScript : MonoBehaviour
{
    public KMSelectable ActivatorCube;
    public Transform CenterOfMass;
    public Transform Cube;
    public Transform ModuleFace;

    private void Start()
    {
        ActivatorCube.OnInteract += delegate
        {
            StartCoroutine(Animation());
            return false;
        };
    }

    private IEnumerator Animation()
    {
        float rotationIncrement = 3f;
        for (int i = 0; i < 30; i++)
        {
            //Rotate the "Cube" around its center of rotation called misleadingly center of mass
            CenterOfMass.localEulerAngles = new Vector3(CenterOfMass.localEulerAngles.x + rotationIncrement, 0, 0);
            //Make sure that module and the cube itself have the same global euler angles
            Cube.eulerAngles = ModuleFace.eulerAngles;
            yield return new WaitForSeconds(.03f);
        }
        //Ensure that the rotation is what we expect in case of floating point error
        CenterOfMass.localEulerAngles = new Vector3(90, 0, 0);
        Cube.eulerAngles = ModuleFace.eulerAngles;
    }
}
