using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
public class xmlwritertest : MonoBehaviour {

	// Use this for initialization
	void Start () {
    List<xmlTest> testList = new List<xmlTest>();

	    for (int i = 0; i < 500; i++)
	    {
	        xmlTest adder = new xmlTest();
            adder.FirstName = "first name tester " + i;
            adder.Mi= "middle initial tester " + i;
            adder.LastName= "last name tester " + i;
	        adder.V = UnityEngine.Random.insideUnitSphere;
            testList.Add(adder);

	    }
	    XmlGridTest gridtester = new XmlGridTest();
	    gridtester.grid = testList;
	    gridtester.Save("Test.xml");

        XmlGridTest gridLoader = new XmlGridTest();
	    gridLoader = XmlGridTest.Load("Test.xml");
        print(gridLoader.grid[22].FirstName);
	}


	// Update is called once per frame
	void Update () {
	
	}
}
