using UnityEngine;
using System.Collections.Generic;
using Assets.Script.AI.PathFinding;

namespace Assets.Script.AI.PathFinding
{
    public class AStarPathFinding : MonoBehaviour
    {
        #region class variables and properties

        [SerializeField] private Grid grid; //the game's grid
        #endregion

        #region unity functions

        void Awake()
        {
            grid = GetComponent<Grid>();
        }

        #endregion

        #region class functions

        void FindPath(Vector3 startPos, Vector3 targetPos)
        {
            Node startNode = grid.QuantizePosition(startPos); // get node relative to the startPos
            Node targetNode = grid.QuantizePosition(targetPos); //get target node relative to the start pos


            //according to the A* algorithm, we need an open list and closed list
            List<Node> openList  = new List<Node>();
            HashSet<Node> closedList = new HashSet<Node>();//we need to be able to check if the list contains a specific node, so a hashset or dictionary set should suffice
        }

        #endregion

    }
}
