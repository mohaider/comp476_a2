using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Script.Tools;

namespace Assets.Script.AI.KinematicsAndSteering
{
    class Wander:MonoBehaviour
    {
        public float wanderOffset;
        public float wanderRadius;
        public float wanderRate;
        public float wanderOrientation;
        public float MaxAcceleration;
        public Face face;
        public GameObject wanderTarget;

        public void wander()
        {
            wanderOrientation += RandomNums.RandomBinomial()*wanderRate;
           
            float angle = face.holderTarget.transform.rotation.eulerAngles.y;
            angle = wanderOrientation + transform.rotation.eulerAngles.y;
            Quaternion rotQuaternion = Quaternion.Euler(0, angle, 0);
            wanderTarget.transform.rotation = rotQuaternion;
 


            //calcuate the center of the wander circle
            wanderTarget.transform.position = transform.position + (wanderOffset * transform.forward);
            wanderTarget.transform.position += wanderRadius * wanderTarget.transform.forward;

            face.alignmentTarget = wanderTarget;

            face.align.steering.Linear = MaxAcceleration*transform.forward;

            //    face.




        }


        private void Awake()
        {
            face = GetComponent<Face>();
        }

        private void Update()
        {
            wander();
        }
    }
}
