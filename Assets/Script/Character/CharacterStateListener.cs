using System.Security.Cryptography;
using Assets.Script.AI.KinematicsAndSteering;
using UnityEngine;
namespace Assets.Script.Character
{
    /**
 * Code written by Mohammed Haider
 * If you have any questions about this code, feel free to email me
 * mrhaider@gmail.com 
 */
    class CharacterStateListener:MonoBehaviour
    {
        /*#region class variables and properties

        public bool showCharacterGizmos = false;
        
        public GameObject target;
        [SerializeField]
        private CharacterStateController.characterState currentState = CharacterStateController.characterState.idle;
        //current state
        public CharacterStateController.characterState CurrentState
        {
            get { return currentState; }
            set { currentState = value; }

        }
        #endregion


        #region class functions
        public void ChangeTarget(GameObject newTarget)
        {
            target = newTarget;
        }
        void OnStateCycle()
        {
            Vector3 directionVector3;
            float dist = 0f;
            if (target != null)
            {
                directionVector3 = target.transform.position - transform.position;
                dist = directionVector3.magnitude;
            }

            switch (currentState)
            {
                case CharacterStateController.characterState.seek:
                    Chase();
                    break;

                case CharacterStateController.characterState.stop:
                    Stop();
                    break;
            }
        }

        private void Stop()
        {
        }

        void Chase()
        {
            Vector3 direction = target.transform.position - transform.position;
            direction.y = 0; //flatten y
            float distance = direction.magnitude;
            Vector3 characterForward = transform.forward;
            characterForward.y = 0;

            float angleBetweenDirectionAndTarget = Vector3.Angle(direction, characterForward);
            float currentSpeed = rigidbody.velocity.magnitude;

            if (currentSpeed <= 1f)
            {
                if (distance < (GetComponent<Arrive>().targetRadius + GetComponent<Arrive>().targetRadius*.10f))
                {
                    float theta = transform.rotation.eulerAngles.y *Mathf.Deg2Rad;
                    float x = Mathf.Cos(theta)*GetComponent<Arrive>().targetRadius*.10f + transform.position.x;
                    float z = Mathf.Sin(theta) * GetComponent<Arrive>().targetRadius * .10f+ transform.position.z;
                    Vector3 newPos = new Vector3(x, 0, z);
                    Debug.Log("might have to switch these two around");
                    transform.position = newPos;
                }
                else
                {
                    float difInAngles = Vector3.Angle(characterForward, direction.normalized);
                    float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                    targetAngle %= 360f;
                    float currentAngle = transform.rotation.eulerAngles.y % 360;
                    float differenceInAngles = targetAngle - currentAngle;
                    if (difInAngles > 20f)
                    {
                       
                        _characterBehaviourWrapper.Rotate(CharacterBehaviourWrapper.HeuristicType.A2);
                    }
                    else
                    {
                        _characterBehaviourWrapper.Move(CharacterBehaviourWrapper.HeuristicType.A2);
                    }
                }
            }

        }
        #endregion

        #region 

        #endregion
*/
    
    }
}
