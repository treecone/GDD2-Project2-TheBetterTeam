using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

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
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            GM.ForwardTime();
            mObject.ApplyTime(Vector2.left);
            jumped = false;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            GM.ForwardTime();
            mObject.ApplyTime(Vector2.right);
            jumped = false;
        }

        //Player Gravity
        if (!jumped)
        {
            PlayerGravity();
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
