using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleLoader : MonoBehaviour
{
    public void LoadNewCircles(Circle[] newCircles){
        Slot[] slots = transform.GetComponent<ToolHandler>().GetAllSlots();
        foreach(Slot slot in slots){
            slot.RemoveCircle(false);
            foreach(Circle circle in newCircles){
                if (circle.XIndex == slot.IndexX && circle.YIndex == slot.IndexY){
                    slot.AddCircle(circle);
                    break;
                }
            }
        }
        // this firing separately, because all circles need to be instantiated before forming links
        foreach(Slot slot in slots){
            slot.DrawArrows();
            slot.DrawMarkers();
        }
    }

    public void SetCirclesPositionsToAssembled(){
        Slot[] slots = transform.GetComponent<ToolHandler>().GetAllSlots();
        foreach(Slot slot in slots){
            if(slot.circleExists){
                slot.circleController.setPositionToAssembled();
            }
        }
    }
}
