using UnityEngine;
using System.Collections;
namespace Assets.Scripts.AI.PathFinding
{
    public class Node
    {
        #region class variables and properties

        private bool _isWalkable;
        private Vector3 _worldPosition;


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

        #endregion

        #region constructor

        public Node(bool isWalkable, Vector3 worldPosition)
        {
            _isWalkable = isWalkable;
            _worldPosition = worldPosition;
        }



        #endregion

 


    }
}