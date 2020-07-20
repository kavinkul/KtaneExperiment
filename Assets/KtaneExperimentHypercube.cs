using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Experiment4;

public class KtaneExperimentHypercube : MonoBehaviour
{
    public GameObject ModuleObject;
    public GameObject CylinderObject;

    private GameObject[] _testObject;
    private GameObject[] _edgeObjects;
    private static readonly Point4D[] _vertices = new Point4D[16]
    {
        new Point4D(-1, -1, -1, -1),
        new Point4D(-1, -1, -1, 1),
        new Point4D(-1, -1, 1, -1),
        new Point4D(-1, 1, -1, -1),
        new Point4D(1, -1, -1, -1),
        new Point4D(-1, -1, 1, 1),
        new Point4D(-1, 1, -1, 1),
        new Point4D(1, -1, -1, 1),
        new Point4D(-1, 1, 1, -1),
        new Point4D(1, -1, 1, -1),
        new Point4D(1, 1, -1, -1),
        new Point4D(1, 1, 1, -1),
        new Point4D(1, 1, -1, 1),
        new Point4D(1, -1, 1, 1),
        new Point4D(-1, 1, 1, 1),
        new Point4D(1, 1, 1, 1)
    };
    private Edge4D[] _edges = new Edge4D[32]
    {
        new Edge4D(_vertices[0], _vertices[1]),
        new Edge4D(_vertices[0], _vertices[2]),
        new Edge4D(_vertices[0], _vertices[3]),
        new Edge4D(_vertices[0], _vertices[4]),
        new Edge4D(_vertices[1], _vertices[5]),
        new Edge4D(_vertices[1], _vertices[6]),
        new Edge4D(_vertices[1], _vertices[7]),
        new Edge4D(_vertices[2], _vertices[5]),
        new Edge4D(_vertices[2], _vertices[8]),
        new Edge4D(_vertices[2], _vertices[9]),
        new Edge4D(_vertices[3], _vertices[6]),
        new Edge4D(_vertices[3], _vertices[8]),
        new Edge4D(_vertices[3], _vertices[10]),
        new Edge4D(_vertices[4], _vertices[7]),
        new Edge4D(_vertices[4], _vertices[9]),
        new Edge4D(_vertices[4], _vertices[10]),
        new Edge4D(_vertices[8], _vertices[11]),
        new Edge4D(_vertices[9], _vertices[11]),
        new Edge4D(_vertices[10], _vertices[11]),
        new Edge4D(_vertices[6], _vertices[12]),
        new Edge4D(_vertices[7], _vertices[12]),
        new Edge4D(_vertices[10], _vertices[12]),
        new Edge4D(_vertices[5], _vertices[13]),
        new Edge4D(_vertices[7], _vertices[13]),
        new Edge4D(_vertices[9], _vertices[13]),
        new Edge4D(_vertices[5], _vertices[14]),
        new Edge4D(_vertices[6], _vertices[14]),
        new Edge4D(_vertices[8], _vertices[14]),
        new Edge4D(_vertices[11], _vertices[15]),
        new Edge4D(_vertices[12], _vertices[15]),
        new Edge4D(_vertices[13], _vertices[15]),
        new Edge4D(_vertices[14], _vertices[15]),
    };
    private float[][] _projectionMatrix = new float[3][]
    {
        new float[4] { 5f, 0f, 0f, 1.5f },
        new float[4] { 0f, 5f, 0f, 1.5f },
        new float[4] { 0f, 0f, 5f, 1.5f }
    };
    void Start () 
    {
        Hypercube();
    }

    private void Hypercube()
    {
        _testObject = _vertices.Select(p => {
            p.Projection(_projectionMatrix);
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            go.transform.parent = ModuleObject.transform;
            go.transform.localPosition = new Vector3(p.ProjectedPoint.X, p.ProjectedPoint.Y, p.ProjectedPoint.Z);
            go.transform.localScale = new Vector3(1, 1, 1);
            p.gameObject = go;
            return go;
        }).ToArray();
        _edgeObjects = _edges.Select(e => {
            GameObject go = Instantiate(CylinderObject);
            go.transform.parent = ModuleObject.transform;
            Point3D midpoint = Point3D.Median(e.FirstPoint.ProjectedPoint, e.SecondPoint.ProjectedPoint);
            go.transform.localPosition = new Vector3(midpoint.X, midpoint.Y, midpoint.Z);
            go.transform.LookAt(e.FirstPoint.gameObject.transform);
            go.transform.localScale = new Vector3(0.5f, 0.5f, Point3D.Distance(e.FirstPoint.ProjectedPoint, e.SecondPoint.ProjectedPoint) / 2);
            return go;
        }).ToArray();
    }
}
