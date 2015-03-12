using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;

namespace Assets.Script.AI.KinematicsAndSteering
{
    class ObstacleAvoidance: MonoBehaviour
    {
        #region class properties and variables
        public GameObject target;
        public float avoidDistance;
        public float leftRayAngle;
        public float rightRayAngle;
        public Ray centerRay;
        public Ray leftRay;
        public Ray rightRay;
        public LayerMask wallMask;
        private Vector3 targetVector3;
        #endregion

        #region unity functions

        void Start()
        {
            
        }
        #endregion

        #region class functions

        public Vector3 CheckForCollisions()
        {
            RaycastHit[] hit = new RaycastHit[3];

            
            Vector3 leftPoint = Quaternion.Euler(0f, leftRayAngle, 0f) * (transform.forward );
            Vector3 rightPoint = Quaternion.Euler(0f, rightRayAngle, 0f) * (transform.forward );
            leftPoint += transform.position;
            rightPoint += transform.position;
            if (!Physics.Raycast(transform.position, transform.forward, out hit[1], avoidDistance, wallMask)
                &&
                !Physics.Raycast(transform.position, leftPoint - transform.position, out hit[0], avoidDistance,
                    wallMask)
                &&
                !Physics.Raycast(transform.position, rightPoint - transform.position, out hit[2], avoidDistance,
                    wallMask))
                return Vector3.zero;

            RaycastHit colHit = new RaycastHit();
            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i].collider != null)
                {
                    colHit = hit[i];
                    break;
                }
            }
             return colHit.point + colHit.normal*avoidDistance;




        }
        #endregion

        #region gizmos

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Vector3 centerPoint = transform.position + transform.forward*avoidDistance;
            
            Vector3 leftPoint = Quaternion.Euler(0f, leftRayAngle, 0f) * (transform.forward*avoidDistance);
            leftPoint += transform.position;

            Vector3 rightPoint = Quaternion.Euler(0f,  rightRayAngle, 0f) * (transform.forward * avoidDistance);
            rightPoint += transform.position;
            
           
            Gizmos.DrawLine(transform.position, centerPoint);
            Gizmos.DrawLine(transform.position, leftPoint);
            Gizmos.DrawLine(transform.position, rightPoint);

            
        }
        #endregion

    }
}
