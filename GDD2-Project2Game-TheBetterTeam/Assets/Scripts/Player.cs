using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public GameObject FocusedObject;
    private MoveableObject mObject;
    private GameManager GM;
    private bool jumped;
    // Start is called before the first frame update
    void Start()
    {
        mObject = gameObject.GetComponent<MoveableObject>();
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Input
        if (Input.GetKeyDown(KeyCode.W) && jumped == false)
        {
            GM.ForwardTime();
            mObject.ApplyTime(Vector2.up);
            jumped = true;
            //move focused object
            if (FocusedObject != null)
            {
                FocusedObject.GetComponent<MoveableObject>().ApplyTime(Vector2.up);
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            GM.ForwardTime();
            mObject.ApplyTime(Vector2.left);
            jumped = false;
            //move focused object
            if (FocusedObject != null)
            {
                FocusedObject.GetComponent<MoveableObject>().ApplyTime(Vector2.left);
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            GM.ForwardTime();
            mObject.ApplyTime(Vector2.right);
            jumped = false;
            //move focused object
            if (FocusedObject != null)
            {
                FocusedObject.GetComponent<MoveableObject>().ApplyTime(Vector2.right);
            }
        }

        //Player Gravity
        if (!jumped)
        {
            PlayerGravity();
            //move focused object
            if (FocusedObject != null)
            {
                FocusedObject.GetComponent<MoveableObject>().ApplyTime(Vector2.down);
            }
        }
    }

    void PlayerGravity()
    {
        if (!mObject.checkDirection(Vector2.down))
        {
            GM.ForwardTime();
            mObject.ApplyTime(Vector2.down);
        }
    }
}
