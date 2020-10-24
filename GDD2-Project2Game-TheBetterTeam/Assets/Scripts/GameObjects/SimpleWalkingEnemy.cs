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

    //Animation
    [SerializeField]
    private Sprite[] movementAnimations;
    [SerializeField]
    private int animationFrame;

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
        // Commented out so it doesn't crash
        //gameObject.GetComponent<SpriteRenderer>().sprite = movementAnimations[animationFrame++];
        //if (animationFrame >= movementAnimations.Length) { animationFrame = 0; }
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

        // If dead, remove from the game manager list and destroy
        if (CheckForDeath())
        {
            FindObjectOfType<GameManager>().RemoveMoveableObject(gameObject);
            Destroy(gameObject);
        }
    }

    // Returns if the enemy is on a ledge.
    private bool IsOnLedge()
    {
        return !CheckTilePosition((Vector2)(gameObject.transform.position) + enemyDirectionVector + Vector2.down);
    }

    // Returns if the enemy is at a wall
    private bool IsAtWall()
    {
        Vector2 positionToCheck = (Vector2)(gameObject.transform.position) + enemyDirectionVector;
        return (CheckTilePosition(positionToCheck) || CheckEntityPosition(positionToCheck));
    }

    // Flips an enemy to face / move the opposite direction
    private void FlipEnemy()
    {
        enemyDirectionVector *= -1;
        gameObject.GetComponent<SpriteRenderer>().flipX = !gameObject.GetComponent<SpriteRenderer>().flipX;
        enemyDirection = EnemyDirectionFromVector(enemyDirectionVector);
    }
}
