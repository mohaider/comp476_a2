using System;
using System.IO;
using UnityEngine;
using Assets.Script.AI.PathFinding;

using System.Xml.Serialization;
[XmlRoot("NodeCollection")]
public class GridToFile 
{
    #region classvariables and properties

    [XmlArray("Nodes")] [XmlArrayItem("Node")] public Node[,] nodes;
   
   
    #endregion
    #region class functions

    public void Save(string path)
    {
        var serializer = new XmlSerializer(typeof (GridToFile));
        using (var stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream,this);
        }
    }

    public static GridToFile LoadFromText(string text)
    {
        var serializer = new XmlSerializer(typeof(GridToFile));
        return serializer.Deserialize(new StringReader(text)) as GridToFile;
    }
    #endregion

    #region unity functions
    // Use this for initialization
	void Start ()
	{
	     
	 
       Save(System.IO.Path.Combine(Application.persistentDataPath, "grid.xml"));
        int[,] n4 = new int[3, 2] { { 1, 2 }, { 3, 4 }, { 5, 6 } };
	    string s = "";
        
      

	    for (int x = 0; x < n4.GetLength(0);x++)
	    {
	        for (int y = 0; y < n4.GetLength(1);y++)
	        {
	            s += n4[x, y];
	            if (y != n4.GetLength(1) - 1)
	                s += ",";
	        }
            if (x != n4.GetLength(0) - 1)
	         s += "\r\n";
	    }
	    System.IO.StreamWriter file = new System.IO.StreamWriter("gridValues.txt");
        file.WriteLine(s);

        file.Close();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    #endregion
    #region serializing
    /// <summary>
    /// Since unity doesn't flag the Vector3 as serializable, we
    /// need to create our own version. This one will automatically convert
    /// between Vector3 and SerializableVector3
    /// </summary>
   /* [Serializable]
    public struct SerializableVector3
    {
        /// <summary>
        /// x component
        /// </summary>
        public float x;

        /// <summary>
        /// y component
        /// </summary>
        public float y;

        /// <summary>
        /// z component
        /// </summary>
        public float z;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="rX"></param>
        /// <param name="rY"></param>
        /// <param name="rZ"></param>
        public SerializableVector3(float rX, float rY, float rZ)
        {
            x = rX;
            y = rY;
            z = rZ;
        }

        /// <summary>
        /// Returns a string representation of the object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("[{0}, {1}, {2}]", x, y, z);
        }

        /// <summary>
        /// Automatic conversion from SerializableVector3 to Vector3
        /// </summary>
        /// <param name="rValue"></param>
        /// <returns></returns>
        public static implicit operator Vector3(SerializableVector3 rValue)
        {
            return new Vector3(rValue.x, rValue.y, rValue.z);
        }

        /// <summary>
        /// Automatic conversion from Vector3 to SerializableVector3
        /// </summary>
        /// <param name="rValue"></param>
        /// <returns></returns>
        public static implicit operator SerializableVector3(Vector3 rValue)
        {
            return new SerializableVector3(rValue.x, rValue.y, rValue.z);
        }
    }*/
    #endregion
}
