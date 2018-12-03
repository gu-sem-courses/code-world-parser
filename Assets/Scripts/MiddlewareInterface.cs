using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MiddlewareInterface : MonoBehaviour {

    // gloabl Text Variable as unity doesnt allow functions called in 
    // the UnityEngine to have more than one parameter
    Text project;
    // Use this for initialization
    void Start () {
 
    }

    // A workaround function so I can have the text objects of both inputfields
    // sense unity doesnt allow functions that are called from the engine to have 
    // more than one simple parameter
    public void SetProj(Text name)
    {
        project = name;
    }
    
    // This function is the first chain in the process to get the neccesarry JSON to the computer
    public void GetJson (Text name) {

        // Defining some variables
        string PathV = UnityEngine.Application.dataPath + "/../middleware/Client2/Client2/bin/Debug/Client2.exe";
        var User = name.text.ToString();
        var Repository = project.text.ToString();
        
        // Making a Proccess that will call on a file and also pass along arguments to that file.
        Process Client = new Process();
        try
        {
            // Declares where to find the .exe file
            Client.StartInfo.FileName = PathV;
            // Declares the argument the .exe file will take 
            Client.StartInfo.Arguments = User +" "+ Repository;
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
