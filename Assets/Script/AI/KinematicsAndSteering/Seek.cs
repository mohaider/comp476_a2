using UnityEngine;
using System.Collections;

namespace Assets.AI.KinematicsAndSteering
{
    public class Seek : MonoBehaviour
    {
        public float maxAcceleration;
        public GameObject target;
        private Vector3 acceleration;
        [SerializeField]
        public Steering steering;
        // Use this for initialization
        void Awake()
        {
            steering = GetComponent<Steering>();
        }

        public void seek()
        {
            steering.Linear = target.transform.position - transform.position;
            steering.Linear.Normalize();
            steering.Linear *= maxAcceleration;
         //   steering.Angular = 0;
        }
        // Update is called once per frame
        void Update()
        {
            seek();

        }
    }
}