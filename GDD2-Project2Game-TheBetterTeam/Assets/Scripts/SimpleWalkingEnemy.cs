using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleWalkingEnemy : Enemy
{
    [SerializeField]
    private EnemyDirection enemyDirection;

    // Does this enemy not walk on ledges?
    [SerializeField]
    private bool isLedgeAvoidant = true;

    // Is this enemy stuck walking on the ground?
    [SerializeField]
    private bool isGrounded = true;

    private Vector2 enemyDirectionVector;

    protected override void Start()
    {
        base.Start();

        enemyDirectionVector = VectorFromEnemyDirection(enemyDirection);
    }

    public override void ApplyTime(Vector2 direction)
    {
        // Apply gravity if this enemy is grounded
        if (isGrounded)
        {
            ApplyDirection(Vector2.down);
        }

        // Avoids walking off of ledges if enabled
        if (isGrounded && isLedgeAvoidant)
        {
            if (IsOnLedge())
            {
                FlipEnemy();
            }
        }

        // If this enemy is solid, turn around at walls
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
