using UnityEngine;
using System.Collections;

public class RandomizePumpkinAngles : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
	    Transform[] pumpkins = GetComponentsInChildren<Transform>();
	    foreach (Transform t in pumpkins)
	    {
	        if (t.name == "pumpkinpatch")
	            continue;
	        float randomAngle = Random.Range(0, 360f);
	        Quaternion rotQuaternion = Quaternion.Euler(0, randomAngle, 0);
	        t.rotation = rotQuaternion;
	    }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
