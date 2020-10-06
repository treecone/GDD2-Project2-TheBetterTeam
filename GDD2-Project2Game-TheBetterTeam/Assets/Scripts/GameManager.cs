using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> moveableObjects;

    void Start()
    {
        moveableObjects = new List<GameObject>(); // Otherwise there's a warning -_-
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
        //update phase blocks
        foreach (GameObject obj in FindObjectsOfType<GameObject>())
        {
            if (obj.GetComponent<PhaseBlock>() != null)
            {
                obj.GetComponent<PhaseBlock>().PhaseCount++;
                if (obj.GetComponent<PhaseBlock>().PhaseCount < 3)
                {
                    obj.GetComponent<SpriteRenderer>().color = Color.gray;
                }
                else if (obj.GetComponent<PhaseBlock>().PhaseCount >= 4)
                {
                    obj.GetComponent<SpriteRenderer>().color = Color.black;
                    obj.GetComponent<PhaseBlock>().PhaseCount = 0;
                }
                else
                {
                    obj.GetComponent<SpriteRenderer>().color = Color.black;
                }

            }
        }
    }

    public void AddMoveableObject(GameObject newObject)
    {
        if (moveableObjects != null && newObject.GetComponent<MoveableObject>() != null)
        {
            moveableObjects.Add(newObject);
        }
    }
}
