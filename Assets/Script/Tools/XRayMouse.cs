using UnityEngine;
using System.Collections;

public class XRayMouse : MonoBehaviour
{
    #region class variables and properties
    [SerializeField] private float radius;
    // Use this for initialization
    #endregion

    #region unity funcs
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    LookInside();
        renderer.material.SetFloat("_Radius", radius);
	}

    private void LookInside()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            renderer.material.SetVector("_ObjPos", new Vector4(hit.point.x,hit.point.y,hit.point.z,0));

        }

    }
    #endregion
}
