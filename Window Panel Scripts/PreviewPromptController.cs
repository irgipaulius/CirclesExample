using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewPromptController : MonoBehaviour
{
    public GameObject BackgroundTint;
    public GameObject ErrorNoCirclesObject;
    public GameObject AssemblyPromptObject;

    private void SetActiveObject(GameObject go){
        BackgroundTint.SetActive(true);
        go.SetActive(true);
    }

    public void ShowErrorNoCircles(){
        SetActiveObject(ErrorNoCirclesObject);
    }

    public void PromptAssemblyType(){
        SetActiveObject(AssemblyPromptObject);
    }

    public void GoBack(){
        HideAllWindows();
    }

    public void HideAllWindows(){
        BackgroundTint.SetActive(false);
        ErrorNoCirclesObject.SetActive(false);
        AssemblyPromptObject.SetActive(false);
    }
}
