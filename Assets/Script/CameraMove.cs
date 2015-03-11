using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour
{
    #region class variables and properties

    [SerializeField] private Transform character;
    [SerializeField] private float offsetX, offsetZ;
    #endregion
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    Vector3 pos = transform.position;
	    pos.x = character.position.x -offsetX;
	    pos.z = character.position.z-offsetZ;
	    transform.position = pos;
	}

   
}
