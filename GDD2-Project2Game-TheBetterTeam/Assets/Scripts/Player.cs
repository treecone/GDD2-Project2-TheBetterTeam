using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private MoveableObject mObject;
    private GameManager GM;
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
        if (Input.GetKeyDown(KeyCode.W))
        {

        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            GM.ForwardTime();
            mObject.ApplyTime(Vector2.left);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {

        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            GM.ForwardTime();
            mObject.ApplyTime(Vector2.right);
        }
    }
}
