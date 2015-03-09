

using System.Collections;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class xmlTest
{

    private string firstName;
    private string MI;
    private string _lastName;
    private Vector3 v;

    public xmlTest()
    {
    }
    public string FirstName
    {
        get { return firstName; }
        set { firstName = value; }
    }
    public string Mi
    {
        get { return MI; }
        set { MI = value; }
    }
    public string LastName
    {
        get { return _lastName; }
        set { _lastName = value; }
    }
    public Vector3 V
    {
        get { return v; }
        set { v = value; }
    }






}
