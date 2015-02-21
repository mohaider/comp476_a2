
using Assets.AI.KinematicsAndSteering;

using UnityEngine;

namespace Assets.Script.AI.KinematicsAndSteering
{
    class Evade:MonoBehaviour
    {
        [SerializeField]
        private float maxPrediction;

        [SerializeField]
        private Flee fleeScript;
        public GameObject pursueTarget;
        public GameObject holderTarget;


        void EvadeTarget()
        {
            Vector3 direction =transform.position- holderTarget.transform.position ;
            float distance = direction.magnitude;
            float prediction = 0;
            float speed = rigidbody.velocity.magnitude;

            if (speed <= distance / maxPrediction)
            {
                prediction = maxPrediction;
            }
            else
            {
                prediction = distance / speed;

            }
            holderTarget.transform.position = pursueTarget.transform.position;
            holderTarget.transform.position += pursueTarget.rigidbody.velocity * prediction;
            fleeScript.target = holderTarget;



        }

        // Use this for initialization
        private void Awake()
        {
            fleeScript = GetComponent<Flee>();
            holderTarget.transform.position = pursueTarget.transform.position;

        }

        private void Update()
        {
            EvadeTarget();
        }
    }
}
