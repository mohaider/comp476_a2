using UnityEngine;
using System.Collections;

public class debug : MonoBehaviour
{
    public Vector3 Velocity;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    Velocity = rigidbody.velocity;
	}
}
