using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Script.AI.PathFinding;

public class CreateClusterLookUpTable : MonoBehaviour
{
    public bool startCreation = false;
    public bool showAllClusterGizmos;

    public Cluster[] clusters;
    public Grid grid;
    private float[,] lookupTable;
	// Use this for initialization
	void Start ()
	{
	     grid = GetComponent<Grid>();
	    clusters=grid.clusters;
	
	        ConstructLookupTables();
        if(startCreation)
    	    WriteLookupTable();

	}

    private void WriteLookupTable()
    {
        string output = ",";

        System.IO.StreamWriter file = new System.IO.StreamWriter("lookUpTable.csv");
        for (int x = 0; x < clusters.Length; x++)
        {
            output += clusters[x].name;
            if (x < clusters.Length - 1)
                output += ",";
            else output += "\r\n";
        }
        for (int i = 0; i < lookupTable.GetLength(0); i++)
        {
           output += clusters[i].name+",";
            for (int j = 0; j < lookupTable.GetLength(1); j++)
            {
                output += lookupTable[i,j];
                if (j < lookupTable.GetLength(1) - 1)
                {
                    output += ",";
                }
               
            }
            if (i < lookupTable.GetLength(0) - 1)
                output += "\r\n"; //newline
        }
        file.WriteLine(output);
        file.Close();
    }
	
	// Update is called once per frame
	void Update () {
        foreach (Cluster c in clusters)
        {
            c.showGizmos = showAllClusterGizmos;
        }
	}

    internal void ConstructLookupTables()
    {
         lookupTable = new float[clusters.Length,clusters.Length];

       /* for (int i = 0; i < clusters.Length-1; i++)//clusters.Count()-1
        {
            float minDistance = Mathf.Infinity;
            float distanceToCompare = 0;
            try
            {
               
                for (int j = 0; j < clusters[i]._nodeList.Count; j ++){
                    Node currentNode = clusters[i].NodeList[j];
                    if (!currentNode.isExitNode || !currentNode.IsWalkable){
                        continue;
                    }
                    for (int k = 0; k < clusters[i + 1]._nodeList.Count; k++){
                        Node ComparisonNode = clusters[i + 1]._nodeList[k];
                        if (!ComparisonNode.IsWalkable){
                            continue;
                        }
                        //find distance
                        Vector2 A = new Vector2(currentNode.WorldPosition.x,currentNode.WorldPosition.z);
                        Vector2 B = new Vector2(ComparisonNode.WorldPosition.x,currentNode.WorldPosition.z);
                        distanceToCompare = Vector2.Distance(A, B);
                        if (distanceToCompare < minDistance){
                            minDistance = distanceToCompare;
                            lookupTable[i, i + 1] = minDistance;
                        } 
                    }

                }
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }

           
        }*/
        for (int i = 0; i < clusters.Length - 1; i++) //clusters.Count()-1
        {
          
            for (int j = 0; j < clusters[i].NodeList.Count; j++)
            {
                Node currentNode = clusters[i].NodeList[j];
                Node ComparisonNode;
                 if (!currentNode.isExitNode || !currentNode.IsWalkable){
                        continue;
                    }
                for (int k = i+1; k < clusters.Length; k++)
                {
                    float minDistance = Mathf.Infinity;
                    float distanceToCompare = 0;
                    for (int l = 0; l < clusters[k].NodeList.Count; i++)
                    {
                 
                        ComparisonNode = clusters[k]._nodeList[l];
                        if (!ComparisonNode.IsWalkable || !ComparisonNode.isExitNode)
                            continue;
                        Vector2 A = new Vector2(currentNode.WorldPosition.x, currentNode.WorldPosition.z);
                        Vector2 B = new Vector2(ComparisonNode.WorldPosition.x, currentNode.WorldPosition.z);
                        distanceToCompare = Vector2.Distance(A, B);
                        if (distanceToCompare < minDistance)
                        {
                            minDistance = distanceToCompare;
                            lookupTable[i,k] = minDistance;
                        } 
                    }
                }
            }
        }
    }

    internal void Djikstras(Node startNode, Node targetNode, out float distance)
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
      
        //Node startNode = grid.QuantizePosition(startPos); // get node relative to the startPos
        //Node targetNode = grid.QuantizePosition(targetPos); //get target node relative to the start pos


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
                    int newMovementCostToNeighbour = AStarPathFinding.GetDistance(currentNode, neighbour, AStarPathFinding.HeuristicType.EuclideanDistance);
                    if (newMovementCostToNeighbour < neighbour.GCost || !openHeap.Contains(neighbour))
                    {
                        neighbour.GCost = newMovementCostToNeighbour;
                       // neighbour.HCost = AStarPathFinding.GetDistance(neighbour, targetNode,AStarPathFinding.HeuristicType.EuclideanDistance);
                        neighbour.ParentNode = currentNode;
                        if (!openHeap.Contains(neighbour))
                            openHeap.Add(neighbour);
                        else openHeap.UpdateItem(neighbour);
                    }
                }
            }


        }

        if (pathSuccess)
        {
            distance = computeTotalPathDistance(startNode, targetNode);
        }
        else
            distance = Mathf.Infinity;
    }

    private float computeTotalPathDistance(Node startNode, Node endNode)
    {
        Node currenNode = endNode;
        float totalDistance = 0;
        while (currenNode != startNode)
        {
            totalDistance += AStarPathFinding.GetDistance(currenNode, currenNode.ParentNode,
                AStarPathFinding.HeuristicType.EuclideanDistance);
            currenNode = currenNode.ParentNode;
        }

        return totalDistance;
    }


}
