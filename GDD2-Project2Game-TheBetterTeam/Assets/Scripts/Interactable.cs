using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : EntityTile
{
    public bool isFocused;

    void Update()
    {
        //highlight focused object
        if (isFocused)
        {
            GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    //when clicked, focus this and unfocus all others
    private void OnMouseDown()
    {
        bool wasFocused = isFocused;
        GameObject[] all = FindObjectsOfType<GameObject>();
        foreach (GameObject go in all)
        {
            if (go.GetComponent<Interactable>() != null)
            {
                go.GetComponent<Interactable>().isFocused = false;
            }
        }
        isFocused = !wasFocused;
        //set player's focused object 
        if (isFocused)
        {
            GameObject.Find("Player").GetComponent<Player>().FocusedObject = this.gameObject;
        }
        else
        {
            GameObject.Find("Player").GetComponent<Player>().FocusedObject = null;
        }
    }

    public override void CollectObject()
    {
        Debug.Log("Tried to collect a non-collectible object: " + gameObject.name);
    }
}
