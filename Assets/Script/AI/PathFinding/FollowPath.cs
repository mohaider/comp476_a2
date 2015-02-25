using System.Reflection;
using Assets.AI.KinematicsAndSteering;
using UnityEngine;

namespace Assets.Script.AI.PathFinding
{
    class FollowPath:MonoBehaviour
    {
        #region class variables

        //holds the path to follow
        private Path path;
        //holds the distance along with the path to generate the target.
        //can be negative if the character is to move along the reverse direction
        private float pathOffset;

        //holds the current position on the path
        private Vector3 currentParam;

        private Seek seek;

        #endregion

        #region class functions

        void followPath()
        {
            //calculate the target to delegate to face
          /*  currentParam = path.getParam(transform.position, currentPos);

            //offset it
            targetParam = currentParam + pathOffset;

            ///get the target position
            target.transform.position = path.getParam(targetParam);

*/
        }

        #endregion


        #region unity functions

        private void Awake()
        {
            seek = GetComponent<Seek>();

        }


        #endregion
    }
}
