using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.AI.PathFinding
{
   
    public class Cluster : MonoBehaviour
    {
        #region class variables

        
        [Range(0, 250)]
        public float _clusterSizeX;
        [Range(0, 250)]
        public float _clusterSizeY;

        public Vector2 Xbounds;
        public Vector2 Zbounds;

        public bool showGizmos = true;

        public Color gridColor = Color.red;
        public bool isSubCluster;
        private List<PoVNodes> povNodes;
        public List<Node> _nodeList;
        public List<Node> _povNodeList; 

        public List<Node> NodeList
        {
            get { return _nodeList; }
            set { _nodeList = value; }
        }

        public List<Node> PovNodeList
        {
            get { return _povNodeList; }
            set { _povNodeList = value; }
        }

        #endregion
        #region unity functions

        void Awake()
        {
            Xbounds= new Vector2(transform.position.x- _clusterSizeX/2,transform.position.x + _clusterSizeX/2);
            Zbounds = new Vector2(transform.position.z - _clusterSizeY / 2, transform.position.z + _clusterSizeY / 2);
            _nodeList  = new List<Node>();
            _povNodeList = new List<Node>();

        }

        #endregion
        #region gizmos

        void OnDrawGizmos()
        {
            if (showGizmos)
            {
                Gizmos.color = gridColor;
              
                Gizmos.DrawCube(transform.position, new Vector3(_clusterSizeX, 10f, _clusterSizeY));

            }
        }

        #endregion
    }
}
