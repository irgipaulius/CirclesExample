using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleDataExtracter : MonoBehaviour
{

    public string GetCircleDataString(){
        string data = FormatCirclesToString(GetCircleInstances());
        return data;
    }
    private List<Circle> GetCircleInstances(){
        Slot[] slots = transform.GetComponent<ToolHandler>().GetAllSlots();
        List<Circle> result = new List<Circle>();
        foreach(Slot slot in slots){
            if (slot.circleExists){
                result.Add(slot.circleController.GetCircle());
            }
        }
        return result;
    }

    private string FormatCirclesToString(List<Circle> circles){
        string[] result = new string[circles.Count];
        for(int i = 0; i < circles.Count; i++){
            result[i] = circles[i].FormatCircleToString();
        }
        return string.Join("\n", result);
    }

    public void LoadCircleDataFromString(string data){
        if (data != ""){
            LoadCircleDataFromString(data.Split('\n'));
        }
    }

    public void LoadCircleDataFromString(string[] data){
        Circle[] newCircles = new Circle[data.Length];
        for(int i = 0; i < data.Length; i++){
            if (data[i] == ""){
                continue;
            }
            string[] values = data[i].Split(',');

            int X = int.Parse(values[0][0] + "");
            int Y = int.Parse(values[0][1] + "");

            int position = int.Parse(values[1]);
            int sectors = int.Parse(values[2]);

            string[] linkStrings = values[3].Split(';');
            List<Circle.Link> Links = new List<Circle.Link>();
            foreach(string link in linkStrings){
                if (link != ""){
                    Circle.Link newLink = new Circle.Link();
                    newLink.direct = (link[0] == '1' ? true : false);
                    newLink.ID = link[1] + "" + link[2];
                    Links.Add(newLink);
                }
            }

            newCircles[i] = new Circle(X, Y);
            newCircles[i].sectors = sectors;
            newCircles[i].position = position;
            newCircles[i].Links = Links;
            // newCircles[i].PrintCircleData();
        }
        gameObject.GetComponent<CircleLoader>().LoadNewCircles(newCircles);   
    }

    
}
