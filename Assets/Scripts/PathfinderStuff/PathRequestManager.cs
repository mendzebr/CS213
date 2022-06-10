using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;

public class PathRequestManager : MonoBehaviour
{
    Queue<pathResult> results = new Queue<pathResult>();

    static PathRequestManager instance;
    Pathfinding pathFinding;

    private void Awake()
    {
        instance = this;
        pathFinding = GetComponent<Pathfinding>();
    }

    private void Update()
    {
        if (results.Count > 0)
        {
            int itemsInQueue = results.Count;
            lock (results)
            {
                for (int i = 0; i < itemsInQueue; i++)
                {
                    pathResult result = results.Dequeue();
                    result.callback(result.path, result.suucess);
                }
            }
        }
    }

    public static void requestPath(pathRequest request)
    {
        ThreadStart threadStart = delegate { instance.pathFinding.findPath(request, instance.finishedProcessingPath); };
        threadStart.Invoke();
    }

    public void finishedProcessingPath(pathResult result)
    {
        lock (results)
        {
            results.Enqueue(result);
        }
    }
}

public struct pathResult
{
    public Vector3[] path;
    public bool suucess;
    public Action<Vector3[], bool> callback;

    public pathResult(Vector3[] path, bool suucess, Action<Vector3[], bool> callback)
    {
        this.path = path;
        this.suucess = suucess;
        this.callback = callback;
    }
}

public struct pathRequest
{
    public Vector3 pathStart;
    public Vector3 pathEnd;
    public Action<Vector3[], bool> callback;

    public pathRequest(Vector3 _start, Vector3 _end, Action<Vector3[], bool> _callback)
    {
        pathStart = _start;
        pathEnd = _end;
        callback = _callback;
    }
}
