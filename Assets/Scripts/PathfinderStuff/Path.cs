﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path
{
    public readonly Vector3[] lookPoints;
    public readonly Line[] turnBoundaries;
    public readonly int finishLineIndex;

    public Path(Vector3[] waypoints, Vector3 startPos, float turnDist)
    {
        lookPoints = waypoints;
        turnBoundaries = new Line[lookPoints.Length];
        finishLineIndex = turnBoundaries.Length - 1;

        Vector2 prevPoint = V3toV2(startPos);
        for (int i = 0; i < lookPoints.Length; i++)
        {
            Vector2 currPoint = V3toV2(lookPoints[i]);
            Vector2 dirToCurrPoint = (currPoint - prevPoint).normalized;
            Vector2 turnBoundaryPoint = (i == finishLineIndex) ? currPoint : currPoint - dirToCurrPoint * turnDist;
            turnBoundaries[i] = new Line(turnBoundaryPoint, prevPoint - dirToCurrPoint * turnDist);
            prevPoint = turnBoundaryPoint;
        }
    }

    Vector3 V3toV2(Vector3 v3)
    {
        return new Vector2(v3.x, v3.z);
    }

    public void DrawWithGizmos()
    {
        Gizmos.color = Color.black;
        foreach (Vector3 p in lookPoints)
        {
            Gizmos.DrawCube(p + Vector3.up, Vector3.one);
        }

        Gizmos.color = Color.white;

        foreach (Line l in turnBoundaries)
        {
            l.DrawWithGizmos(10);
        }
    }
}
