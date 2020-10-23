using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseBlock : EntityTile
{
    public int PhaseCount = 0;
    public int PhaseCountMax = 1;

    private SpriteRenderer spriteRenderer;
    ContactFilter2D contactFilter;

    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        contactFilter = new ContactFilter2D();
        contactFilter.useTriggers = true;
    }

    public void UpdatePhaseCount()
    {
        PhaseCount++;
        if (PhaseCount == PhaseCountMax)
        {
            isSolid = !isSolid;
            PhaseCount = 0;
        }

        if (isSolid)
        {
            spriteRenderer.color = Color.white;
        }
        else
        {
            spriteRenderer.color = new Color(0, 0, 0, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            Debug.Log("Collide with player!");
        }
    }

    private void Update()
    {
        BoxCollider2D collider2D = gameObject.GetComponent<BoxCollider2D>();
        List<Collider2D> collisions = new List<Collider2D>();
        if (collider2D.OverlapCollider(contactFilter, collisions) > 0)
        {
            foreach (Collider2D colliderResult in collisions)
            {
                if (colliderResult.GetComponent<Player>() != null)
                {
                    Debug.Log("Collision with player!");
                }
            }
        }
    }

    public override void CollectObject()
    {
        Debug.LogError("You tried to collect a non-collectable object...");
    }
}
