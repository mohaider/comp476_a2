using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.AI.KinematicsAndSteering
{
    class Face:MonoBehaviour
    {
    
        public GameObject alignmentTarget;
        public GameObject holderTarget;
        [SerializeField] private Align align;


        public void FaceTarget()
        {
            holderTarget.transform.position = alignmentTarget.transform.position;
            Vector3 direction = holderTarget.transform.position - transform.position;
            float mag = direction.magnitude;
            if (mag <= 0.1f && mag >= 0f)
            {
                return;
            }
            
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            align.target = holderTarget;

            Quaternion rotQuaternion = Quaternion.Euler(new Vector3(0, targetAngle, 0));
            holderTarget.transform.rotation = rotQuaternion;
            
        }


        private void Awake()
        {
            align = GetComponent<Align>();
        }

        private void Update()
        {
            FaceTarget();
 
        }
    }
}
