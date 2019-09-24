using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarkerScript : MonoBehaviour
{
    public GameObject MarkerPrefab;
    
    public void DrawMarkers(List<Color> colors){
        for(int i = 0; i < colors.Count; i++){
            GameObject marker = Instantiate(MarkerPrefab, transform, false);
            marker.GetComponent<Image>().color = colors[i];
            marker.transform.eulerAngles = new Vector3(0,0, 360.0f / (float)colors.Count * (float)i);
        }
    }

    public void DeleteMarkers(){
        for(int i = 0; i < transform.childCount; i++){
            Destroy(transform.GetChild(i).gameObject);
        }
    }


}
