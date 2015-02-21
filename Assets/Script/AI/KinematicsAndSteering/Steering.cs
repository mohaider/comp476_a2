using UnityEngine;
using System.Collections;

namespace Assets.AI.KinematicsAndSteering
{
    public class Steering : MonoBehaviour
    {
        [SerializeField] private Vector3 linear;//acceleration component;

        [SerializeField] private float angular;     //representation of the character's angular acceleration

        public Vector3 Linear
        {
            get { return linear; }
            set { linear = value; }
        }

        public float Angular
        {
            get { return angular; }
            set { angular = value; }
        }

        void Update()
        {
            linear.y = 0;
        }

    }
}