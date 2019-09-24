using UnityEngine;

public class ToolHandler : MonoBehaviour
{
    public ToolController.Tool currentTool = ToolController.Tool.None;
    public ToolSelector InfoPanelToolGraphicObject;


    private Slot selectedSlot = null;

    public void ChangeTool(ToolController.Tool newTool){
        currentTool = newTool;
        Slot[] slots = transform.GetComponentsInChildren<Slot>(false);
        for (int i = 0; i < slots.Length; i++){
            slots[i].ChangeTool(newTool);
        }
        SelectSlot(-1,-1); //setting null selection
        InfoPanelToolGraphicObject.ChangeToolType(newTool);
    }


    public bool SelectSlot(int X, int Y){
        if (selectedSlot != null){
            if (currentTool == ToolController.Tool.LinksCCW || currentTool == ToolController.Tool.LinksCW ){
                // if links
                selectedSlot.EstablishLink(FindSlot(X,Y));
                X = -1;
                Y = -1;
            }
            selectedSlot.Unselect();
        }
        // set new instance
        selectedSlot = FindSlot(X,Y);
        return selectedSlot == null ? false : true;
    }

    public Slot FindSlot(int X, int Y){
        Slot result = null;
        Slot[] slots = transform.GetComponentsInChildren<Slot>(false);
        foreach(Slot slot in slots){
            if (slot.IndexX == X && slot.IndexY == Y){
                result = slot;
                break;
            }
        }

        return result;
    }

    public Slot FindSlot(string ID){
        if (ID.Length == 2){
            int X = int.Parse(ID[0].ToString());
            int Y = int.Parse(ID[1].ToString());
            return FindSlot(X,Y);
        } else {
            return null;
        }
    }

    public Slot[] GetAllSlots(){
        return transform.GetComponentsInChildren<Slot>(false);
    }
}
