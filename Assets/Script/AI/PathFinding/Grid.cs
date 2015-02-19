using System;
using UnityEngine;
using System.Collections;

namespace Assets.Scripts.AI.PathFinding
{
    public class Grid : MonoBehaviour
    {
        #region class variables and properties

        Node[,] grid;
        [SerializeField]
        private Vector2 worldSize; //our area in the world

        [SerializeField]
        private float _nodeDiameter;

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





        #endregion


        #region gizmos

        void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position,new Vector3(worldSize.x,1f,worldSize.y));
            //draw walkable paths
            if(grid !=null)
            foreach (Node n in grid)
            {
                Gizmos.color = (n.IsWalkable ? Color.white : Color.red);
                Gizmos.DrawCube(n.WorldPosition, Vector3.one * (_nodeDiameter-.1f));

            }
        }
        #endregion

    }
}