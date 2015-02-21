using UnityEngine;

using Assets.AI.KinematicsAndSteering;

namespace Assets.Script.AI.KinematicsAndSteering
{



    public class Flee : MonoBehaviour
    {

        public float maxAcceleration;
        public GameObject target;
        private Vector3 acceleration;
        [SerializeField]
        private Steering steering;
        // Use this for initialization
        void Awake()
        {
            steering = GetComponent<Steering>();
        }

        void flee()
        {
            steering.Linear = transform.position - target.transform.position;
            steering.Linear.Normalize();
            steering.Linear *= maxAcceleration;
            steering.Angular = 0;
        }

        // Update is called once per frame
        void Update()
        {
            flee();

        }
    }
}