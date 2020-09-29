using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObject : MonoBehaviour
{
    void Start()
    {
        
    }

    IEnumerator Move (Vector2 direction, float deltaTime)
    {
        Vector2 endPosition = (Vector2)transform.position + direction;
        float newTime = Time.time;
        while (Time.time < newTime + deltaTime)
        {
            transform.position = Vector2.Lerp(transform.position, endPosition, (Time.time - newTime) / deltaTime);
            yield return null;
        }
        transform.position = endPosition;
    }

    public void ApplyTime (Vector2 direction)
    {

        //Movement
        if(direction != Vector2.zero)
        {
            if(!CheckCollision(direction))
            {
                StartCoroutine(Move(direction, 0.1f));
            }
        }
    }

    bool CheckCollision (Vector2 dir)
    {
        Debug.DrawRay((Vector2)transform.position + (dir * 1.05f), dir * 0.45f, Color.red);
        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position + dir*1.05f, dir, 0.45f);
        if (hit.collider != null)
        {
            return true;
        }
        return false;
    }
}
