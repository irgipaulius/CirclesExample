using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolShed : MonoBehaviour
{
    private Slot slot;

    private GameObject addNewCircleButton;
    private GameObject removeCircleButton;

    private GameObject sectorSlider;

    private GameObject spinToolObject;
    
    private GameObject playToolObject;
    private GameObject playSelectionObject;
    private GameObject playArrowCWObject;
    private GameObject playArrowCCWObject;
    private GameObject playArrowIndicatorCWObject;
    private GameObject playArrowIndicatorCCWObject;

    private GameObject linkToolObject;
    private GameObject linkDeleteObject;
    private GameObject linkHighlightCWObject;
    private GameObject linkHighlightCCWObject;


    private bool selected = false;

    public ArrowController Arrows {
        get {
            return linkToolObject.transform.Find("Arrows").GetComponent<ArrowController>();
        }
    }

    void Start()
    {
        slot = transform.parent.GetComponent<Slot>();
        
        linkToolObject = transform.Find("LinkTool").gameObject;
        linkDeleteObject = linkToolObject.transform.Find("Delete").gameObject;
        linkHighlightCWObject = linkToolObject.transform.Find("HighlightCW").gameObject;
        linkHighlightCCWObject = linkToolObject.transform.Find("HighlightCCW").gameObject;

        playToolObject = transform.Find("PlayTool").gameObject;
        playSelectionObject = playToolObject.transform.Find("Selection").gameObject;
        playArrowCWObject = playToolObject.transform.Find("CW").gameObject;
        playArrowCCWObject = playToolObject.transform.Find("CCW").gameObject;
        playArrowIndicatorCWObject = playToolObject.transform.Find("Spin Marker CW").gameObject;
        playArrowIndicatorCCWObject = playToolObject.transform.Find("Spin Marker CCW").gameObject;

        addNewCircleButton = transform.Find("CircleSpawner").Find("Add").gameObject;
        removeCircleButton = transform.Find("CircleSpawner").Find("Remove").gameObject;

        sectorSlider = transform.Find("SectorTool").Find("Slider").gameObject;

        spinToolObject = transform.Find("SpinTool").gameObject;

        ResetTools();
    }

    public Slot GetParentSlot(){
        return slot;
    }

    public void ChangeTool(ToolController.Tool newTool){
        ResetTools();
        SetTool(newTool);
    }

    private void ResetTools(){
        SetCircleToolActive(false, false);
        SetSectorsToolActive(false);
        SetSpinToolActive(false);
        SetPlayToolActive(false);
        SetLinkToolActive(false, true);
        SetLinkArrowIndicator(false, true);
    }

    public Circle GetCircleInstance(){
        try{
            return slot.circleController.GetCircle();
        } catch {
            return null;
        }
    }

    private void SetTool(ToolController.Tool tool){
        if (ToolController.Tool.Circles == tool){
            SetCircleToolActive(true, false);
        } else if(ToolController.Tool.Sectors == tool){
            SetSectorsToolActive(true);
        } else if(ToolController.Tool.Spin == tool){
            SetSpinToolActive(true);
        } else if(ToolController.Tool.Play == tool){
            SetPlayToolActive(true);
        } else if(ToolController.Tool.LinksCW == tool){
            SetLinkToolActive(true, true);
        } else if(ToolController.Tool.LinksCCW == tool){
            SetLinkToolActive(true, false);
        } else if (ToolController.Tool.None == tool){

        } else {
            Debug.LogError("Unrecognized tool ?? " + tool);
        }
    }

    private void SetSpinToolActive(bool enable){
        if(enable && slot.circleExists){
            spinToolObject.SetActive(true);
        } else {
            spinToolObject.SetActive(false);
        }
    }

    private void SetSectorsToolActive(bool enable){
        if (enable && slot.circleExists){
            sectorSlider.SetActive(true);
            SetSliderValueSilent(GetCircleInstance().sectors);
        } else {
            sectorSlider.SetActive(false);
        }
    }
    private void SetSliderValueSilent(int value){
        sectorSlider.GetComponent<Slider>().SetValueWithoutNotify(value);
    }
    
    public void SetCircleToolActive(bool enable, bool ignoreCircleObj){
        if (enable){
            if (!slot.circleExists || ignoreCircleObj){
                // if circle is not in the slot, or it should be ignored, then display AddNewCircle button only
                addNewCircleButton.SetActive(true);
                removeCircleButton.SetActive(false);
            } else {
                // if circle is in the slot, and it shouldn't be ignored, then display RemoveCircle button only
                addNewCircleButton.SetActive(false);
                removeCircleButton.SetActive(true);
            }
        } else {
            addNewCircleButton.SetActive(false);
            removeCircleButton.SetActive(false);
        }
    }

    public void SetPlayToolActive(bool enable){
        if(enable && slot.circleExists){
            if (selected){
                playToolObject.SetActive(true);
                playArrowCCWObject.SetActive(true);
                playArrowCWObject.SetActive(true);
                playSelectionObject.SetActive(false);
            } else {
                playToolObject.SetActive(true);
                playArrowCCWObject.SetActive(false);
                playArrowCWObject.SetActive(false);
                playSelectionObject.SetActive(true);
            }
        } else {
            playToolObject.SetActive(false);
            playArrowCCWObject.SetActive(false);
            playArrowCWObject.SetActive(false);
            playSelectionObject.SetActive(false);
        }
    }

    public void SetLinkToolActive(bool enable, bool direct) {
        linkDeleteObject.SetActive(false);
        linkHighlightCWObject.SetActive(false);
        linkHighlightCCWObject.SetActive(false);
        if(enable && slot.circleExists){
                linkToolObject.SetActive(true);
            if (selected){
                if (direct){
                    linkHighlightCWObject.SetActive(true);
                    linkHighlightCCWObject.SetActive(false);
                } else {
                    linkHighlightCWObject.SetActive(false);
                    linkHighlightCCWObject.SetActive(true);
                }
            } 
            if (slot.circleController.GetCircle().Links.Count > 0){
                linkDeleteObject.SetActive(true);
            }
        } else {
            linkToolObject.SetActive(false);
        }
    }

    public void SetLinkArrowIndicator(bool enable, bool direct){
        playArrowIndicatorCWObject.SetActive(false);
        playArrowIndicatorCCWObject.SetActive(false);
        if (enable){
            if (direct){
                playArrowIndicatorCWObject.SetActive(true);
            } else {
                playArrowIndicatorCCWObject.SetActive(true);
            }
        }
    }

    public void Select(bool select, ToolController.Tool onTool){
        selected = select;
        SetTool(onTool);
    }


}
