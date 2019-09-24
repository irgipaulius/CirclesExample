using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayModeController : MonoBehaviour
{
    public GameObject PlayPanel;
    public GameObject InstrumentPanel;
    public PreviewPromptController PreviewPromptObject;
    
    public void ChangeToolToPlayMode(){
        if (GetCirclesInGridCount() <= 0){
            PreviewPromptObject.ShowErrorNoCircles();
        } else {
            PreviewPromptObject.PromptAssemblyType();
        }
    }
    
    private int GetCirclesInGridCount(){
        int result = 0;
        Slot[] slots = transform.GetComponentsInChildren<Slot>();
        for(int i = 0; i < transform.childCount; i++){
            if (slots[i].circleExists){
                result++;
            }
        }
        return result;
    }

    public void SaveState(){
        // instances already loaded, no need for extra functionality
        ExitPreview();
    }

    public void ExitWithoutSavingState(){
        string data = PlayerPrefs.GetString("CircleData","");
        gameObject.GetComponent<CircleDataExtracter>().LoadCircleDataFromString(data);
        ExitPreview();
    }

    private void ExitPreview(){
        PlayPanel.SetActive(false);
        InstrumentPanel.SetActive(true);
        gameObject.GetComponent<ToolHandler>().ChangeTool(ToolController.Tool.None);
    }

    public void StartAssembled(bool assembled){
        InstrumentPanel.SetActive(false);
        PlayPanel.SetActive(true);
        gameObject.GetComponent<ToolHandler>().ChangeTool(ToolController.Tool.Play);
        PlayerPrefs.SetString("CircleData", gameObject.GetComponent<CircleDataExtracter>().GetCircleDataString());
        if (assembled){
            gameObject.GetComponent<CircleLoader>().SetCirclesPositionsToAssembled();
        }
    }

    private bool isAssembled(){
        Slot[] slots = gameObject.GetComponent<ToolHandler>().GetAllSlots();
        foreach(Slot slot in slots){
            if (slot.circleController){
                if (!slot.circleController.isAssembled()){
                    print("slot " + slot.circleController.GetCircle().ID + " is not assembled.");
                    return false;   // not everything is assembled
                }
            } 
        }
        return true;
    }

    public void checkAssembly(){
        if (PlayPanel.activeInHierarchy){
            PlayPanel.transform.Find("LightBulb").GetComponent<LightBulbIndicator>().SetAssembled(isAssembled());
        }
    }
    
}
