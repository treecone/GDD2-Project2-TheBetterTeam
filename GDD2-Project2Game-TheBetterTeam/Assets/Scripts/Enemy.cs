using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;

public abstract class Enemy : MoveableObject
{
    public enum EnemyDirection
    {
        Left,
        Right,
        Up,
        Down,
        Invalid
    }

    // Removes enemy from the game manager's moveable object list
    private void OnDestroy()
    {
        GameManager manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        manager.RemoveMoveableObject(gameObject);
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
            Debug.Log("Invalid");
            return EnemyDirection.Invalid;
        }
    }
}
