using Assets.Script.Tools;
using Assets.AI.KinematicsAndSteering;
using UnityEngine;

namespace Assets.Script.AI.KinematicsAndSteering
{
    internal class Arrive : MonoBehaviour
    {
        public GameObject target;

        public float maxAcceleration;
        public float maxSpeed;
        public float targetRadius;
        public float slowRadius;
        public float timeToTarget = 0.1f;

        public Steering steering;

        public void Arrival()
        {
            Vector3 direction = target.transform.position - transform.position;
            direction.y = 0;
            float distance = direction.magnitude;
            float targetSpeed = 0;
            if (distance < targetRadius)
            {
                steering.Linear = Vector3.zero;
                rigidbody.velocity = Vector3.zero;
                return;
            }
            if (distance > slowRadius)
            {
                targetSpeed = maxSpeed;
            }
            else
            {
                targetSpeed = maxSpeed*distance/slowRadius;
            }
            Vector3 targetVelocity = direction;
            targetVelocity.Normalize();
            targetVelocity *= targetSpeed;
            steering.Linear = targetVelocity - rigidbody.velocity;
            steering.Linear /= timeToTarget;

            steering.Linear = AdditionalVector3Tools.Limit(steering.Linear, maxAcceleration);
            GetComponent<Kinematic>().MaxSpeed = targetSpeed;

        }


        private void Awake()
        {
            steering = GetComponent<Steering>();
           
        }

        void Update()
        {
            Arrival();
        }
}
}
