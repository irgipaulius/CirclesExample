using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FileItemRouter : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData){
        string filename = transform.Find("Text").GetComponent<Text>().text;
        transform.parent.parent.parent.GetComponent<FileHandlerForScrollView>().SetFilename(filename);
    }
}
