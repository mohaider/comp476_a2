using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Runtime.Remoting.Channels;
using System.Xml;

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

     //   public UnityEngine.UI.Text text;
        private float[,] lookUpTable;
        private LoadLookUpTable LookupTableLoader;
        public enum HeuristicType
        {
            EuclideanDistance = 0,
            nullHeuristic,
            clusterHeuristic
        }

        public UnityEngine.UI.Text HeuristicText;
        public enum PathFindingGraphType
        {
            GridSearch = 0,
            PoVSearch
        }
        public HeuristicType heuristicType = HeuristicType.EuclideanDistance;
        private bool usingGridSearch = false;
        public PathFindingGraphType pathType = PathFindingGraphType.GridSearch;
        private Grid grid; //the game's grid
        public PoVNodeGraph nodeGraph;
        private PathRequestManager requestmanager;

        public float[,] LookUpTable
        {
            get { return lookUpTable; }
            set { lookUpTable = value; }
        }

        public Grid Grid
        {
            get { return grid; }
            set { grid = value; }
        }

        /*        public Transform seeker;
                public Transform target;*/

        #endregion

        #region unity functions

        void Start()
        {
            HeuristicText.text = heuristicType.ToString();
        }
        void Awake()
        {
            grid = GetComponent<Grid>();
            nodeGraph = GetComponent<PoVNodeGraph>();
            requestmanager = GetComponent<PathRequestManager>();
            LookupTableLoader = GetComponent<LoadLookUpTable>();
            LookupTableLoader.Load();
        }

        #endregion

        #region class functions

        public void StartFindPath(Vector3 startPos, Vector3 endPos)
        {
            switch (pathType)
            {
                case PathFindingGraphType.GridSearch:
                    StartCoroutine
                (FindPath(startPos, endPos));
                    break;
                case PathFindingGraphType.PoVSearch:
                    StartCoroutine(FindPoVPath(startPos, endPos));
                    break;
            }
        }



        private IEnumerator FindPoVPath(Vector3 startPos, Vector3 targetPos)
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
            Node startNode = nodeGraph.QuantizePosition(startPos); // get node relative to the startPos

            Node targetNode = nodeGraph.QuantizePosition(targetPos); //get target node relative to the start pos


            Vector3[] wayPoints = new Vector3[0];
            bool pathSuccess = false;

            // if (startNode.IsWalkable && targetNode.IsWalkable)

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


                    pathSuccess = true;
                    break; //exit out of the loop
                }

                //     foreach neighbour of the current node
                //     if neighbour is not traversable or is in CLOSED
                //        skip to the next neighbour
                foreach (Node neighbour in nodeGraph.GetNeighbours(currentNode))
                {
                    if (closedSet.Contains(neighbour))
                        continue;
                    int newMovementCostToNeighbour = currentNode.GCost + GetDistance(currentNode, neighbour, heuristicType);
                    if (newMovementCostToNeighbour < neighbour.GCost || !openHeap.Contains(neighbour))
                    {
                        neighbour.GCost = newMovementCostToNeighbour;
                        neighbour.HCost = GetDistance(neighbour, targetNode, heuristicType);
                        neighbour.ParentNode = currentNode;
                        if (!openHeap.Contains(neighbour))
                            openHeap.Add(neighbour);
                        else openHeap.UpdateItem(neighbour);
                    }
                }
            }



            yield return null;
            if (pathSuccess)
            {
                wayPoints = RetracePath(startNode, targetNode);
            }

            requestmanager.FinishProcessingPath(wayPoints, pathSuccess);
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
          //  text.text = "Start node info "+startNode.ToString()+"\n target node "+targetNode.ToString();

            Vector3[] wayPoints = new Vector3[0];
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
                        int newMovementCostToNeighbour = currentNode.GCost + GetDistance(currentNode, neighbour, heuristicType);
                        if (newMovementCostToNeighbour < neighbour.GCost || !openHeap.Contains(neighbour))
                        {
                            neighbour.GCost = newMovementCostToNeighbour;
                            neighbour.HCost = GetDistance(neighbour, targetNode, heuristicType);
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
           
            requestmanager.FinishProcessingPath(wayPoints, pathSuccess);

        }


        Vector3[] RetracePath(Node startNode, Node endNode)
        {
            List<Node> path = new List<Node>();
            Node currenNode = endNode;

            while (currenNode != startNode)
            {
                path.Add(currenNode);
                currenNode = currenNode.ParentNode;
            }
            if (path.Count == 1)
            {
                path.Add(startNode);
            }
            Vector3[] wayPoints = SimplifyPath(path);
            Array.Reverse(wayPoints);
            //grid.Path = path;
            return wayPoints;
        }

        private Vector3[] SimplifyPath(List<Node> path)
        {
            List<Vector3> wayPoints = new List<Vector3>();
            Vector2 directionOld = Vector2.zero;
            switch (pathType)
            {
                case AStarPathFinding.PathFindingGraphType.GridSearch:
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
                    break;
                case AStarPathFinding.PathFindingGraphType.PoVSearch:
                    for (int i = 0; i < path.Count; i++)
                    {
                        wayPoints.Add(path[i].WorldPosition);
                    }
                    break;

            }

            return wayPoints.ToArray();
        }

        //diagonal moves cost 14
        //horizonal moves cost 10
        public  int GetDistance(Node A, Node B, HeuristicType heuristicType)
        {
            int returner = 0;
            switch (heuristicType)
            {
                case HeuristicType.EuclideanDistance:


                    returner =ComputeEuclideanDistance(A, B);

                    break;

                case HeuristicType.nullHeuristic:
                    
                    break;
                case HeuristicType.clusterHeuristic:
                  
                    int indexA = A._inCluster.Node.Id;
                    int indexB = B._inCluster.node.Id;
                    if (indexA == indexB)
                        returner = ComputeEuclideanDistance(A, B);
                    else
                        returner = (int)lookUpTable[indexA, indexB];
                    
                    
                    break;
            }
            return returner;
        }

       public static int ComputeEuclideanDistance(Node A, Node B)
        {
            int returner = 0;
           int distanceX = Mathf.Abs(A.PositionX - B.PositionX);
           int distanceY = Mathf.Abs(A.PositionY - B.PositionY);
           if (distanceX > distanceY)
               return 14*distanceY + 10*(distanceX - distanceY);
           else
               return 14*distanceX + 10*(distanceY - distanceX);
            Vector2 posA = new Vector2(A.WorldPosition.x, A.WorldPosition.z);
            Vector2 posB = new Vector2(B.WorldPosition.x, B.WorldPosition.z);
            //float distance = Vector2.Distance(posA, posB);
            float distance = Vector2.SqrMagnitude(posB - posA);
            returner = (int)distance;
           return returner;
           
        }

        public void ToggleGridOrPoV()
        {
            usingGridSearch = !usingGridSearch;

            if(usingGridSearch)
                pathType = PathFindingGraphType.GridSearch;
            else 
                pathType = PathFindingGraphType.PoVSearch;
            
        }

        public void ToggleBetweenHeuristic()
        {
            int currentEnumVal = (int) heuristicType;
            if (currentEnumVal == Enum.GetNames(typeof (HeuristicType)).Length - 1)
            {
                currentEnumVal = 0;
            }
            else
            {
                currentEnumVal++;
            }
            heuristicType = (HeuristicType)currentEnumVal;
            HeuristicText.text = heuristicType.ToString();
        }

        #endregion

    }
}
