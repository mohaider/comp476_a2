using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;


[XmlRoot("Grid")]
public class XmlGridTest {
    [XmlArray("grid"), XmlArrayItem("node")]
    public List<xmlTest> grid = new List<xmlTest>();
	// Use this for initialization
    public void Save(string path)
    {
        var serializer = new XmlSerializer(typeof(XmlGridTest));
        using (var stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, this);
        }
    }

    public static XmlGridTest Load(string path)
    {
        var serializer = new XmlSerializer(typeof(XmlGridTest));
        using (var stream = new FileStream(path, FileMode.Open))
        {
            return serializer.Deserialize(stream) as XmlGridTest;
        }
    }

    public static XmlGridTest LoadFromText(string text)
    {
        var serializer = new XmlSerializer(typeof(XmlGridTest));
        return serializer.Deserialize(new StringReader(text)) as XmlGridTest;
    }
}
