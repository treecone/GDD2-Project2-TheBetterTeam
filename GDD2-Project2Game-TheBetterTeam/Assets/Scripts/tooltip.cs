using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tooltip : MonoBehaviour
{
    public int radius;
    public bool startShown;

    bool showing;
    float opacity;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        showing = startShown;
        if (startShown)
            opacity = 0;
        else
            opacity = 255;
    }

    // Update is called once per frame
    void Update()
    {
        showing = (player.transform.position - transform.position).magnitude < radius;
            
        if(showing)
        {
            opacity = Mathf.Min(1, opacity + .025f);
        }
        else
        {
            opacity = Mathf.Max(0, opacity - .025f);
        }
        this.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, opacity);
    }
}
