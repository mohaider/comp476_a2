using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Assets.Script.AI.PathFinding;

public class LoadLookUpTable : MonoBehaviour
{
    private float[,] lookupTable;
    // Use this for initialization
    void Start()
    {
       
       
    
    }
    public void Load()
    {
        int counter = 0;
        string readString = "";
        List<List<int>> ints = new List<List<int>>();
        System.IO.StreamReader file = new System.IO.StreamReader("lookUpTable.csv");
        while ((readString = file.ReadLine()) != null)
        {
            if (counter == 0)
            {
                counter++;
                continue;
            }
            int firstComma = readString.IndexOf(",") + 1;
            string ss = readString.Substring(firstComma, readString.Length - firstComma);
            List<int> numbers = SplitStringIntoInts(readString.Substring(firstComma, readString.Length - firstComma));
            ints.Add(numbers);
            counter++;
        }

        file.Close();
        lookupTable = new float[counter - 1, counter - 1];

        for (int i = 0; i < counter - 1; i++)
        {
            for (int j = 0; j < counter - 1; j++)
            {
                lookupTable[i, j] = ints[i][j];
            }
        }

        CreateNeighborsForClusterNodes(counter, ints);

    }

    void CreateNeighborsForClusterNodes(int size, List<List<int>> neighborCosts)
    {
        AStarPathFinding astar = GetComponent<AStarPathFinding>();
        astar.LookUpTable = lookupTable;

        for (int i = 0; i < size - 1; i++)
        {
            List<ClusterNode> neighborList = new List<ClusterNode>();
            for (int j = 0; j < size - 1; j++)
            {
                ClusterNode neighbour = new ClusterNode();
                neighbour.ClusterName = astar.Grid.clusters[j].name;
                neighbour.Cluster = astar.Grid.clusters[j];
                neighbour.Id = j;
                neighbour.GCost = neighborCosts[i][j];
                neighborList.Add(neighbour);

                
            }
            astar.Grid.clusters[i].Node = new ClusterNode();
            
            astar.Grid.clusters[i].Node.ClusterName = astar.Grid.clusters[i].name;
            astar.Grid.clusters[i].node.Cluster = astar.Grid.clusters[i];
            astar.Grid.clusters[i].Node.Id = i;
            astar.Grid.clusters[i].Node.Neighbours = neighborList;
        }
    
    }
    List<int> SplitStringIntoInts(string list)
    {
        List<int> numbers = new List<int>();
        string[] split = list.Split(new char[1] { ',' });
        int parsed;
        foreach (string s in split)
        {
            if (int.TryParse(s, out parsed))
                numbers.Add(parsed);
        }
        return numbers;
    }
}
