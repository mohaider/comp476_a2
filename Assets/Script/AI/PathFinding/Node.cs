using System;
using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
namespace Assets.Script.AI.PathFinding
{
    [Serializable]
    public class Node : IHeapItem<Node>
    {
        public Node()
        {
        }

        #region class variables and properties

        private int _id;
        private bool _isWalkable;
        private Node _parentNode;
     
        public Vector3 _worldPosition;
        private int _gCost;
         private int _hCost;
        private int _positionX;
        public string _clusterGroup;
        public Cluster _inCluster;
        private int _positionY;
        private int _heapIndex;
        private PoVNodes _associatedPovNode;
        public bool isExitNode;
        public bool IsWalkable
        {
            get { return _isWalkable; }
            set { _isWalkable = value; }
        }

        public Vector3 WorldPosition
        {
            get { return _worldPosition; }
            set { _worldPosition = value; }
        }

        public int GCost
        {
            get { return _gCost; }
            set { _gCost = value; }
        }

        public int HCost
        {
            get { return _hCost; }
            set { _hCost = value; }
        }

        public int FCost
        {
            get { return _gCost + _hCost; }
        }

        public int PositionX
        {
            get { return _positionX; }
            set { _positionX = value; }
        }

        public int PositionY
        {
            get { return _positionY; }
            set { _positionY = value; }
        }

        public Node ParentNode
        {
            get { return _parentNode; }
            set { _parentNode = value; }
        }
       
        public int HeapIndex
        {
            get { return _heapIndex; }
            set { _heapIndex = value; }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public PoVNodes AssociatedPovNode
        {
            get { return _associatedPovNode; }
            set { _associatedPovNode = value; }
        }

        #endregion

        #region constructor

        public Node(bool isWalkable, Vector3 worldPosition, int xPos, int yPos)
        {
            _isWalkable = isWalkable;
            _worldPosition = worldPosition;
            _positionX = xPos;
            _positionY = yPos;
        }



        #endregion

        #region class functions


        public int CompareTo(Node nodeA)
        {
            int comparer = 0;

            comparer = FCost.CompareTo(nodeA.FCost);
            if (comparer == 0)
            {
                comparer = HCost.CompareTo(nodeA.HCost);
            }
            return -comparer;
        }

        #endregion

        #region class functions

        public int CompareTo(object obj)
        {
            int comparer = 0;
            Node NodeToCompare = obj as Node;
            comparer = FCost.CompareTo(NodeToCompare.FCost);
            if (comparer == 0)
            {
                comparer = HCost.CompareTo(NodeToCompare.HCost);
            }
            return -comparer;
        }

   
        #endregion

      

    }
}