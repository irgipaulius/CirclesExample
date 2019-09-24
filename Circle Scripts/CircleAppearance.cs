using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleAppearance : MonoBehaviour
{
    
    public GameObject sectorDivider;

    public void setCircleColor(Color color){
        gameObject.GetComponent<Image>().color = color;
    }

    public void setBackgroundPosition(float angle){
        transform.eulerAngles = new Vector3(0,0,angle);
    }

    public void drawSectors(int amount){
        for(int i = 0; i < amount; i++){
            float angle = 360.0f / amount * i;
            drawSector(angle);
        }
    }

    public void drawSector(float angle) {
        GameObject divider = Instantiate(sectorDivider, transform.Find("Sectors"));
        divider.transform.eulerAngles = new Vector3(0.0f, 0.0f, angle);
    }

    public void destroySectors(){
        Transform sectorsObject = transform.Find("Sectors");
        for(int i = 0; i < sectorsObject.childCount; i++){
            Destroy(sectorsObject.GetChild(i).gameObject);
        }
    }

    public Vector3 getBackgroundEulerAngles(){
        return transform.eulerAngles;
    }
}
