using UnityEngine;
using System.Collections;
using System.Xml.Schema;

public class raycastingChecker : MonoBehaviour
{
    public LayerMask mask;
    public int totalRot;
    public int numberOfRotationPoints;
    public Vector3 point = Vector3.forward;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (Input.GetKeyDown(KeyCode.A))
	        Startlooking();
	}

    void Startlooking()
    {
        float angle = -360f / numberOfRotationPoints;
        for (int i = 0; i < numberOfRotationPoints; i++)
        {
            RaycastHit hit;
            Vector3 endPos = Quaternion.Euler(0f, angle * (i - 1), 0f) * point * 10;
            //Debug.DrawLine(transform.position,endPos,Color.red,5f);
            if (Physics.CapsuleCast(transform.position + Vector3.up * .5f, transform.position + Vector3.up * -.5f, 2f, endPos - transform.position, out hit, Mathf.Infinity,
                mask));
            if (hit.collider != null)
            {
                if (hit.collider.tag =="NavNodes")
                print(hit.collider.name);
            }
            else print("nothing");
        }
    }
}
