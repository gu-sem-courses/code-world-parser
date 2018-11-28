using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MiddlewareInterface : MonoBehaviour {
    string inputField;
    // Use this for initialization
    void Start () {
 
    }

    public void GetJson (Text input) {
        string PathV = UnityEngine.Application.dataPath + "/../middleware/Client2/Client2/bin/Debug/Client2.exe";
        inputField = input.text;
        



        Process Client = new Process();
        try
        {
            Client.StartInfo.FileName = PathV;
            Client.StartInfo.Arguments = inputField.ToString();
            Client.Start();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

    }

    // Update is called once per frame
    void Update () {
		
	}
}
