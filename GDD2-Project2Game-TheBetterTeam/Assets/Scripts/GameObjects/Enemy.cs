using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;

public abstract class Enemy : MoveableObject
{
    ContactFilter2D contactFilter;

    float enemySpriteHeight;

    BoxCollider2D enemyCollider;

    public enum EnemyDirection
    {
        Left,
        Right,
        Up,
        Down,
        Invalid
    }

    //Animation
    [SerializeField]
    private Sprite[] walkingAnimations;
    [SerializeField]
    private Sprite falingAnimation;
    [SerializeField]
    private int animationFrame;

    protected override void Start()
    {
        base.Start();

        enemySpriteHeight = gameObject.GetComponent<SpriteRenderer>().bounds.extents.y;
        enemyCollider = gameObject.GetComponent<BoxCollider2D>();
        contactFilter = new ContactFilter2D();
        contactFilter.useTriggers = true;
    }
    // Removes enemy from the game manager's moveable object list
    private void OnDestroy()
    {
        GameManager manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        manager.RemoveMoveableObject(gameObject);
    }

    public override void ApplyTime(Vector2 direction)
    {
        if(direction == Vector2.down)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = falingAnimation;

        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = walkingAnimations[animationFrame++];
        }
        if (animationFrame >= walkingAnimations.Length)
            animationFrame = 0;
        Debug.Log("!@#");


        base.ApplyTime(direction);

    }

    // Converts an enemy direction into the appropriate Vector2
    public Vector2 VectorFromEnemyDirection(EnemyDirection direction)
    {
        switch (direction)
        {
            case EnemyDirection.Left:
                return Vector2.left;
            case EnemyDirection.Right:
                return Vector2.right;
            case EnemyDirection.Up:
                return Vector2.up;
            case EnemyDirection.Down:
                return Vector2.down;
            default:
                return Vector2.zero;
        }
    }

    // Convert a vector2 to an enemy direction
    public EnemyDirection EnemyDirectionFromVector(Vector2 vector)
    {
        if (vector == Vector2.left)
        {
            return EnemyDirection.Left;
        }
        else if (vector == Vector2.right)
        {
            return EnemyDirection.Right;
        }
        else if (vector == Vector2.up)
        {
            return EnemyDirection.Up;
        }
        else if (vector == Vector2.down)
        {
            return EnemyDirection.Down;
        }
        else
        {
            Debug.LogError("Invalid EnemyDirectionFromVector");
            return EnemyDirection.Invalid;
        }
    }

    // Checks if the enemy should be dead
    protected bool CheckForDeath()
    {
        // Fell off screen - uses sprite height so the death happens after the player is off-screen
        if (gameObject.transform.position.y < 0 - enemySpriteHeight)
        {
            return true;
        }

        // Handles enemy collisions
        List<Collider2D> collisions = new List<Collider2D>();
        if (enemyCollider.OverlapCollider(contactFilter, collisions) > 0)
        {
            foreach (Collider2D colliderResult in collisions)
            {
                // Collide with phase block
                if (colliderResult.GetComponent<PhaseBlock>())
                {
                    return colliderResult.GetComponent<PhaseBlock>().isSolid;
                }
                // Collide with interactable block
                else if (colliderResult.GetComponent<Interactable>())
                {
                    return colliderResult.GetComponent<Interactable>().isSolid;
                }
            }
        }

        return false;
    }
}
