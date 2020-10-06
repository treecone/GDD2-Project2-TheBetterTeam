using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleWalkingEnemy : Enemy
{
    [SerializeField]
    private EnemyDirection enemyDirection;

    [SerializeField]
    private bool avoidLedges = true;

    private Vector2 enemyDirectionVector;

    protected override void Start()
    {
        base.Start();

        enemyDirectionVector = VectorFromEnemyDirection(enemyDirection);
    }

    public override void ApplyTime(Vector2 direction)
    {
        // Gravity
        ApplyDirection(Vector2.down);

        // Avoids walking off a ledge, if enabled
        if (avoidLedges)
        {
            if (IsOnLedge())
            {
                FlipEnemy();
            }
        }

        // If solid, avoid walls
        if (isSolid)
        {
            if (IsAtWall())
            {
                FlipEnemy();
            }
        }

        ApplyDirection(enemyDirectionVector);
    }

    // Returns if the enemy is on a ledge.
    private bool IsOnLedge()
    {
        return !CheckTilePosition((Vector2)(gameObject.transform.position) + enemyDirectionVector + Vector2.down);
    }

    // Returns if the enemy is at a wall
    private bool IsAtWall()
    {
        return CheckTilePosition((Vector2)(gameObject.transform.position) + enemyDirectionVector);
    }

    // Flips an enemy to face / move the opposite direction
    private void FlipEnemy()
    {
        enemyDirectionVector *= -1;
        enemyDirection = EnemyDirectionFromVector(enemyDirectionVector);
    }
}
