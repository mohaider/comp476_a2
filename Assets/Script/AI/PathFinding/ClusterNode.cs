

using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script.AI.PathFinding
{
   public  class ClusterNode:Node
    {
        #region class variables and properties
        private Cluster _cluster;
        private string _clusterName;
       [SerializeField]
        public List<ClusterNode> _neighbours;


        public Cluster Cluster
        {
            get { return _cluster; }
            set { _cluster = value; }
        }

        public string ClusterName
        {
            get { return _clusterName; }
            set { _clusterName = value; }
        }

        public List<ClusterNode> Neighbours
        {
            get { return _neighbours; }
            set { _neighbours = value; }
        }

        #endregion
    }
}
