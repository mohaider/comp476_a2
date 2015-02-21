﻿using UnityEngine;
using System.Collections.Generic; //to use a queue to store all the requests
using System;
using Assets.Script.AI.PathFinding;

public class PathRequestManager : MonoBehaviour
{

    #region class function
    private Queue<PathRequest> pathrequestQueue = new Queue<PathRequest>();
    private PathRequest currentPathRequest;
    private static PathRequestManager instance;
    private AStarPathFinding pathFinding;
    private bool isProcessing;

    /// <summary>
    /// takes in a vector3 for start, end and an action
    /// will be called from a unit class. The response won't be given immediately, the requests will be 
    /// spread out over a number of frames. IT is instead going to be passed in a method and will be called once a path is calculated
    /// the callback indicates if the action has been succesfull
    /// </summary>
    /// <param name="pathStart"></param>
    /// <param name="pathEnd"></param>
    /// <param name="?"></param>
    public static void RequestPath(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callback) 
    {
        //need access information, so we create a static variable
        PathRequest newRequest = new PathRequest(pathStart,pathEnd, callback);
        instance.pathrequestQueue.Enqueue(newRequest);
        instance.TryNextProcess();

    }

    /// <summary>
    /// if we are not processing a path, process the next one
    /// </summary>
    void TryNextProcess()
    {
        if (!isProcessing && pathrequestQueue.Count > 0)
        {
            currentPathRequest = pathrequestQueue.Dequeue(); //dequeue pathrequest
            isProcessing = true;  //now processing
            pathFinding.StartFindPath(currentPathRequest.pathStart, currentPathRequest.pathEnd);

        }
    }

    public void FinishProcessingPath(Vector3[] path, bool success)
    {
        currentPathRequest.callback(path, success);
        isProcessing = false;
        TryNextProcess();
    }


    struct PathRequest
    {
        public Vector3 pathStart;
        public Vector3 pathEnd;
        public Action<Vector3[], bool> callback;

        public PathRequest(Vector3 _start, Vector3 _end, Action<Vector3[], bool> _callback)
        {
            pathStart = _start;
            pathEnd = _end;
            callback = _callback;
        }
    }
    #endregion

    #region unity functions

    private void Awake()
    {
        instance = this;
        pathFinding = GetComponent<AStarPathFinding>();
    }

    #endregion


}
