using Assets.Script.AI.PathFinding;
using UnityEngine;
namespace Assets.Script.Character
{
    class MouseController : MonoBehaviour
    {
        #region class properties and variables
        [SerializeField]
        private Transform mouseTarget;
        [SerializeField]
        private Grid grid;

        [SerializeField]
        private PoVNodeGraph nodeGraph;

        [SerializeField]
        private LayerMask floorMask;

        [SerializeField]
        private AStarPathFinding astar;

        [SerializeField] private Unit unit;
        #endregion

        #region unity functions

        void Update()
        {
            CheckForMousePress();
        }

        #endregion

        #region class functions

        void CheckForMousePress()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(mouseRay, out hit, Mathf.Infinity, floorMask))
                {
                    if (CheckValidPoint(hit.point))
                    {
                        Vector3 pos = mouseTarget.position;
                        Vector3 hitPos = hit.point;
                        
                        pos.x = hit.point.x;
                        pos.z = hit.point.z;

                        mouseTarget.position = pos;

                        unit.Move();
                    }
                   

                }
               
            }
        }

        private bool CheckValidPoint(Vector3 hit)
        {
            bool isValidPoint = false;
            switch (astar.pathType)
            {
                case AStarPathFinding.PathFindingGraphType.GridSearch:
                    Node n = grid.QuantizePosition(hit);
                    isValidPoint = n.IsWalkable;
                    break;

                case AStarPathFinding.PathFindingGraphType.PoVSearch:
                    Node n1 = nodeGraph.QuantizePosition(hit);
                    isValidPoint = true;
                    break;




            }
            return isValidPoint;
        }
        #endregion
    }
}
