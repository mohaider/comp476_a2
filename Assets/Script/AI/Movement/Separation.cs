using UnityEngine;
using System.Collections;



namespace Assets.Scripts.AI.Movement
{

    public class Separation : MonoBehaviour
    {/*

        private ArrayList teamPool;
        [SerializeField] private float threshhold;
        [SerializeField] private float decayCoefficient;
        [SerializeField] private float maxAcceleration;
        private Vector3 acceleration;
        // Use this for initialization
        private void Start()
        {
            GameObject manager;
            if (tag == "TeamOrange")
                manager = GameObject.FindGameObjectWithTag("TeamOrangeManager");
            else
                manager = GameObject.FindGameObjectWithTag("TeamBananaManager");
            if (manager != null)
                teamPool = manager.GetComponent<TeamManager>().TeamPool;
            maxAcceleration = GetComponent<MovementBehaviour>().maxAcceleration;
            acceleration = Vector3.zero;
        }

        // Update is called once per frame
        private void Update()
        {



        }


        public Vector3 GetAccelerationVector3()
        {
            for (int i = 0; i < teamPool.Count; i++)
            {

                float strength = 0;
                GameObject target = teamPool[i] as GameObject;
                if (target == gameObject)
                    continue;
                Vector3 directionVector3 = target.transform.position - gameObject.transform.position;
                float distance = directionVector3.magnitude;
                if (distance < threshhold)
                {
                    strength = Mathf.Min(decayCoefficient/(distance*distance), maxAcceleration);
                }
                //add the acceleration 
                directionVector3 = directionVector3.normalized;
                acceleration += strength*directionVector3;

            }
            return acceleration;
        }*/
    }
}