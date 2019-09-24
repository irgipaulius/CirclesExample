using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FilesController : MonoBehaviour
{

    public FileHandlerForScrollView ScrollViewHandler;
    public CircleDataExtracter circleData;


    public void OpenPrompt(){
        gameObject.SetActive(true);
        ScrollViewHandler.UpdateScrollViewItems();
    }
    public void ClosePrompt(){
        gameObject.SetActive(false);
    }

    public void SaveProgressToFile(string path){
        print("saving to " + path);
        File.WriteAllText(path, circleData.GetCircleDataString());
    }

    public void LoadFromFile(string path){
        print("loading from " + path);
        string[] data = File.ReadAllLines(path);
        circleData.LoadCircleDataFromString(data);
    }

    public bool CheckFileExistance(string filename){
        return File.Exists(filename);
    }

}
