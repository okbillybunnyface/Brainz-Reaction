using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class PoissonDisc
{
    private static System.Random random = new System.Random();

    //There must be at least one numPoints, and nothing may be negative
    public static Vector2[] Bridsons(Vector2 origin, float minDistance, int numPoints, int maxTries)
    {
        Queue<Vector2> activePoints = new Queue<Vector2>();
        Stack<Vector2> outputPoints = new Stack<Vector2>();

        Vector2 firstPoint = GenerateVector(origin, minDistance);
        activePoints.Enqueue(firstPoint);
        outputPoints.Push(firstPoint);

        while (activePoints.Count > 0 && outputPoints.Count < numPoints)
        {
            Vector2 activePoint = activePoints.Dequeue();
            bool success = false;
            do
            {
                int tries = 0;
                do
                {
                    tries++;
                    Vector2 candidate = GenerateVector(activePoint, minDistance);
                    success = true;
                    Vector2[] temp = outputPoints.ToArray();
                    foreach (Vector2 point in outputPoints)
                    {
                        if ((candidate - point).sqrMagnitude < minDistance * minDistance)
                        {
                            success = false;
                            break;
                        }
                    }
                    if (success)
                    {
                        activePoints.Enqueue(candidate);
                        outputPoints.Push(candidate);
                        break;
                    }
                }
                while (tries < maxTries);
            } 
            while (success);
        }

        return outputPoints.ToArray();
    }

    private static Vector2 GenerateVector(Vector2 origin, float minDistance)
    {
        float angle = (float)(random.NextDouble() * 2 * Math.PI);
        float distance = (float)((random.NextDouble() * minDistance) + minDistance);

        Vector2 output = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        output *= distance;
        output += origin;

        return output;
    }
}
