using Assets.AI.KinematicsAndSteering;
using UnityEngine;
using Assets.Script.Tools;
/**
 * Code written by Mohammed Haider
 * If you have any questions about this code, feel free to email me
 * mrhaider@gmail.com 
 */

namespace Assets.Script.AI.KinematicsAndSteering
{
    public class Kinematic : MonoBehaviour
    {
        #region class properties
        [SerializeField] private Steering steering;
        [SerializeField] private float maxSpeed;


        public float MaxSpeed
        {
            get { return maxSpeed; }
            set { maxSpeed = value; }
        }

        #endregion
        // Use this for initialization
        private void Awake()
        {
            steering = GetComponent<Steering>();
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            Vector3 yFlattener = rigidbody.velocity + steering.Linear * Time.fixedDeltaTime;
            yFlattener.y = 0;
            rigidbody.velocity = AdditionalVector3Tools.Limit(yFlattener, maxSpeed);
          //  .rigidbody.velocity += steering.Linear*Time.fixedDeltaTime;
           // rigidbody.velocity = AdditionalVector3Tools.Limit(rigidbody.velocity, maxSpeed);

            float angle = transform.rotation.eulerAngles.y;

            angle += steering.Angular*Time.fixedDeltaTime;
            Quaternion rotationQuaternion = Quaternion.Euler(0, angle, 0);
            transform.rotation = rotationQuaternion;



        }
    }
}