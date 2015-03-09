using UnityEngine;
using System.Collections;

public class crax : MonoBehaviour
{
    public string str;
	// Use this for initialization
	void Start ()
	{
	    str =removeDupes(str);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public static string removeDupes(string str)
    {
        if (str == null)
            return str;
        int len = str.Length;
        if (len < 2)
            return str;
        int tail = 1;

        for (int i = 1; i < len; ++i)
        {
            int j;
            for (j = 0; j < tail; ++j)
            {
                if (str[i] == str[j])
                {
                   
                    break;
                }
            }
            if (j == tail)
            {
                char[] c=
                str.ToCharArray();
                c[tail] = str[i];
                str = new string(c);
                ++tail;
            }
        }
        char[] carray =
               str.ToCharArray();
        carray[tail] = '0';
        str = new string(carray);
        return str;
    }
}
