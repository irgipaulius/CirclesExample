using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class FileHandlerForScrollView : MonoBehaviour
{

    public GameObject ItemPrefab;

    private Transform ScrollViewContent;
    private FilesController FileController;
    private InputField InputField;
    private GameObject[] items;

    private string directoryPath;

    private GameObject filenameMissingErrorObject;
    
    private void Awake() {
        filenameMissingErrorObject = transform.parent.parent.Find("ErrorFilenameMissing").gameObject;
        filenameMissingErrorObject.SetActive(false);
        directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "Exported Data (Circles Constructor)\\");
        if (!Directory.Exists (directoryPath)) 
        {
            Directory.CreateDirectory (directoryPath);
        }
        ScrollViewContent = transform.Find("Viewport").Find("Content");
        FileController = transform.parent.parent.GetComponent<FilesController>();
        InputField = transform.parent.GetComponentInChildren<InputField>();
    }

    public void UpdateScrollViewItems(){
        filenameMissingErrorObject.SetActive(false);
        
        if (items != null){
            foreach(var item in items){
                Destroy(item);
            }
        }
        
        string[] filenames = System.IO.Directory.GetFiles(directoryPath);
        items = new GameObject[filenames.Length];
        for(int i = 0; i < filenames.Length; i++){
            filenames[i] = filenames[i].Replace(directoryPath, "").Replace(".csv", "");
            items[i] = Instantiate(ItemPrefab, ScrollViewContent);
            items[i].transform.Find("Text").gameObject.GetComponent<Text>().text = filenames[i];
        }
    }

    public void SetFilename(string filename){
        InputField.text = filename;
    }

    public void SaveUsingSetFilename(){
        if (InputField.text != ""){
            string filepath = directoryPath + InputField.text + ".csv";
            FileController.SaveProgressToFile(filepath);
            FileController.ClosePrompt();
        } else {
            filenameMissingErrorObject.SetActive(true);
        }
    }

    public void LoadUsingSetFilename(){
        if (InputField.text != ""){
            string filepath = directoryPath + InputField.text + ".csv";
            if (FileController.CheckFileExistance(filepath)){
                FileController.LoadFromFile(filepath);
                FileController.ClosePrompt();
                return;
            } 
        }

        // this error is shown when no filename is entered or no file with filename entered exists
        filenameMissingErrorObject.SetActive(true);
    }

    

}
