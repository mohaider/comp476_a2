using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Environment
{

    public class SubRoomYAxisCorrection : MonoBehaviour
    {
        #region class variables and properties

        [SerializeField] private Vector3 positionCorrection;

        #endregion
        // Use this for initialization
        private void Start()
        {
            transform.position += positionCorrection;
        }

        // Update is called once per frame
        private void Update()
        {

        }
    }
}