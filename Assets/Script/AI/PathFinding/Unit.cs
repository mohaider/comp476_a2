using UnityEngine;
using System.Collections;
using System.Linq;
using Assets.Script.AI.KinematicsAndSteering;
using Assets.Script.AI.PathFinding;
using Assets.Script.Character;

/**
 * Code written by Mohammed Haider
 * If you have any questions about this code, feel free to email me
 * mrhaider@gmail.com 
 */
public class Unit : MonoBehaviour
{

    #region class variables and properties

    public Color lineColor;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float speed = 20;
    [SerializeField]
    private float _lineHeight = 5f;
    private Vector3[] path;
    private int targetIndex;
    private Arrive arrive;
    public GameObject holder;
    [SerializeField]
    private float arriveRadius;
     [SerializeField]
   //private UnityEngine.UI.Text text;




    #endregion

    #region class functions

    public void OnPathFound(Vector3[] newPath, bool isSuccesfull)
    {
  if (isSuccesfull)
        {

            path = newPath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
            
        }

    }

    IEnumerator FollowPath()
    {

        if (path.Length == 0)
        {
            path = new Vector3[1];
            path[0] = transform.position + transform.forward * 1.5f;
        }
        Vector3 currentWayPoint = path[0];
        holder.transform.position = currentWayPoint;
        arrive.target = holder;
        while (true)
        {
            Vector3 direction = transform.position - currentWayPoint;
           // direction.y = 0;
     //       text.text = "transform position is " + transform.position;
            if (direction.magnitude < 0.1f)
            {
                targetIndex++;

                if (targetIndex >= path.Length)
                {
                    targetIndex = 0;
                    path = null;
                    yield break;
                }
                currentWayPoint = path[targetIndex];
                holder.transform.position = currentWayPoint;
            }

            arrive.target = holder;
            // transform.position = Vector3.MoveTowards(transform.position, currentWayPoint, speed*Time.deltaTime);

            yield return null;
        }

    }

    public void Move()
    {
        targetIndex = 0;
        PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
    }
    #endregion

    void Start()
    {
        arrive = GetComponent<Arrive>();
        GameObject a = GameObject.FindGameObjectWithTag("gridMaker");
      //  arriveRadius = a.GetComponent<Grid>().NodeRadius;
        arriveRadius = GetComponent<CharacterStateController>().DistanceThreshold;
        arrive.target = holder;
       PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
    }

    void OnDrawGizmos()
    {
        bool pathIsnull = (path == null);
        if (!pathIsnull)
        {
        
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = lineColor;
                Gizmos.DrawCube(path[i], Vector3.one);

                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position + Vector3.up * _lineHeight, path[i] + Vector3.up * _lineHeight);

                }
                else
                {
                    Gizmos.DrawLine(path[i - 1] + Vector3.up * _lineHeight, path[i] + Vector3.up * _lineHeight);
                }
            }
          
        }
    }

}
