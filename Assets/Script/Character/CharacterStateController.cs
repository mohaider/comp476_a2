using UnityEngine;

namespace Assets.Script.Character
{
    /**
 * Code written by Mohammed Haider
 * If you have any questions about this code, feel free to email me
 * mrhaider@gmail.com 
 */
    class CharacterStateController:MonoBehaviour
    {

        public enum characterState
        {
            seek,
            stop,
            idle
        }

        public delegate void characterStateHander(characterState newState);

        public event characterStateHander OnStateChange;

        public void ChangeState(characterState newState)
        {
            if (OnStateChange != null)
                OnStateChange(newState);
        }

  
    }
}
