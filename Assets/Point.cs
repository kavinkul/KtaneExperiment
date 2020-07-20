using System;
using System.Linq;
using UnityEngine;

namespace Experiment4
{
    public class Point4D
    {
        public float X, Y, Z, W;

        public Point3D ProjectedPoint;

        public GameObject gameObject;
        public Point4D(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public Point4D(float[] coordinate)
        {
            if (coordinate == null)
                throw new ArgumentNullException("Coordinate cannot be null.");
            if (coordinate.Length != 4)
                throw new ArgumentException("Coordinate must have length of 4.");
            X = coordinate[0];
            Y = coordinate[1];
            Z = coordinate[2];
            W = coordinate[3];
        }

        public void Projection(float[][] projectionMatrix)
        {
            if (projectionMatrix == null)
                throw new ArgumentNullException("Projection Matrix cannot be null.");
            if (projectionMatrix.Length != 3 || projectionMatrix.Any(p => p.Length != 4))
                throw new ArgumentException("Projection Matrix must have dimension of 3 by 4.");
            ProjectedPoint = new Point3D(projectionMatrix[0][0] * X + projectionMatrix[0][1] * Y + projectionMatrix[0][2] * Z + projectionMatrix[0][3] * W,
                projectionMatrix[1][0] * X + projectionMatrix[1][1] * Y + projectionMatrix[1][2] * Z + projectionMatrix[1][3] * W,
                projectionMatrix[2][0] * X + projectionMatrix[2][1] * Y + projectionMatrix[2][2] * Z + projectionMatrix[2][3] * W);
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

        public Edge4D(Point4D firstPoint, Point4D secondPoint)
        {
            if (firstPoint == null || secondPoint == null)
                throw new ArgumentNullException("Points cannot be null");
            FirstPoint = firstPoint;
            SecondPoint = secondPoint;
        }
    }
}