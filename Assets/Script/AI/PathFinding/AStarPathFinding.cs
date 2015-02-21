using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System;

namespace Assets.Script.AI.PathFinding
{
    /**
 * Code written by Mohammed Haider
 * If you have any questions about this code, feel free to email me
 * mrhaider@gmail.com 
 */
    public class AStarPathFinding : MonoBehaviour
    {
        #region class variables and properties

       
        private Grid grid; //the game's grid
        private PathRequestManager requestmanager;
/*        public Transform seeker;
        public Transform target;*/

        #endregion

        #region unity functions

        void Awake()
        {
            grid = GetComponent<Grid>();
           requestmanager = GetComponent<PathRequestManager>();
        }

        #endregion

        #region class functions

        public void StartFindPath(Vector3 startPos, Vector3 endPos)
        {
            StartCoroutine(FindPath(startPos, endPos));
        }
        void Update()
        {
           // if (Input.GetButtonDown("Jump")) 
              //  FindPath(seeker.position,target.position);
        }
        /// <summary>
        /// this is an implementation of the A* path finding algorithm. 
        /// </summary>
        /// <param name="startPos"></param>
        /// <param name="targetPos"></param>
        IEnumerator FindPath(Vector3 startPos, Vector3 targetPos)
        {
            // OPEN //the set of nodes that need to be evaluated in the openlist
            // CLOSED // the set of nodes already evaluated
            // loop
            //     current = node in OPEN with the lowest fCost
            //    remove current from open 
            //     add current to CLOSED
            //     
            //     if current is the target node //path has been found
            //        return
            //    foreach neighbour of the current node
            //    if neighbour is not traversable or is in CLOSED
            //         skip to the next neighbour
            // 
            //     if new path to neighbor is shorter OR neighbour is not in OPEN
            //         SET FCost of neighbour
            //         SET parent of neighbour to current
            //         if neighbour is not in open
            //             add neighbor to open 
            //
            Node startNode = grid.QuantizePosition(startPos); // get node relative to the startPos
            Node targetNode = grid.QuantizePosition(targetPos); //get target node relative to the start pos

            Stopwatch sw = new Stopwatch();
            sw.Start();
            Vector3[] wayPoints = new Vector3[0] ;
            bool pathSuccess = false;

            if (startNode.IsWalkable && targetNode.IsWalkable)
            { 
            //according to the A* algorithm, we need an open list and closed list
          // List<Node> openList = new List<Node>();
            Heap<Node> openHeap = new Heap<Node>(grid.MaxSize);
            HashSet<Node> closedSet = new HashSet<Node>();//we need to be able to check if the list contains a specific node, so a hashset or dictionary set should suffice
            openHeap.Add(startNode);
           // openList.Add(startNode);


                while (openHeap.Count > 0)
                {
                    //Node currentNode = openList[0];

                    /*  for (int i = 1; i < openList.Count; i++) 
                {

                    if (openList[i].FCost < currentNode.FCost || openList[i].FCost == currentNode.FCost && openList[i].HCost < currentNode.HCost)
                    {
                        currentNode = openList[i];
                    }
                    openList.Remove(currentNode);
                    closedSet.Add(currentNode);
                }*/

                    Node currentNode = openHeap.RemoveFirstItem();
                    closedSet.Add(currentNode);
                    if (currentNode == targetNode) //path has been found
                    {
                        sw.Stop();
                        print("total time with unoptimaized path finding is " + sw.ElapsedMilliseconds + "ms");

                        pathSuccess = true;
                        break; //exit out of the loop
                    }

                    //     foreach neighbour of the current node
                    //     if neighbour is not traversable or is in CLOSED
                    //        skip to the next neighbour
                    foreach (Node neighbour in grid.GetNeighbours(currentNode))
                    {
                        if (!neighbour.IsWalkable || closedSet.Contains(neighbour))
                            continue;
                        int newMovementCostToNeighbour = currentNode.GCost + GetDistance(currentNode, neighbour);
                        if (newMovementCostToNeighbour < neighbour.GCost || !openHeap.Contains(neighbour))
                        {
                            neighbour.GCost = newMovementCostToNeighbour;
                            neighbour.HCost = GetDistance(neighbour, targetNode);
                            neighbour.ParentNode = currentNode;
                            if (!openHeap.Contains(neighbour))
                                openHeap.Add(neighbour);
                            else openHeap.UpdateItem(neighbour);
                        }
                    }
                }


            }
            yield return null;
            if (pathSuccess)
            {
                wayPoints = RetracePath(startNode, targetNode);
            }

            requestmanager.FinishProcessingPath(wayPoints,pathSuccess);
            
        }

        //retrace our steps

        Vector3[] RetracePath(Node startNode, Node endNode)
        {
            List<Node> path = new List<Node>();
            Node currenNode = endNode;

            while (currenNode != startNode)
            {
                path.Add(currenNode);
                currenNode = currenNode.ParentNode;
            }
            Vector3[] wayPoints = SimplifyPath(path);
            Array.Reverse(wayPoints);
            //grid.Path = path;
            return wayPoints;
        }

        Vector3[] SimplifyPath(List<Node> path)
        {
            List<Vector3> wayPoints = new List<Vector3>();
            Vector2 directionOld = Vector2.zero;
            for (int i = 1; i < path.Count; i++)
            {
                Vector2 directionNew = new Vector2(path[i - 1].PositionX - path[i].PositionX, 
                    path[i - 1].PositionY - path[i].PositionY);
                if (directionNew != directionOld)
                {
                    wayPoints.Add(path[i].WorldPosition);
                }
                directionOld = directionNew;
            }
            return wayPoints.ToArray();
        }

        //diagonal moves cost 14
        //horizonal moves cost 10
        int GetDistance(Node A, Node B)
        {
            int distX = Mathf.Abs(A.PositionX - B.PositionX);
            int distY = Mathf.Abs(A.PositionY - B.PositionY);

            if (distX > distY)
                return 14 * distY + 10 * (distX - distY);
            else
            {
                return 14 * distX + 10 * (distY - distX);
            }
        }
        #endregion

    }
}
