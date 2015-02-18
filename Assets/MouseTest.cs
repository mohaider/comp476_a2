using UnityEngine;
using System.Collections;

public class MouseTest : MonoBehaviour
{
    RaycastHit hit;
    public Transform mousetar;
    public Vector3 mousepos;
    #region
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    MoveObject();

	    /*        //mousepos = Input.get
                mousepos = Input.mousePosition;
                mousepos = Camera.main.ScreenToWorldPoint(mousepos);
                Vector3 mousetarpos;
                mousetarpos.x = mousepos.x;
                mousetarpos.z = mousepos.y;
                mousetarpos.y = 1.1f;
	   
                //mousepos.y = 1.1f;
                mousetar.position = mousetarpos;*/
	}

    private void MoveObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            mousetar.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
        }
    }


    #endregion
}
