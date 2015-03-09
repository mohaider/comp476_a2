
using UnityEngine;
/**
 * Code written by Mohammed Haider
 * If you have any questions about this code, feel free to email me
 * mrhaider@gmail.com 
 */
namespace Assets.Script.AI.KinematicsAndSteering
{
    class LookWhereYoureGoing:MonoBehaviour
    {

        public GameObject holderTarget;
        [SerializeField]
        private Align align;


        public void LookInFrontOfYou()
        {
          //  holderTarget.transform.position = alignmentTarget.transform.position;
            Vector3 direction = rigidbody.velocity;
            
            float mag = rigidbody.velocity.magnitude;

            if (mag <= 0.1f )
            {
                return;
            }
          
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            

            Quaternion rotQuaternion = Quaternion.Euler(new Vector3(0, targetAngle, 0));
            holderTarget.transform.rotation = rotQuaternion;
            align.target = holderTarget;
        }


        private void Awake()
        {
            align = GetComponent<Align>();
            align.target = holderTarget;
        }

        private void Update()
        {
            LookInFrontOfYou();

        }
    }
}
