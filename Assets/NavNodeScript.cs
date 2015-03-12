using UnityEngine;
using Assets.Script.AI.PathFinding;

public class NavNodeScript : MonoBehaviour
{
    public bool showNodes = false;
    public Grid grid;
    public PoVNodeGraph povGraph ;
    public CreateClusterLookUpTable clusterLookup;
    private Transform[] OrangyNodes;

    public bool ShowNodes
    {
        get { return showNodes; }
        set { showNodes = value; }
    }

    // Use this for initialization
	void Awake ()
	{
	    GameObject AStar = GameObject.FindGameObjectWithTag("gridMaker");
	    grid = AStar.GetComponent<Grid>();
	    povGraph = AStar.GetComponent<PoVNodeGraph>();
         OrangyNodes = GetComponentsInChildren<Transform>();
	    int id = grid.TotalNodes + 1;
	    foreach (Transform t in OrangyNodes)
	    {
	        if (t.name != "OrangyNode")
	            continue;
	        else
	        {
	            //add a node component to this object
                Node n = new Node(true,t.position,(int)t.position.x,(int)t.position.z);
	            n.Id = id ++;
	            t.name = "Plump and juicy orange node " + id;
	            t.gameObject.AddComponent<PoVNodes>();
	            t.gameObject.GetComponent<PoVNodes>().AssociatedNode = n;
	            n.AssociatedPovNode = t.gameObject.GetComponent<PoVNodes>();
	            n.isExitNode= t.tag == "NavNodeExit";
                povGraph.InsertIntoPoVList(n);
                t.gameObject.renderer.enabled = false;
	        }
	    }
	    
	}

    public void ToggleView()
    {
        showNodes = !showNodes;
        foreach (Transform t in OrangyNodes)
        {
            if (t.gameObject.renderer )
                t.gameObject.renderer.enabled = showNodes;
        }

    }
    internal void ToggleView(bool isDisplayed)
    {
        foreach (Transform t in OrangyNodes)
        {
            if (t.gameObject.renderer)
                t.gameObject.renderer.enabled = isDisplayed;
        }
    }


   
}
