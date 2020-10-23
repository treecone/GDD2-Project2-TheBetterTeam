using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasMeteor : MonoBehaviour
{
    public Sprite collected, empty;

    public void Collect()
    {
        gameObject.GetComponent<Image>().sprite = collected;
    }
}
