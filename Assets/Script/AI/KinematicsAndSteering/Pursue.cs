using Assets.AI.KinematicsAndSteering;

using UnityEngine;


namespace Assets.Script.AI.KinematicsAndSteering
{
    class Pursue:MonoBehaviour
    {
    
        [SerializeField]
        private float maxPrediction;

        [SerializeField] private Seek seekScript;
        public GameObject pursueTarget;
        public GameObject holderTarget;


        void persueTarget()
        {
            Vector3 direction = holderTarget.transform.position - transform.position;
            float distance = direction.magnitude;
            float prediction = 0;
            float speed = rigidbody.velocity.magnitude;

            if (speed <= distance/maxPrediction)
            {
                prediction = maxPrediction;
            }
            else
            {
                prediction = distance/speed;
                
            }
            holderTarget.transform.position = pursueTarget.transform.position;
            holderTarget.transform.position += pursueTarget.rigidbody.velocity * prediction;
            seekScript.target = holderTarget;
           

           
        }

        // Use this for initialization
        private void Awake()
        {
            seekScript = GetComponent<Seek>();
            holderTarget.transform.position = pursueTarget.transform.position;

        }

        private void Update()
        {
            persueTarget();
        }

    }
}
