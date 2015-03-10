using System;
using UnityEngine;
using System.Collections.Generic;
using System.Xml;
namespace Assets.Script.AI.PathFinding
{/**
 * Code written by Mohammed Haider
 * If you have any questions about this code, feel free to email me
 * mrhaider@gmail.com 
 */
    public class Grid : MonoBehaviour
    {

      //  public List<Node> _gridAsList;
        private List<Cluster> _clusterList; 
        public bool displayGridGizmos;
        #region class variables and properties
        [SerializeField]
        private bool ShowAllGizmos = false;
        public Node[,] grid; //grid representation
        
        private List<Node> _path;
        private int _totalNodes;
        public Cluster[] clusters;

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

        [SerializeField] private LayerMask ExitNodeMask;

        [SerializeField]
        private int _gridSizeX, _gridSizeY;
        [SerializeField]
        private int _maxSize;

        private GridToFile gridtoFile;
        public List<Node> Path
        {
            get { return _path; }
            set { _path = value; }
        }

        public int MaxSize
        {
            get { return _gridSizeX * _gridSizeY; }

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

        public int GridSizeX
        {
            get { return _gridSizeX; }
            set { _gridSizeX = value; }
        }

        public int GridSizeY
        {
            get { return _gridSizeY; }
            set { _gridSizeY = value; }
        }

        public List<Cluster> ClusterList
        {
            get { return _clusterList; }
            set { _clusterList = value; }
        }

        public int TotalNodes
        {
            get { return _totalNodes; }
            set { _totalNodes = value; }
        }

        #endregion
        #region UnityFunctions

        void Awake()
        {
            _nodeDiameter = _nodeRadius * 2;
            _gridSizeX = Mathf.RoundToInt(worldSize.x / _nodeDiameter);
            _gridSizeY = Mathf.RoundToInt(worldSize.y / _nodeDiameter);
            CreateGrid();
            
            //    gridtoFile = new GridToFile();
            //    gridtoFile.nodes = grid;
            // gridtoFile.Save(System.IO.Path.Combine(Application.persistentDataPath, "grid.xml"));
            string path = "";
            _clusterList = new List<Cluster>();

        }





        #endregion
        #region Class functions

        void CreateGrid()
        {
            grid = new Node[_gridSizeX, _gridSizeY];
            Vector3 worldBottomLeft = transform.position - Vector3.right * worldSize.x / 2 - Vector3.forward * worldSize.y / 2;

            //set the grid
            int id = 0;
            for (int x = 0; x < _gridSizeX; x++)
            {
                for (int y = 0; y < _gridSizeY; y++)
                {
                    Vector3 worldPosition = worldBottomLeft + Vector3.right * (x * _nodeDiameter + _nodeRadius) +
                                            Vector3.forward * (y * _nodeDiameter + _nodeRadius);
                    //check to see if current position is walkable
                    bool isWalkable = !Physics.CheckSphere(worldPosition, _nodeRadius, UnwalkableMask);
                  //  bool exitNode = Physics.CheckSphere(worldPosition, _nodeRadius, ExitNodeMask);
                  //  Debug.
                    grid[x, y] = new Node(isWalkable, worldPosition, x, y);
                    //grid[x, y].isExitNode = exitNode;
                    SetNodeToCluster(grid[x, y]);
                    grid[x, y].Id = id++;
                }
            }
            _totalNodes = id;
        }

       

        public void SetNodeToCluster(Node n)
        {

            for (int k = 0; k < clusters.Length; k++)
            {
                if (n.WorldPosition.x > clusters[k].Xbounds.x && n.WorldPosition.x < clusters[k].Xbounds.y
                    && n.WorldPosition.z > clusters[k].Zbounds.x && n.WorldPosition.z < clusters[k].Zbounds.y)
                {
                    if (n._inCluster == null){ //cluster group is not set 
                        n._inCluster = clusters[k];
                        if(n.AssociatedPovNode != null)
                            clusters[k].PovNodeList.Add(n);
                        else
                            clusters[k].NodeList.Add(n);
                    } else if (n._inCluster.isSubCluster) //node is a sub cluster
                        continue;
                    else if (!n._inCluster.isSubCluster)
                    {                         // the node is in a cluster but not a subcluster. Subclusters takes precedence.
                        if (n.AssociatedPovNode != null){
                            n._inCluster.PovNodeList.Remove(n);
                            n._inCluster = clusters[k];
                            clusters[k].PovNodeList.Add(n);
                        } else {
                            n._inCluster.NodeList.Remove(n);
                            n._inCluster = clusters[k];
                            clusters[k].NodeList.Add(n);
                        }
                    } 

                }
            }
        }

      


        /// <summary>
        /// creates a list of nodes and returns from the 2d grid array of nodes
        /// </summary>
        /// <returns>List of nodes</returns>
        public List<Node> MakeListFromGrid()
        {

            List<Node> nodes = new List<Node>(_gridSizeX * _gridSizeY);
            for (int i = 0; i < _gridSizeX; i++)
                for (int j = 0; j < _gridSizeY; j++)
                {
                    nodes.Add(grid[i, j]);
                }
            return nodes;
        }
        public Node QuantizePosition(Vector3 worldPosition)
        {
            float percentX = (worldPosition.x - transform.position.x + worldSize.x / 2) / worldSize.x;
            float percentY = (worldPosition.z - transform.position.z + worldSize.y / 2) / worldSize.y;

            percentX = Mathf.Clamp01(percentX);
            percentY = Mathf.Clamp01(percentY);

            int x = Mathf.RoundToInt((_gridSizeX - 1) * percentX);
            int y = Mathf.RoundToInt((_gridSizeY - 1) * percentY);

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

                    if (checkX >= 0 && checkX < _gridSizeX && checkY >= 0 && checkY < _gridSizeY)
                    {
                        neighbours.Add(grid[checkX, checkY]);
                    }
                }

            }
            return neighbours;

        }
        public List<Node> GetWalkableNeighbors(Node n)
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

                    if (checkX >= 0 && checkX < _gridSizeX && checkY >= 0 && checkY < _gridSizeY)
                    {
                        if (grid[checkX, checkY].IsWalkable)
                            neighbours.Add(grid[checkX, checkY]);

                    }
                }

            }
            return neighbours;

        }

        public int[,] CreateAdjacencyMatrix()
        {
            int[,] adjacencyMatrix = new int[_totalNodes, _totalNodes];
            for (int i=0; i < _totalNodes ; i++)
            {
                for (int j = 0; j < _totalNodes; j++)
                {
                    adjacencyMatrix[i, j] = 0;
                }
            }
            for (int x = 0; x < _gridSizeX; x++)
            {
                for (int y = 0; y < _gridSizeY; y++)
                {
                    if (!grid[x, y].IsWalkable)
                        continue;
                    Node n = grid[x, y];
                    List<Node> neighbors = GetWalkableNeighbors(n);
                    for (int k = 0; k < neighbors.Count; k++)
                    {
                        adjacencyMatrix[n.Id, neighbors[k].Id] = 1;
                    }
                }
     
            }
            return adjacencyMatrix;
        }

        #endregion


        #region gizmos

        void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position, new Vector3(worldSize.x, 1f, worldSize.y));

            if (grid != null && displayGridGizmos == true)
            {

                foreach (Node n in grid)
                {
                    Gizmos.color = (n.IsWalkable ? Color.white : Color.red);
                    int id = n.Id;
                    if (id == 0||id ==1||id==2||id==10)
                        Gizmos.color = Color.blue;
                    Gizmos.DrawCube(n.WorldPosition, Vector3.one * (_nodeDiameter - .1f));

                }
            }
        }

        #endregion




        internal void SetUpExitNodeList()
        {
            foreach (Cluster c in clusters)
            {
                c.SetUpExitNodesList();
            }
        }
    }
}