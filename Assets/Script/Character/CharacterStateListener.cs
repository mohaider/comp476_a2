using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Script.AI.KinematicsAndSteering;
using UnityEngine;

namespace Assets.Script.Character
{
    class CharacterStateListener: MonoBehaviour
    {
        #region class properties and variables
        [SerializeField]
        private CharacterStateController _controller;
        #endregion
        #region unity function

        void Awake()
        {
            _controller = GetComponent<CharacterStateController>();
        }

        void Update()
        {
            switch (_controller.CurrentState)
            {
                case CharacterStateController.CharacterState.stop:
                    _controller.Arrive1.enabled = false;
                    rigidbody.velocity = Vector3.zero;
                    break;

                case CharacterStateController.CharacterState.rotateAndMove:
                    _controller.Arrive1.enabled = true;
                     _controller.WhereYoureGoing.enabled = true;
                    _controller.Face1.enabled = false;
                    break;

                    case CharacterStateController.CharacterState.stopAndRotate:
                    _controller.Arrive1.enabled = false;
                    _controller.WhereYoureGoing.enabled = false;
                    _controller.Face1.enabled = true;
                    rigidbody.velocity = Vector3.zero;
                    break;

                    case CharacterStateController.CharacterState.move:
                    _controller.Arrive1.enabled = true;
                
                    break;


            }
        }

        #endregion
    }
}
