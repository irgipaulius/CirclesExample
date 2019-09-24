using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ArrowController : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Material directArrowMaterial;
    public Material reverseArrowMaterial;
    private ToolShed toolShed;

     

    void Start()
    {
        toolShed = transform.parent.parent.GetComponent<ToolShed>();
    }

    private CircleController circleController{
        get {
            try{
                return toolShed.GetParentSlot().circleController;
            } catch {
                return null;
            }
        }
    }

    public void drawArrow(CircleController toCircle, bool direct){
        Vector3 ArrowOrigin = circleController.transform.position;
        Vector3 ArrowTarget = toCircle.transform.position;

        GameObject myLine = Instantiate(arrowPrefab, transform, false);
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = direct ? directArrowMaterial : reverseArrowMaterial;
        lr.SetPosition(0, Vector3.Lerp(ArrowOrigin, ArrowTarget, 0.1f));
        lr.SetPosition(1, Vector3.Lerp(ArrowOrigin, ArrowTarget, 0.9f));
    }

    public void deleteArrows(){
        for(int i = 0; i < transform.childCount; i++){
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
