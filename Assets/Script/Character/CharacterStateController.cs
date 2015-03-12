using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Script.AI.KinematicsAndSteering;
using UnityEngine;

namespace Assets.Script.Character
{
    class CharacterStateController:MonoBehaviour
    {
        #region class properties and variables
        [SerializeField]
        private float velocityThreshold;
        [SerializeField]
        private float distanceThreshold;
        [SerializeField]
        private Arrive arrive;
        [SerializeField]
        private LookWhereYoureGoing lookWhereYoureGoing;
        [SerializeField]
        private Align align;
        [SerializeField]
        private Face face;
        public string outputinfo;

        public enum CharacterState
        {
            move,
            stop,
            rotateAndMove,
            stopAndRotate
        }
        [SerializeField]
        private CharacterState currentState = CharacterState.stop;

        public CharacterState CurrentState
        {
            get { return currentState; }
            set { currentState = value; }
        }

        public float VelocityThreshold
        {
            get { return velocityThreshold; }
            set { velocityThreshold = value; }
        }

        public float DistanceThreshold
        {
            get { return distanceThreshold; }
            set { distanceThreshold = value; }
        }

        public Arrive Arrive1
        {
            get { return arrive; }
            set { arrive = value; }
        }

        public LookWhereYoureGoing WhereYoureGoing
        {
            get { return lookWhereYoureGoing; }
            set { lookWhereYoureGoing = value; }
        }

        public Align Align1
        {
            get { return align; }
            set { align = value; }
        }

        public Face Face1
        {
            get { return face; }
            set { face = value; }
        }

        public string Outputinfo
        {
            get { return outputinfo; }
            set { outputinfo = value; }
        }

        #endregion
        #region unity functions

        void Awake()
        {
            arrive = GetComponent<Arrive>();
            lookWhereYoureGoing = GetComponent<LookWhereYoureGoing>();
            align = GetComponent<Align>();
            face = GetComponent<Face>();
            distanceThreshold = arrive.targetRadius + arrive.targetRadius*0.25f;
        }

        void Update()
        {
            CheckState();
        }

        #endregion
        private void CheckState()
        {
            Vector3 directionalVector3 = align.target.transform.position - transform.position;
            directionalVector3.y = 0; //flatten y;
            Vector3 characterForward = transform.forward;
            characterForward.y = 0;
            float angleBetweenDirAndTar = Vector3.Angle(directionalVector3, transform.forward);
            float distance = directionalVector3.magnitude;
            float currentSpeed = rigidbody.velocity.magnitude;

            if (currentSpeed <= velocityThreshold)//A
            {
                outputinfo = "In A";

                //if the character is really close to the target(A.1)
                if (distance < distanceThreshold)
                {
                    outputinfo = "In A.i";
                    transform.position = align.target.transform.position;
                    currentState = CharacterState.stop;
                }
                else
                {
                    //need to stop and rotate:
                    float difInAngles = Vector3.Angle(characterForward, directionalVector3.normalized);
                    float targetAngle = Mathf.Atan2(directionalVector3.x, directionalVector3.z) * Mathf.Rad2Deg;
                    targetAngle %= 360f;
                    float currentAngle = transform.rotation.eulerAngles.y % 360;
                  

                    outputinfo = "In A.ii, difference between angles is " + difInAngles;

                    // float diferen = curangle - angere;

                    if (difInAngles > 20f)
                    {
                        outputinfo = "In A.ii difference between angles is " + difInAngles;
                        currentState = CharacterState.stopAndRotate;
                    }
                    else
                    {
                        currentState = CharacterState.rotateAndMove;
                    }

                }
            }
            else
            {
                outputinfo = "In B";
                if (Mathf.Abs(angleBetweenDirAndTar) < 100f)
                {
                    currentState = CharacterState.rotateAndMove;
                    //  print("chase state 3");
                    outputinfo = "In B.i";
                }
                else
                {
                    //  rigidbody.velocity = Vector3.zero;
                    //stop the character
                    currentState = CharacterState.stopAndRotate;

                    outputinfo = "In B.ii";
                    //  print("chase state 4");

                }
            }

        }
    }
}
