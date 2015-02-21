using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour
{

    #region class variables and properties

    [SerializeField] private Transform target;
    [SerializeField] private float speed = 20;
    [SerializeField] private float _lineHeight = 5f;
    private Vector3[] path;
    private int targetIndex;
    


   

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
        Vector3 currentWayPoint = path[0];
        while (true)
        {
            if (transform.position == currentWayPoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    yield break;
                }
                currentWayPoint = path[targetIndex];
            }
            transform.position = Vector3.MoveTowards(transform.position, currentWayPoint, speed*Time.deltaTime);
            yield return null;
        }

    }
    #endregion

    void Start()
    {
        PathRequestManager.RequestPath(transform.position, target.position,OnPathFound);
    }

    void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i],Vector3.one);

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
