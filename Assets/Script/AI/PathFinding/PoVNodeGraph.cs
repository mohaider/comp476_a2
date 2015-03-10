using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.LinearAlgebra.Complex;
using UnityEngine;

namespace Assets.Script.AI.PathFinding
{
    /// <summary>
    /// this class, like Grid holds a list of the PoV nodes used in the level
    /// </summary>
    public class PoVNodeGraph : MonoBehaviour
    {
        #region class variables and properties
        public List<Node> PoVNodesList;
        public Grid grid;
        public LayerMask mask;
        public int numberOfRotationPoints;
        public Vector3 point = Vector3.forward;
        

        #endregion

        #region class functions

        public void InsertIntoPoVList(Node n)
        {
            if (PoVNodesList == null)//check if the list is not instantiated. If so, instantiate it
                PoVNodesList = new List<Node>();
            PoVNodesList.Add(n);
            grid.SetNodeToCluster(n);
            grid.SetUpExitNodeList();
        }

        public List<Node> GetNeighbours(Node n)
        {
            List<Node> neighbors= new List<Node>();
          
            float angle = -360f / numberOfRotationPoints;
            for (int i = 0; i < numberOfRotationPoints; i++)
            {
                RaycastHit hit;
                Vector3 endPos = Quaternion.Euler(0f, angle * (i - 1), 0f) * (n.WorldPosition + point) * 10;
                //Debug.DrawLine(n.WorldPosition, endPos, Color.blue, 5f);
               // if (Physics.CapsuleCast(n.WorldPosition + Vector3.up * .5f, n.WorldPosition + Vector3.up * -.5f, 2f,
                  //  endPos - n.WorldPosition, out hit, Mathf.Infinity, mask))
               // if (Physics.Linecast(n.WorldPosition, endPos, out hit, mask))
                if (Physics.Raycast(n.WorldPosition, endPos, out hit, 1000f, mask))
                {

                    if (hit.collider.tag == "NavNodes" || hit.collider.tag == "NavNodeExit")
                    {
                        GameObject obj = hit.collider.gameObject;
                        PoVNodes nud = obj.GetComponent<PoVNodes>();
                        if(nud == null )
                            print(obj.name + "has no pov node attached");
                        neighbors.Add(hit.collider.gameObject.GetComponent<PoVNodes>().AssociatedNode);
                    }
                }
            }
            return neighbors;
        }

        #endregion

        #region unity function

        private void Awake()
        {
            grid = GetComponent<Grid>();
        }

        #endregion

        /// <summary>
        /// tries to find the closest node to the target position, 
        /// </summary>
        /// <param name="targetPos"></param>
        /// <returns></returns>
        internal Node QuantizePosition(Vector3 targetPos)
        {
            RaycastHit hit;
            Node n = new Node();
            float distance = Mathf.Infinity;
            float angle = -360f / numberOfRotationPoints;
             for (int i = 0; i < numberOfRotationPoints; i++)
            {
                
                Vector3 endPos = Quaternion.Euler(0f, angle * (i - 1), 0f) * (targetPos +point) * 10;
               
             //   if (Physics.CapsuleCast(targetPos + Vector3.up * 1f, targetPos + Vector3.up * -1f, 5f,
               //     endPos - targetPos, out hit, Mathf.Infinity, mask))
                   // if(Physics.Linecast(targetPos,endPos,out hit,mask))
                if (Physics.Raycast(targetPos, endPos, out hit,1000f, mask))
                 {

                    if (hit.collider.tag == "NavNodes" || hit.collider.tag == "NavNodeExit")
                    {
                        Vector2 Dist = new Vector2(targetPos.x - hit.transform.position.x,
                            targetPos.z - hit.transform.position.z);
                        Dist.y = 0;
                       
                        float newDistance = Dist.magnitude;
                        if (newDistance < distance)
                        {
                            distance = newDistance;
                            n = hit.collider.gameObject.GetComponent<PoVNodes>()._associatedNode;
                        }


                    }
                }
            }
            return n;


        }
    }
}
