﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public GameObject FocusedObject;
    private bool focusedJumped;

    private MoveableObject mObject;
    private GameManager GM;
    private bool jumped;

    private float playerSpriteHeight;

    // Start is called before the first frame update
    void Start()
    {
        mObject = gameObject.GetComponent<MoveableObject>();
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerSpriteHeight = gameObject.GetComponent<SpriteRenderer>().bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        //Input
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (FocusedObject == null)
            {
                if (jumped == false)
                {
                    GM.ForwardTime();
                    mObject.ApplyTime(Vector2.up);
                    jumped = true;
                }
            }
            else
            {
                if (focusedJumped == false)
                {
                    GM.ForwardTime();
                    //move focused object
                    FocusedObject.GetComponent<MoveableObject>().ApplyTime(Vector2.up);
                    focusedJumped = true;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            GM.ForwardTime();
            if (FocusedObject == null)
            {
                mObject.ApplyTime(Vector2.left);
                jumped = false;
            }
            else
            {
                //move focused object
                FocusedObject.GetComponent<MoveableObject>().ApplyTime(Vector2.left);
                focusedJumped = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            GM.ForwardTime();
            if (FocusedObject == null)
            {
                mObject.ApplyTime(Vector2.right);
                jumped = false;
            }
            else
            {
                //move focused object
                FocusedObject.GetComponent<MoveableObject>().ApplyTime(Vector2.right);
                focusedJumped = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            GM.ForwardTime();
            if (FocusedObject == null)
            {
                mObject.ApplyTime(Vector2.down);
                jumped = false;
            }
            else
            {
                //move focused object
                FocusedObject.GetComponent<MoveableObject>().ApplyTime(Vector2.down);
                focusedJumped = false;
            }
        }

        if (FocusedObject == null)
        {
            //Player Gravity
            if (!jumped)
            {
                PlayerGravity();
            }
        }
        else
        {
            //Focused Object Gravity
            if (!focusedJumped)
            {
                FocusedObjectGravity();
            }
        }

        // Handles player death if applicable
        if (CheckForDeath())
        {
            Debug.Log("Player has died!");
        }
    }

    // Checks if the player did something to die
    private bool CheckForDeath()
    {
        // Fell off screen - uses sprite height so the death happens after the player is off-screen
        if (gameObject.transform.position.y < 0 - playerSpriteHeight)
        {
            return true;
        }

        return false;
    }

    void PlayerGravity()
    {
        if (!mObject.checkDirection(Vector2.down))
        {
            GM.ForwardTime();
            mObject.ApplyTime(Vector2.down);
        }
    }

    void FocusedObjectGravity()
    {
        if (!FocusedObject.GetComponent<MoveableObject>().checkDirection(Vector2.down))
        {
            GM.ForwardTime();
            FocusedObject.GetComponent<MoveableObject>().ApplyTime(Vector2.down);
        }
    }

    public void ResetJumps()
    {
        if(FocusedObject == null)
        {
            focusedJumped = false;
        }
        else
        {
            jumped = false;
        }
    }
}
