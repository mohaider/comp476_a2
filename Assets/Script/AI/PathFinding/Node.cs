using UnityEngine;
using System.Collections;

namespace Assets.Script.AI.PathFinding
{
    public class Node : IHeapItem<Node>
    {
        #region class variables and properties

        private bool _isWalkable;
        private Node _parentNode;
        private Vector3 _worldPosition;
        [SerializeField] private int _gCost;
        [SerializeField] private int _hCost;

        [SerializeField] private int _positionX;
        [SerializeField] private int _positionY;

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

        private int _heapIndex;
        public int HeapIndex
        {
            get { return _heapIndex; }
            set { _heapIndex = value; }
        }

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
    }
}