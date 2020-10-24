using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasMeteor : MonoBehaviour
{
    public Sprite collected, empty;

    public void SetCollectionStatus(bool isCollected)
    {
        if (isCollected)
            gameObject.GetComponent<Image>().sprite = collected;
        else
            gameObject.GetComponent<Image>().sprite = empty;
    }
}
