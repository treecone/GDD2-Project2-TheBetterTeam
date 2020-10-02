using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> moveableObjects;

    void Start()
    {
        GameObject[] allObj = FindObjectsOfType<GameObject>();
        foreach(GameObject moveObj in allObj)
        {
            if(moveObj.GetComponent<MoveableObject>() != null)
            {
                moveableObjects.Add(moveObj);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debugging
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ForwardTime();
        }
    }

    public void ForwardTime ()
    {
        foreach(GameObject obj in moveableObjects)
        {
            if(obj.tag != "Player") 
            {
                //Gravity
                obj.GetComponent<MoveableObject>().ApplyTime(Vector2.down);
            }
        }
    }
}
