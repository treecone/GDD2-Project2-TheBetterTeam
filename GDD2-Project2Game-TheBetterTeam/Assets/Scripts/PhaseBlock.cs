using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseBlock : EntityTile
{
    public int PhaseCount = 0;
    public int PhaseCountMax = 1;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
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

    public override void CollectObject()
    {
        Debug.LogError("You tried to collect a non-collectable object...");
    }
}
