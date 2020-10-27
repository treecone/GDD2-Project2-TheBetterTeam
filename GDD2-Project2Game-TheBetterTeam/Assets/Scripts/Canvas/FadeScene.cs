using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScene : MonoBehaviour
{
    private SpriteRenderer sprite;
    public float start, end, current;

    void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
        sprite.color = new Color(0, 0, 0, current);
        if (start < end)
        {
            current += 0.01f;
            //if (current >= end)
                //Destroy(gameObject);
        }
        else
        {
            current -= 0.01f;
            if (current <= end)
                Destroy(gameObject);
        }
    }

    public void Init(float start, float end)
    {
        current = start;
        this.start = start;
        this.end = end;
    }

    public void OnLevelWasLoaded(int level)
    {
        Destroy(gameObject);
    }
}
