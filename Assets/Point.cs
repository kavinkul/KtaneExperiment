using System;
using System.Linq;
using UnityEngine;

namespace Experiment4
{
    public class Point4D
    {
        public float X, Y, Z, W;

        public Point3D ProjectedPoint;

        public GameObject gameObject = null;
        public Transform parent = null;
        public Point4D(float x, float y, float z, float w, Transform parent)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
            this.parent = parent;
        }

        public Point4D(float[] coordinate, Transform parent)
        {
            if (coordinate == null)
                throw new ArgumentNullException("Coordinate cannot be null.");
            if (coordinate.Length != 4)
                throw new ArgumentException("Coordinate must have length of 4.");
            X = coordinate[0];
            Y = coordinate[1];
            Z = coordinate[2];
            W = coordinate[3];
            this.parent = parent;
        }

        public void Draw(float[][] projectionMatrix)
        {
            Projection(projectionMatrix);
            if (gameObject == null)
            {
                gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                gameObject.transform.parent = parent;
            }
            gameObject.transform.localPosition = new Vector3(ProjectedPoint.X, ProjectedPoint.Y, ProjectedPoint.Z);
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        public void Projection(float[][] projectionMatrix)
        {
            if (projectionMatrix == null)
                throw new ArgumentNullException("Projection Matrix cannot be null.");
            if (projectionMatrix.Length != 3 || projectionMatrix.Any(p => p.Length != 4))
                throw new ArgumentException("Projection Matrix must have dimension of 3 by 4.");
            float[] temp = new float[3];
            for (int i = 0; i < 3; i++)
                temp[i] = projectionMatrix[i][0] * X + projectionMatrix[i][1] * Y + projectionMatrix[i][2] * Z + projectionMatrix[i][3] * W;
            ProjectedPoint = new Point3D(temp[0], temp[1], temp[2]);
        }
        public void Rotation(float[][] rotationMatrix)
        {
            if (rotationMatrix == null)
                throw new ArgumentNullException("RotationMatrix Matrix cannot be null.");
            if (rotationMatrix.Length != 4 || rotationMatrix.Any(p => p.Length != 4))
                throw new ArgumentException("RotationMatrix Matrix must have dimension of 4 by 4.");
            float[] temp = new float[4];
            for (int i = 0; i < 4; i++)
                temp[i] = rotationMatrix[i][0] * X + rotationMatrix[i][1] * Y + rotationMatrix[i][2] * Z + rotationMatrix[i][3] * W;
            X = temp[0];
            Y = temp[1];
            Z = temp[2];
            W = temp[3];
        }
    }
    public class Point3D
    {
        public float X, Y, Z;
 
        public Point3D(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Point3D(float[] coordinate)
        {
            if (coordinate == null)
                throw new ArgumentNullException("Coordinate cannot be null.");
            if (coordinate.Length != 2)
                throw new ArgumentException("Coordinate must have length of 4.");
            X = coordinate[0];
            Y = coordinate[1];
            Z = coordinate[2];
        }

        public static float Distance(Point3D firstPoint, Point3D secondPoint)
        {
            return (float)Math.Sqrt(Math.Pow(firstPoint.X - secondPoint.X, 2) + Math.Pow(firstPoint.Y - secondPoint.Y, 2) + Math.Pow(firstPoint.Z - secondPoint.Z, 2));
        }

        public static Point3D Median(Point3D firstPoint, Point3D secondPoint)
        {
            return new Point3D((firstPoint.X + secondPoint.X)/2, (firstPoint.Y + secondPoint.Y) / 2, (firstPoint.Z + secondPoint.Z) / 2);
        }
    }
    public class Edge4D
    {
        public Point4D FirstPoint;
        public Point4D SecondPoint;

        public GameObject Cylinder;
        public GameObject gameObject = null;
        public Transform parent;
        public Edge4D(Point4D firstPoint, Point4D secondPoint, Transform parent, GameObject cylinder)
        {
            if (firstPoint == null || secondPoint == null)
                throw new ArgumentNullException("Points cannot be null");
            FirstPoint = firstPoint;
            SecondPoint = secondPoint;
            this.parent = parent;
            Cylinder = cylinder;
        }

        public void Draw()
        {
            if (gameObject == null)
            {
                gameObject = UnityEngine.Object.Instantiate(Cylinder);
                gameObject.transform.parent = parent;
            }
            Point3D midpoint = Point3D.Median(FirstPoint.ProjectedPoint, SecondPoint.ProjectedPoint);
            gameObject.transform.localPosition = new Vector3(midpoint.X, midpoint.Y, midpoint.Z);
            gameObject.transform.LookAt(FirstPoint.gameObject.transform);
            gameObject.transform.localScale = new Vector3(0.5f, 0.5f, Point3D.Distance(FirstPoint.ProjectedPoint, SecondPoint.ProjectedPoint) / 2);
        }
    }
}