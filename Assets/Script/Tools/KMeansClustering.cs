using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Script.AI.PathFinding;
using UnityEngine;

namespace Assets.Script.Tools
{
    
    /// <summary>
    /// using Lloyd's algorithm , this class is an implementation of the Kmeans algorithm 
    /// this algorithm will cluster sets of data according to a specified number of clusters. This number must be specified 
    /// </summary>
    public class KMeansClustering
    {
        #region class variables
        private Node[,] _rawData;
        private int _clusterSize;
        private int[] _clusteredData;
        #endregion

        #region properties with getters and setters
        public Node[,] RawData
        {
            get { return _rawData; }
            set { _rawData = value; }
        }

        public int ClusterSize
        {
            get { return _clusterSize; }
            set { _clusterSize = value; }
        }

        #endregion
        #region constructor

        public KMeansClustering(int clusterSize, Node[,] dataNodes)
        {
            if (clusterSize==0)
                throw new Exception("tried to pass in zero for the number of clusters");
            this._clusterSize = clusterSize;
            this._rawData = dataNodes;
        }



        #endregion

        #region class functions

      /*  public static Node[] Cluster(KMeansClustering cluster)
        {
            //kmeans clustering, index to return is tuple id, cell is cluster id
            //the id through nodes is generally their positions in the node[][] cell

           //first we normalize the data

//            Node[,] normalizedNodeData = Normalized(cluster._rawData);
            return null;

        }*/
        /// <summary>
        /// The kmeans algorithm requires the data to be normalized
        /// this is computed by (x - means)/ standardDev
        /// alternatively, is min = max where v' = (v-min)/(max-min);
        /// </summary>
        /// <param name="nodeData"></param>
        /// <returns></returns>
       /* public static Node[,] Normalized(Node[,] nodeData)
        {
            //deep copy of the data
            Node[,] result = nodeData.Clone() as Node[,] ;
          

        /*    for (int i = 0; i < nodeData.Length; i++)
            {
                result[i] = new Vector3[nodeData.GetLength(1)] ;
                Array.Copy(nodeData[i]);
            }#1#

d            for (int j = 0; j < result.GetLength(0); j++)
            {
                for (int i = 0; i < result.GetLength(1); i++)
                {
                    
                }
            }
        }*/
        #endregion 
    }
}
