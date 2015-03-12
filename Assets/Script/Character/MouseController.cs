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

        public RectTransform button1;
    
        public RectTransform button2;

        public RectTransform button3;
        [SerializeField]
        private PoVNodeGraph nodeGraph;

        [SerializeField]
        private LayerMask floorMask;

        [SerializeField]
        private AStarPathFinding astar;

        [SerializeField]
        private Unit unit;
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
                if (!IsHoveringOverButtons())
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
        }

        private bool IsHoveringOverButtons()
        {
            Vector3[] vectorPos1 = new Vector3[4];
            Vector3[] vectorPos2 = new Vector3[4];
            Vector3[] vectorPos3 = new Vector3[4];
            button1.GetWorldCorners(vectorPos1);
            button2.GetWorldCorners(vectorPos2);
            button2.GetWorldCorners(vectorPos3);
            Vector3 mousepos = Input.mousePosition;

            return (CheckIfInsideRectangle(vectorPos1, mousepos) || CheckIfInsideRectangle(vectorPos2, mousepos) || CheckIfInsideRectangle(vectorPos3, mousepos));
        }

        private bool CheckIfInsideRectangle(Vector3[] rectVector3s, Vector3 point)
        {
            Vector2 AB = new Vector2(rectVector3s[1].x - rectVector3s[0].x, rectVector3s[1].y - rectVector3s[0].y);
            Vector2 AD = new Vector2(rectVector3s[3].x - rectVector3s[0].x, rectVector3s[3].y - rectVector3s[0].y);
            Vector2 AM = new Vector2(point.x - rectVector3s[0].x, point.y - rectVector3s[0].y);

            float AMdotAB = Vector2.Dot(AM, AB);
            float ABdotAB = Vector2.Dot(AB, AB);
            float AMdotAD = Vector2.Dot(AM, AD);
            float ADdotAD = Vector2.Dot(AD, AD);

            return ((AMdotAB >= 0) && (AMdotAB <= ABdotAB)) && ((AMdotAD >= 0) && (AMdotAD <= ADdotAD));
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
        public Rect GetScreenRect(Vector3[] corners)
        {
            RectTransform rectTransform = (RectTransform)transform;
            rectTransform.GetWorldCorners(corners);

            float xMin = float.PositiveInfinity, xMax = float.NegativeInfinity, yMin = float.PositiveInfinity, yMax = float.NegativeInfinity;
            for (int i = 0; i < 4; ++i)
            {
                // For Canvas mode Screen Space - Overlay there is no Camera; best solution I've found
                // is to use RectTransformUtility.WorldToScreenPoint) with a null camera.
                Vector3 screenCoord = RectTransformUtility.WorldToScreenPoint(null, corners[i]);
                if (screenCoord.x < xMin) xMin = screenCoord.x;
                if (screenCoord.x > xMax) xMax = screenCoord.x;
                if (screenCoord.y < yMin) yMin = screenCoord.y;
                if (screenCoord.y > yMax) yMax = screenCoord.y;
                corners[i] = screenCoord;
            }
            Rect result = new Rect(xMin, yMin, xMax - xMin, yMax - yMin);
            return result;
        }
        #endregion
    }
}
