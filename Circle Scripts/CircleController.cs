using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleController : MonoBehaviour
{
    public int XIndex;
    public int YIndex;

    private Circle instance;

    private CircleAppearance appearanceScript;


    public void init(int X, int Y){
        appearanceScript = transform.Find("Background Image").GetComponent<CircleAppearance>();

        XIndex = X;
        YIndex = Y;

        instance = new Circle(X, Y);
        instance.sectors = 3;
        instance.XIndex = XIndex;
        instance.YIndex = YIndex;

        appearanceScript.setCircleColor(instance.GetColor());
        setSectors(instance.sectors);
    }

    public void replaceCircle(Circle newCircle){
        init(newCircle.XIndex, newCircle.YIndex);
        setSectors(newCircle.sectors);
        setPosition(newCircle.position);
        setLinks(newCircle.Links);
    }

    public void setLinks(List<Circle.Link> links){
        instance.Links = links;
    }

    public List<Circle.Link> GetLinks(){
        return instance.Links;
    }

    public string[] GetLinkedCircleIDs(){
        return instance.getLinkedCircleIDs();
    }

    public Circle GetCircle(){
        return instance;
    }

    public void setPosition(int pos){
        appearanceScript.setBackgroundPosition(pos * 360.0f / instance.sectors);
        instance.UpdatePosition(appearanceScript.getBackgroundEulerAngles().z);
    }

    public void setPositionToAssembled(){
        setPosition(instance.sectors / 2);
    }

    public bool isAssembled(){
        return (instance.position == instance.sectors / 2);
    }

    public void setSectors(Slider slider){
        setSectors((int)slider.value);
    }
    public void setSectors(int amount){
        if (amount != instance.sectors){
            appearanceScript.destroySectors();
        }
        instance.sectors = amount;
        instance.position = 0;
        appearanceScript.setBackgroundPosition(0.0f);
        appearanceScript.drawSectors(amount);
        transform.Find("Sector Count").GetComponent<Text>().text = instance.sectors.ToString();
    }
    

    public void SpinCircle(bool clockwise){
        int nextPosition = 0;
        if (clockwise){
            nextPosition = ClampToRange(instance.position - 1, 0, instance.sectors);
        } else {
            nextPosition = ClampToRange(instance.position + 1, 0, instance.sectors);
        }
        appearanceScript.setBackgroundPosition(nextPosition * 360.0f / instance.sectors);
        instance.UpdatePosition(appearanceScript.getBackgroundEulerAngles().z);
    }

    public void DrawMarkers(List<Circle> circles){
        List<Color> colors = new List<Color>();
        foreach(Circle circle in circles){
            colors.Add(circle.GetColor());
        }
        transform.Find("Markers").GetComponent<MarkerScript>().DrawMarkers(colors);
    }

    public void DeleteMarkers(){
        transform.Find("Markers").GetComponent<MarkerScript>().DeleteMarkers();
    }

    private static int ClampToRange(int number, int min, int max){
        int result;
        int diff = max - min;
         if (number < min){
             result = number + diff;
         } else if (number >= max){
             result = number - diff;
         } else {
             result = number;
         }
         return result;
    }

}
