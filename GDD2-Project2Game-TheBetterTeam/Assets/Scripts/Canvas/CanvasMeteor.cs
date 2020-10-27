using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasMeteor : MonoBehaviour
{
    public Sprite collected, empty;

    public void SetCollectionStatus(bool isCollected)
    {
        Vector3 v = gameObject.GetComponent<RectTransform>().localPosition;
        if (isCollected)
        {
            gameObject.GetComponent<RectTransform>().localPosition = new Vector3(v.x, 152, 0);
            gameObject.GetComponent<Image>().sprite = collected;
        }
        else
        {
            gameObject.GetComponent<RectTransform>().localPosition = new Vector3(v.x, 127, 0);
            gameObject.GetComponent<Image>().sprite = empty;
        }
    }
}
