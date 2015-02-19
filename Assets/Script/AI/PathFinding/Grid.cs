using System;
using UnityEngine;
using System.Collections;

namespace Assets.Script.AI.PathFinding
{
    public class Grid : MonoBehaviour
    {
        #region class variables and properties

        Node[,] grid;
        [SerializeField]
        private Vector2 worldSize; //our area in the world
        [SerializeField]
        private Transform _player; //position of the player in the world

        [SerializeField]
        private float _nodeDiameter; //diameter of the node. 

        [SerializeField]
        private float _nodeRadius;

        [SerializeField] private LayerMask UnwalkableMask; 

        [SerializeField] private int GridSizeX, GridSizeY;




        #endregion
        #region UnityFunctions

        void Awake()
        {
            _nodeDiameter = _nodeRadius * 2;
            GridSizeX = Mathf.RoundToInt(worldSize.x/_nodeDiameter); 
            GridSizeY = Mathf.RoundToInt(worldSize.y/_nodeDiameter); 
            CreateGrid();
        }





        #endregion
        #region Class functions

        void CreateGrid()
        {
            grid = new Node[GridSizeX,GridSizeY];
            Vector3 worldBottomLeft = transform.position - Vector3.right*worldSize.x/2 - Vector3.forward*worldSize.y/2;

            //set the grid

            for (int x = 0; x < GridSizeX; x++)
            {
                for (int y = 0; y < GridSizeY; y++)
                {
                    Vector3 worldPosition = worldBottomLeft + Vector3.right*(x*_nodeDiameter + _nodeRadius) +
                                            Vector3.forward*(y*_nodeDiameter + _nodeRadius);
                    //check to see if current position is walkable
                    bool isWalkable = !Physics.CheckSphere(worldPosition, _nodeRadius, UnwalkableMask);
                    grid[x,y] = new Node(isWalkable,worldPosition);
                }
            }




        }

        public Node QuantizePosition(Vector3 worldPosition)
        {
            float percentX = (worldPosition.x -transform.position.x  + worldSize.x/2)/worldSize.x;
            float percentY = (worldPosition.z - transform.position.z + worldSize.y / 2) / worldSize.y;

            percentX = Mathf.Clamp01(percentX);
            percentY = Mathf.Clamp01(percentY);

            int x = Mathf.RoundToInt((GridSizeX - 1) * percentX);
            int y = Mathf.RoundToInt((GridSizeY - 1) * percentY);
            print("x "+x+" y"+y);
            return grid[x, y];
        }




        #endregion


        #region gizmos

        void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position,new Vector3(worldSize.x,1f,worldSize.y));
          
            //draw walkable paths
            if(grid !=null)
            {
                Node playernode = QuantizePosition(_player.position);
                foreach (Node n in grid)
            {
                Gizmos.color = (n.IsWalkable ? Color.white : Color.red);
                if (playernode == n)
                    Gizmos.color = Color.green;
                Gizmos.DrawCube(n.WorldPosition, Vector3.one * (_nodeDiameter-.1f));

            }}
        }
        #endregion

    }
}