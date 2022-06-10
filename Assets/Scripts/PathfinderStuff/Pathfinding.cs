using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#if UNITY_EDITOR
using System.Diagnostics;
#endif

public class Pathfinding : MonoBehaviour
{
    Grid grid;

    private void Awake()
    {
        grid = GetComponent<Grid>();
    }

    public void findPath(pathRequest request, Action<pathResult> callback)
    {
#if UNITY_EDITOR
        Stopwatch sw = new Stopwatch();
        sw.Start();
#endif
        Vector3[] wayPoints = new Vector3[0];
        bool pathSuccess = false;

        Node startNode = grid.nodeFromWorldPoint(request.pathStart);
        Node targetNode = grid.nodeFromWorldPoint(request.pathEnd);

        if (startNode.walkable && targetNode.walkable)
        {
            Heap<Node> openSet = new Heap<Node>(grid.maxSize);
            HashSet<Node> closedSet = new HashSet<Node>();
            openSet.Add(startNode);
            while (openSet.Count > 0)
            {
                Node currentNode = openSet.removeFirst();

                closedSet.Add(currentNode);

                if (currentNode == targetNode)
                {
#if UNITY_EDITOR
                    sw.Stop();
                    print("Path found in : " + sw.ElapsedMilliseconds + " ms");
#endif
                    pathSuccess = true;
                    break;
                }

                foreach (Node neightbour in grid.GetNeightbours(currentNode))
                {
                    if (!neightbour.walkable || closedSet.Contains(neightbour))
                    {
                        continue;
                    }

                    int newMovementCostToNeightbour = currentNode.gCost + getDistance(currentNode, neightbour) + neightbour.movementPenalty;
                    if (newMovementCostToNeightbour < neightbour.gCost || !openSet.Contains(neightbour))
                    {
                        neightbour.gCost = newMovementCostToNeightbour;
                        neightbour.hCost = getDistance(neightbour, targetNode);
                        neightbour.parent = currentNode;

                        if (!openSet.Contains(neightbour))
                        {
                            openSet.Add(neightbour);
                        }
                        else
                            openSet.updateItem(neightbour);
                    }
                }
            }
        }
        if (pathSuccess)
        {
            wayPoints = retracePath(startNode, targetNode);
            pathSuccess = wayPoints.Length > 0;
        }
        callback(new pathResult(wayPoints, pathSuccess, request.callback));
    }

    Vector3[] retracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        Vector3[] waypoints = simplifyPath(path);
        Array.Reverse(waypoints);
        return waypoints;
    }

    Vector3[] simplifyPath(List<Node> path)
    {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;

        for (int i = 1; i < path.Count; i++)
        {
            Vector2 directionNew = new Vector2(path[i - 1].GridX - path[i].GridX, path[i - 1].GridY - path[i].GridY);
            if (directionNew != directionOld)
            {
                waypoints.Add(path[i - 1].worldPosition);
            }
            directionOld = directionNew;
        }
        return waypoints.ToArray();
    }

    int getDistance(Node nodeA, Node nodeB)
    {
        int distX = Mathf.Abs(nodeA.GridX - nodeB.GridX);
        int distY = Mathf.Abs(nodeA.GridY - nodeB.GridY);

        if (distX > distY)
        {
            return 14 * distY + 10 * (distX - distY);
        }
        return 14 * distX + 10 * (distY - distX);
    }
}
