using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Experiment4;

public class KtaneExperimentHypercube : MonoBehaviour
{
    public KMSelectable ActivatorCube;
    public GameObject CylinderObject;
    public Transform HypercubeTransform;

    private Coroutine _activeCoroutine = null;
    private Point4D[] _vertices;
    private Edge4D[] _edges;
    private float[][] _projectionMatrix = new float[3][]
    {
        new float[4] { 5f, 0f, 0f, 1.5f },
        new float[4] { 0f, 5f, 0f, 1.5f },
        new float[4] { 0f, 0f, 5f, 1.5f }
    };
    private static float _angle = Mathf.PI * 5f / 180f;
    private float[][] _rotationMatrix = new float[4][]
    {
        new float[4] { Mathf.Cos(_angle), 0f, 0f, -Mathf.Sin(_angle) },
        new float[4] { 0f, 1f, 0f, 0f},
        new float[4] { 0f, 0f, 1f, 0f},
        new float[4] { Mathf.Sin(_angle), 0f, 0f, Mathf.Cos(_angle) },
    };
    void Start () 
    {
        Hypercube();
        ActivatorCube.OnInteract += delegate 
        {
            if (_activeCoroutine == null)
                _activeCoroutine = StartCoroutine(Animation());
            return false; 
        };
    }

    private void Hypercube()
    {
        _vertices = new Point4D[16]
        {
            new Point4D(-1, -1, -1, -1, HypercubeTransform),
            new Point4D(-1, -1, -1, 1, HypercubeTransform),
            new Point4D(-1, -1, 1, -1, HypercubeTransform),
            new Point4D(-1, 1, -1, -1, HypercubeTransform),
            new Point4D(1, -1, -1, -1, HypercubeTransform),
            new Point4D(-1, -1, 1, 1, HypercubeTransform),
            new Point4D(-1, 1, -1, 1, HypercubeTransform),
            new Point4D(1, -1, -1, 1, HypercubeTransform),
            new Point4D(-1, 1, 1, -1, HypercubeTransform),
            new Point4D(1, -1, 1, -1, HypercubeTransform),
            new Point4D(1, 1, -1, -1, HypercubeTransform),
            new Point4D(1, 1, 1, -1, HypercubeTransform),
            new Point4D(1, 1, -1, 1, HypercubeTransform),
            new Point4D(1, -1, 1, 1, HypercubeTransform),
            new Point4D(-1, 1, 1, 1, HypercubeTransform),
            new Point4D(1, 1, 1, 1, HypercubeTransform)
        };
        _edges = new Edge4D[32]
        {
            new Edge4D(_vertices[0], _vertices[1], HypercubeTransform, CylinderObject),
            new Edge4D(_vertices[0], _vertices[2], HypercubeTransform, CylinderObject),
            new Edge4D(_vertices[0], _vertices[3], HypercubeTransform, CylinderObject),
            new Edge4D(_vertices[0], _vertices[4], HypercubeTransform, CylinderObject),
            new Edge4D(_vertices[1], _vertices[5], HypercubeTransform, CylinderObject),
            new Edge4D(_vertices[1], _vertices[6], HypercubeTransform, CylinderObject),
            new Edge4D(_vertices[1], _vertices[7], HypercubeTransform, CylinderObject),
            new Edge4D(_vertices[2], _vertices[5], HypercubeTransform, CylinderObject),
            new Edge4D(_vertices[2], _vertices[8], HypercubeTransform, CylinderObject),
            new Edge4D(_vertices[2], _vertices[9], HypercubeTransform, CylinderObject),
            new Edge4D(_vertices[3], _vertices[6], HypercubeTransform, CylinderObject),
            new Edge4D(_vertices[3], _vertices[8], HypercubeTransform, CylinderObject),
            new Edge4D(_vertices[3], _vertices[10], HypercubeTransform, CylinderObject),
            new Edge4D(_vertices[4], _vertices[7], HypercubeTransform, CylinderObject),
            new Edge4D(_vertices[4], _vertices[9], HypercubeTransform, CylinderObject),
            new Edge4D(_vertices[4], _vertices[10], HypercubeTransform, CylinderObject),
            new Edge4D(_vertices[8], _vertices[11], HypercubeTransform, CylinderObject),
            new Edge4D(_vertices[9], _vertices[11], HypercubeTransform, CylinderObject),
            new Edge4D(_vertices[10], _vertices[11], HypercubeTransform, CylinderObject),
            new Edge4D(_vertices[6], _vertices[12], HypercubeTransform, CylinderObject),
            new Edge4D(_vertices[7], _vertices[12], HypercubeTransform, CylinderObject),
            new Edge4D(_vertices[10], _vertices[12], HypercubeTransform, CylinderObject),
            new Edge4D(_vertices[5], _vertices[13], HypercubeTransform, CylinderObject),
            new Edge4D(_vertices[7], _vertices[13], HypercubeTransform, CylinderObject),
            new Edge4D(_vertices[9], _vertices[13], HypercubeTransform, CylinderObject),
            new Edge4D(_vertices[5], _vertices[14], HypercubeTransform, CylinderObject),
            new Edge4D(_vertices[6], _vertices[14], HypercubeTransform, CylinderObject),
            new Edge4D(_vertices[8], _vertices[14], HypercubeTransform, CylinderObject),
            new Edge4D(_vertices[11], _vertices[15], HypercubeTransform, CylinderObject),
            new Edge4D(_vertices[12], _vertices[15], HypercubeTransform, CylinderObject),
            new Edge4D(_vertices[13], _vertices[15], HypercubeTransform, CylinderObject),
            new Edge4D(_vertices[14], _vertices[15], HypercubeTransform, CylinderObject),
        };
        foreach (Point4D p in _vertices)
            p.Draw(_projectionMatrix);
        foreach (Edge4D e in _edges)
            e.Draw();
    }
    private IEnumerator Animation()
    {
        while (true)
        {
            foreach (Point4D p in _vertices)
            {
                p.Rotation(_rotationMatrix);
                p.Draw(_projectionMatrix);
            }
            foreach (Edge4D e in _edges)
                e.Draw();
            yield return new WaitForSeconds(.05f);
        }
    }
}
