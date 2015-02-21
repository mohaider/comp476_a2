using System;
using UnityEngine;
using System.Collections.Generic;

namespace Assets.Script.AI.PathFinding
{/**
 * Code written by Mohammed Haider
 * If you have any questions about this code, feel free to email me
 * mrhaider@gmail.com 
 */
    public class Grid : MonoBehaviour
    {
        public bool displayGridGizmos ;
        #region class variables and properties
        [SerializeField]
        private bool ShowAllGizmos = false;
        Node[,] grid;
        private List<Node> _path;

        [SerializeField]
        private Vector2 worldSize; //our area in the world
        [SerializeField]
        private Transform _player; //position of the player in the world

        [SerializeField]
        private float _nodeDiameter; //diameter of the node. 

        [SerializeField]
        private float _nodeRadius;

        [SerializeField]
        private LayerMask UnwalkableMask;

        [SerializeField]
        private int GridSizeX, GridSizeY;

        public List<Node> Path
        {
            get { return _path; }
            set { _path = value; }
        }

        public int MaxSize
        {
            get { return GridSizeX * GridSizeY; }

        }

        public float NodeDiameter
        {
            get { return _nodeDiameter; }
            set { _nodeDiameter = value; }
        }

        public float NodeRadius
        {
            get { return _nodeRadius; }
            set { _nodeRadius = value; }
        }

        [SerializeField]
        private int _maxSize;

        #endregion
        #region UnityFunctions

        void Awake()
        {
            _nodeDiameter = _nodeRadius * 2;
            GridSizeX = Mathf.RoundToInt(worldSize.x / _nodeDiameter);
            GridSizeY = Mathf.RoundToInt(worldSize.y / _nodeDiameter);
            CreateGrid();
        }





        #endregion
        #region Class functions

        void CreateGrid()
        {
            grid = new Node[GridSizeX, GridSizeY];
            Vector3 worldBottomLeft = transform.position - Vector3.right * worldSize.x / 2 - Vector3.forward * worldSize.y / 2;

            //set the grid

            for (int x = 0; x < GridSizeX; x++)
            {
                for (int y = 0; y < GridSizeY; y++)
                {
                    Vector3 worldPosition = worldBottomLeft + Vector3.right * (x * _nodeDiameter + _nodeRadius) +
                                            Vector3.forward * (y * _nodeDiameter + _nodeRadius);
                    //check to see if current position is walkable
                    bool isWalkable = !Physics.CheckSphere(worldPosition, _nodeRadius, UnwalkableMask);
                    grid[x, y] = new Node(isWalkable, worldPosition, x, y);
                }
            }




        }

        public Node QuantizePosition(Vector3 worldPosition)
        {
            float percentX = (worldPosition.x - transform.position.x + worldSize.x / 2) / worldSize.x;
            float percentY = (worldPosition.z - transform.position.z + worldSize.y / 2) / worldSize.y;

            percentX = Mathf.Clamp01(percentX);
            percentY = Mathf.Clamp01(percentY);

            int x = Mathf.RoundToInt((GridSizeX - 1) * percentX);
            int y = Mathf.RoundToInt((GridSizeY - 1) * percentY);

            return grid[x, y];
        }

        public List<Node> GetNeighbours(Node n)
        {
            //where is this node in the grid?
            List<Node> neighbours = new List<Node>();

            //this will search in a 3X3 block
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0)
                        continue; //we're at the current node

                    int checkX = n.PositionX + x;
                    int checkY = n.PositionY + y;

                    if (checkX >= 0 && checkX < GridSizeX && checkY >= 0 && checkY < GridSizeY)
                    {
                        neighbours.Add(grid[checkX, checkY]);
                    }
                }

            }
            return neighbours;

        }


        #endregion


        #region gizmos

        void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position, new Vector3(worldSize.x, 1f, worldSize.y));

            if (grid != null && displayGridGizmos==true)
            {

                foreach (Node n in grid)
                {
                    Gizmos.color = (n.IsWalkable ? Color.white : Color.red);

                    Gizmos.DrawCube(n.WorldPosition, Vector3.one * (_nodeDiameter - .1f));

                }
            }
        }

        #endregion

    }
}