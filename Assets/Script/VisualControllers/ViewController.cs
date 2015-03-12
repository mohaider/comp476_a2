using Assets.Script.AI.PathFinding;
using UnityEngine;

namespace Assets.Script.VisualControllers
{
    public class ViewController: MonoBehaviour
    {
        #region class variables and properties

        [SerializeField] private AStarPathFinding astar;
        [SerializeField] private NavNodeScript navNodes;
        [SerializeField] private Grid grid;
        [SerializeField] private bool _showNodes =false;


        #endregion

        #region class functions

        public void ToggleViewOnOrOff()
        {
            _showNodes = !_showNodes;
            switch (astar.pathType)
            {
                case AStarPathFinding.PathFindingGraphType.GridSearch:
                    if (_showNodes)
                    {
                        navNodes.ToggleView(false);
                        grid.ToggleGridView(true);
                    }
                    else
                        grid.ToggleGridView(false);

                    break;

                case AStarPathFinding.PathFindingGraphType.PoVSearch:
                    if (_showNodes)
                    {
                        grid.ToggleGridView(false);
                        navNodes.ToggleView(true);
                       
                    }
                    else
                        navNodes.ToggleView(false);
                    break;

            }
           
            
        }

        public void ToggleBetweenViews()
        {
            switch (astar.pathType)
            {
                case AStarPathFinding.PathFindingGraphType.GridSearch:
                    if (_showNodes)
                    {
                        navNodes.ToggleView(false);
                        grid.ToggleGridView(true);
                    }
                    

                    break;

                case AStarPathFinding.PathFindingGraphType.PoVSearch:
                    if (_showNodes)
                    {
                        navNodes.ToggleView(true);
                        grid.ToggleGridView(false);
                    }
                    break;

            }
        }

        #endregion

    }
}
