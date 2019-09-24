using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle
{
    public string ID;
    public int XIndex;
    public int YIndex;

    public int sectors;
    public int position;

    public List<Link> Links;

    public struct Link{
        public bool direct; // true -> linked circle will turn the same way    // false -> linked circle will turn the other way
        public string ID; // this is an id of the AFFECTED OBJECT
    }

    public void UpdatePosition(float eulerZ){
        position = Mathf.RoundToInt(( eulerZ * sectors ) / 360.0f);
        if (position > sectors){
            position -= sectors;
        }
    }

    public void PrintCircleData(){
        string linksString = "";
        foreach(Link l in Links){
            linksString += (l.direct ? "CW" : "CCW") + l.ID + ", ";
        }
        Debug.Log("Circle " + XIndex + ";" + YIndex + " position: " + position + "/" + sectors + ". Links with: " + linksString);
    }

    public Circle(int X, int Y){
        XIndex = X;
        YIndex = Y;

        ID = (string)(XIndex + "" + YIndex);

        Links = new List<Link>();
    }

    public string FormatCircleToString(){
        List<string> linksStrings = new List<string>();
        foreach(Circle.Link link in Links){
            linksStrings.Add((link.direct ? "1" : "0") + link.ID);
        }

        return ID + "," + position.ToString() + "," + sectors.ToString() + "," + string.Join(";", linksStrings);
    }

    public void addLink(int X, int Y, bool direct){
        Link newLink = new Link();
        newLink.ID = (string)(X + "" + Y);
        newLink.direct = direct;
        Links.Add(newLink);
        // check for repeated links
        for(int i = 0; i < Links.Count; i++ ){
            for(int l = 0; l < Links.Count; l++ ){
                if (i != l && Links[i].ID == Links[l].ID){
                    // if there is another link with same ID, delete older one
                    Links.RemoveAt(i);
                }
            }
        }
    }


    public List<string> getLinkedIDs(){
        List<string> result = new List<string>();
        foreach(Link link in Links){
            result.Add(link.ID);
        }
        return result;
    }

    public void deleteLinks() {
        Links = new List<Link>();
    }

    public string[] getLinkedCircleIDs(){
        string[] result = new string[Links.Count];
        for(int i = 0; i < Links.Count; i++){
            result[i] = Links[i].ID;
        }
        return result;
    }

    public Color GetColor(){
        float colorValue = 1.0f / 6.0f * (YIndex + 3);
        float colorValueSecond = 1.0f / 6.0f * (6 - YIndex);
        // float colorValueInv = 1.0f / 4.0f * (3 - Y);
        switch(XIndex){
            case 0:{
                return new Color(colorValue, colorValueSecond, 0, 1);
                }
            case 1:{
                return new Color(0, colorValue, colorValueSecond, 1);
                }
            case 2:{
                return new Color(colorValueSecond, 0, colorValue, 1);
                }
            default:{
                return new Color(0,0,0,1);
            }
        }
    }

    
}
