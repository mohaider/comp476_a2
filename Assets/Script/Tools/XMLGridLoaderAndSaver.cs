using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Assets.Script.AI.PathFinding;

using System.Xml.Serialization;

namespace Assets.Script.Tools
{
    /// <summary>
    /// the whole purpose of this class is to load an xml file and store it into the grid
    /// as well as save a grid into an xml file
    /// 
    /// </summary>
    /// 
    [XmlRoot("NodeCollection")]
    public class XMLGridLoaderAndSaver
    {
        #region class variables
        private int gridSizeX;
        private int gridSizeY;
        private float _radius;

        [XmlArray("grid"), XmlArrayItem("node")]
        public List<Node> grid = new List<Node>();
        #endregion


        #region class properties
        public int GridSizeX
        {
            get { return gridSizeX; }
            set { gridSizeX = value; }
        }

        public int GridSizeY
        {
            get { return gridSizeY; }
            set { gridSizeY = value; }
        }

        public float Radius
        {
            get { return _radius; }
            set { _radius = value; }
        }

        #endregion

        #region class functions


        public void Save(string path)
        {
            var serializer = new XmlSerializer(typeof(XMLGridLoaderAndSaver));
            using (var stream = new FileStream(path, FileMode.Create))
            {
                serializer.Serialize(stream, this);
            }
        }

        public static XMLGridLoaderAndSaver Load(string path)
        {
            var serializer = new XmlSerializer(typeof(XMLGridLoaderAndSaver));
            using (var stream = new FileStream(path, FileMode.Open))
            {
                return serializer.Deserialize(stream) as XMLGridLoaderAndSaver;
            }
        }

        public static XMLGridLoaderAndSaver LoadFromText(string text)
        {
            var serializer = new XmlSerializer(typeof(XMLGridLoaderAndSaver));
            return serializer.Deserialize(new StringReader(text)) as XMLGridLoaderAndSaver;
        }
        #endregion
    }
}
