using UnityEngine;

using Assets.AI.KinematicsAndSteering;
using Assets.Script.Tools;

namespace Assets.Script.AI.KinematicsAndSteering
{
    class Align : MonoBehaviour
    {

        public GameObject target;

        public float maxAngularAcceleration;
        public float maxRotation;

        public float targetRadius;
        public float slowRadius;
        public float timeToTarget = 0.1f;
        [SerializeField]
        private Steering steering;


        private void align()
        {
            float rotation = target.transform.rotation.eulerAngles.y - transform.rotation.eulerAngles.y;

            rotation = AdditionalVector3Tools.mapAngleToRange(rotation);
            float rotationSize = Mathf.Abs(rotation);
            float targetRotation = 0;
            //only rotation if we're outside the target radius
            if (rotationSize < targetRadius)
            {
                steering.Angular =0;
                return;
            }

            if (rotationSize > slowRadius)
            {
                targetRotation = maxRotation;
          
            }
            else
            {
                targetRotation = maxRotation * rotationSize / slowRadius;

            }
            targetRotation *= maxRotation*rotation/rotationSize;

            steering.Angular = targetRotation - transform.rotation.eulerAngles.y;
            steering.Angular /= timeToTarget;

            float angularAcceleration = Mathf.Abs(steering.Angular);

            //check if the acceleration is too great
            if (angularAcceleration > maxAngularAcceleration)
            {
                steering.Angular /= angularAcceleration;
                steering.Angular *= maxAngularAcceleration;
            }
            steering.Linear = Vector3.zero;

        }

        void Awake()
        {
            steering = GetComponent<Steering>();
        }



        // Update is called once per frame
        void Update()
        {
            //hold the naive direction to the target
            align();
            

        }
    }
}
