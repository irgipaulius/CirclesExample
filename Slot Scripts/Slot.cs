
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public int IndexX;
    public int IndexY;

    public GameObject CirclePrefab;

    private ToolController.Tool usingTool = ToolController.Tool.None;

    public string IDstring {
        get{
            return IndexX + "" + IndexY;
        }
    }

    public CircleController circleController {
        get {
            if (circleExists){
                return transform.Find("Circle").GetChild(0).GetComponent<CircleController>();
            } else {
                return null;
            }
        }
    }

    public bool circleExists {
        get{
            return transform.Find("Circle").childCount > 0;
        }
    }

    public ToolShed toolShed {
        get {
            return transform.Find("Tools").GetComponent<ToolShed>();
        }
    }

    public ToolHandler toolHandler {
        get {
            return transform.GetComponentInParent<ToolHandler>();
        }
    }


    public void SetSectors(Slider slider){
        circleController.setSectors(slider);
    }
    
    public void AddCircle(Circle newCircle){
        if (!circleExists){
            GameObject circleClone = Instantiate(CirclePrefab, transform.Find("Circle"));
            circleClone.GetComponent<CircleController>().replaceCircle(newCircle);

            ChangeTool(usingTool);
        } else {
            Debug.LogError("circle object is already created");
        }
    }
    public void CreateDefaultCircle(){
        if (!circleExists){
            GameObject circleClone = Instantiate(CirclePrefab, transform.Find("Circle"));
            circleClone.GetComponent<CircleController>().init(IndexX, IndexY);

            ChangeTool(usingTool);
        } else {
            Debug.LogError("circle object is already created");
        }
    }


    public void RemoveCircle(bool unlinkLinkedCircles){
        try{
            if (circleExists){
                if (unlinkLinkedCircles){
                    UnlinkCirclesLinkedToCircle();
                }
                DestroyImmediate(transform.Find("Circle").GetChild(0).gameObject);
            }
            toolShed.ChangeTool(usingTool);
            DrawArrows();
        } catch {
            Debug.Log("Error removing circle. maybe it doesnt exist?");
        }
    }

    private void UnlinkCirclesLinkedToCircle(){
        foreach(Slot slot in CheckWhoLinksWith()){
            if (slot.circleExists){
                slot.DetachLinks();
            }
        }
    }

    private List<Slot> CheckWhoLinksWith(){
        List<Slot> result = new List<Slot>();
        if (circleExists){
            Slot[] slots = toolHandler.GetAllSlots();
            foreach(Slot slot in slots){
                if (slot.IDstring != IDstring && slot.circleExists){
                    List<Circle.Link> slotLinks = slot.circleController.GetLinks();
                    if (slotLinks.Count > 0){
                        foreach(Circle.Link link in slotLinks){
                            if (link.ID == circleController.GetCircle().ID){
                                result.Add(slot);
                            }
                        }
                    }
                }
            }
        }
        return result;
    }

    public void ChangeTool(ToolController.Tool newTool){
        Unselect();
        toolShed.ChangeTool(newTool);
        if (newTool == ToolController.Tool.LinksCCW || newTool == ToolController.Tool.LinksCW) {
            DrawArrows();
        }
        usingTool = newTool;
    }

    //from spin tool buttons
    public void SpinCircleWithoutLinks(bool clockwise){
        if (circleExists){
            circleController.SpinCircle(clockwise);
        }
    }
    //from play tool buttons
    public void SpinCircle(bool clockwise){
        if (circleExists){
            circleController.SpinCircle(clockwise);
            if (circleController.GetLinks().Count > 0){
                foreach(Circle.Link link in circleController.GetLinks()){
                    bool direction = link.direct ? clockwise : !clockwise;
                    toolHandler.FindSlot(link.ID).circleController.SpinCircle(direction);
                }
            }
        }
        transform.parent.GetComponent<PlayModeController>().checkAssembly();
    }

    public void Unselect(){
        if (circleExists){
            toolShed.Select(false, usingTool);
            SetArrowIndicatorsOnLinkedCircles(false);
        }
    }
    public void Select(){
        if (toolHandler.SelectSlot(IndexX, IndexY)){
            toolShed.Select(true, usingTool);
        }
        SetArrowIndicatorsOnLinkedCircles(true);
    }
    private void SetArrowIndicatorsOnLinkedCircles(bool enable){
        List<Circle.Link> links = circleController.GetLinks();
        foreach(Circle.Link link in links){
            Slot slot = toolHandler.FindSlot(link.ID);
            if (slot != null){
                slot.toolShed.SetLinkArrowIndicator(enable, link.direct);
            } else {
                Debug.LogError("Select(): slot " + link.ID + " was not found.");
            }
        }
    }

    public void EstablishLink(Slot otherSlot){
        bool direct = usingTool == ToolController.Tool.LinksCW; // if LinkCW, then TRUE, otherwise - FALSE
        if (circleExists && otherSlot != null){
            circleController.GetCircle().addLink(otherSlot.IndexX, otherSlot.IndexY, direct);
            DrawArrows();
            DrawMarkers();
        }
    }

    public void DetachLinks(){
        if (circleExists){
            circleController.GetCircle().deleteLinks();
            toolShed.ChangeTool(usingTool);
            DrawArrows();
            DrawMarkers();
        } else {
            print("Slot " + IDstring + " does not have circle instance to detach links");
        }
    }

    public void DrawArrows(){
        toolShed.Arrows.deleteArrows();
        if (circleExists){
            List<Circle.Link> links = circleController.GetLinks();
            foreach(Circle.Link link in links){
                Slot otherSlot = toolHandler.FindSlot(link.ID);
                if (otherSlot.circleExists){
                    toolShed.Arrows.drawArrow(otherSlot.circleController, link.direct);
                } else {
                    print(otherSlot.IDstring + " doesnt have circle instance to draw an arrow to.");
                }
            }
        }
    }

    public void DrawMarkers() {
        if (circleExists){
            circleController.DeleteMarkers();
            List<Circle> circles = new List<Circle>();
            foreach(string id in circleController.GetLinkedCircleIDs()){
                Slot slot = toolHandler.FindSlot(id);
                circles.Add(slot.circleController.GetCircle());
            }
            circleController.DrawMarkers(circles);
        }
    }
    
}
