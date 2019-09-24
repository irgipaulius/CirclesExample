using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolSelector : MonoBehaviour, IPointerClickHandler
{
    public ToolController.Tool toolType = ToolController.Tool.None;
    public bool interactable = true;
    public ToolHandler ToolHandler;

    public GameObject NoneGraphic;
    public GameObject CirclesGraphic;
    public GameObject SectorsGraphic;
    public GameObject SpinGraphic;
    public GameObject LinksCWGraphic;
    public GameObject LinksCCWGraphic;
    public GameObject PlayGraphic;

    private GameObject toolGraphic;



    public void OnPointerClick(PointerEventData eventData)
    {
        if (interactable)
        {
            if (toolType == ToolController.Tool.Play){
                ToolHandler.gameObject.GetComponent<PlayModeController>().ChangeToolToPlayMode();
            } else {
                ToolHandler.ChangeTool(toolType);
            }
        }
    }


    public void ChangeToolType(ToolController.Tool newTool)
    {
        if (toolType != newTool)
        {
            if (toolGraphic)
            {
                Destroy(toolGraphic);
            }

            switch (newTool)
            {
                case ToolController.Tool.None:
                    toolGraphic = null;
                    break;
                case ToolController.Tool.Circles:
                    toolGraphic = Instantiate(CirclesGraphic, transform);
                    break;
                case ToolController.Tool.Sectors:
                    toolGraphic = Instantiate(SectorsGraphic, transform);
                    break;
                case ToolController.Tool.Spin:
                    toolGraphic = Instantiate(SpinGraphic, transform);
                    break;
                case ToolController.Tool.LinksCW:
                    toolGraphic = Instantiate(LinksCWGraphic, transform);
                    break;
                case ToolController.Tool.LinksCCW:
                    toolGraphic = Instantiate(LinksCCWGraphic, transform);
                    break;
                case ToolController.Tool.Play:
                    toolGraphic = Instantiate(PlayGraphic, transform);
                    break;
                default:
                    Debug.LogError("Tool unrecognized");
                    break;
            }

            toolType = newTool;
        }
    }

}
