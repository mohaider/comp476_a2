using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script.AI.PathFinding
{
    public class PoVNodes:MonoBehaviour
    {
        #region class variables and properties

        public Node _associatedNode;
        public string info;
        #endregion

        
        public Node AssociatedNode
        {
            get { return _associatedNode; }
            set { _associatedNode = value; }
        }

        void Update()
        {
            if (_associatedNode._inCluster !=null)
            info = (_associatedNode == null) ? "null" : "is in cluster " + _associatedNode._inCluster.name;
            if (_associatedNode.ParentNode != null)
                info += " parent node is " + _associatedNode.ParentNode.AssociatedPovNode.name;
        }
    }
}
