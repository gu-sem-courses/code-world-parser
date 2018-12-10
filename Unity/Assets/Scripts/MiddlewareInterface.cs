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
    // since unity doesnt allow functions that are called from the engine to have 
    // more than one simple parameter (by unity standard)
    public void SetProj(Text name)
    {
        project = name;
        
    }
    
    // This function is the first chain in the process to get the neccesarry JSON to the computer
    public void GetJson (Text name) {

        // Defining some variables
        string NodePath = UnityEngine.Application.dataPath + "/../middleware/Client2/Client2/bin/Debug/Client2.exe";
        var User = name.text.ToString();
        var Repository = project.text.ToString();
        
        // Making a Proccess that will call on a file and also pass along arguments to that file.
        Process FirstNode = new Process();
        try
        {
            // Declares where to find the .exe file
            FirstNode.StartInfo.FileName = NodePath;
            // Declares the argument the .exe file will take 
            FirstNode.StartInfo.Arguments = User +" "+ Repository;
            FirstNode.Start();
            FirstNode.WaitForExit();
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
