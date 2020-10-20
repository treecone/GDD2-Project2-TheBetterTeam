using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MoveableObject : MonoBehaviour
{
    public bool inMovement;
    public bool isSolid = true;
    private Tilemap mainTilemap;
    private GridLayout mainGridLayout;

    protected virtual void Start()
    {
        inMovement = false;
        mainGridLayout = GameObject.Find("Grid").GetComponent<Grid>();
        mainTilemap = GameObject.Find("Grid").transform.GetChild(0).GetComponent<Tilemap>();

    }

    //Actual script that lerps the object from position to the direction
    IEnumerator Move (Vector2 direction, float deltaTime)
    {
        inMovement = true;
        Vector2 endPosition = (Vector2)transform.position + direction;
        float newTime = Time.time;
        while (Time.time < newTime + deltaTime)
        {
            transform.position = Vector2.Lerp(transform.position, endPosition, (Time.time - newTime) / deltaTime);
            yield return null;
        }
        transform.position = endPosition;
        inMovement = false;
    }

    //Void to be called everytime time is moved forward. Game Manager Calls this.
    // Child classes may reimplement to apply time in their own way.
    public virtual void ApplyTime (Vector2 direction)
    {
        ApplyDirection(direction);
    }

    public void ApplyDirection (Vector2 direction)
    {
        //Movement
        if (direction != Vector2.zero && !inMovement)
        {
            if (!isSolid)
            {
                StartCoroutine(Move(direction, 0.2f));
            }
            else if (!checkDirection(direction))
            {
                StartCoroutine(Move(direction, 0.2f));
            }
        }
    }

    public bool CheckTilePosition (Vector2 position)
    {
        Vector3Int convertedPos = new Vector3Int((int)position.x, (int)position.y, (int)mainTilemap.transform.position.z);
        if (mainTilemap.HasTile(convertedPos)) { return true; }
        return false;
    }

    // Entities are not tiles, and therefor need their own method to dheck where they are
    public bool CheckEntityPosition (Vector2 direction)
    {
        // Do a raycast
        RaycastHit2D raycastResult = Physics2D.Raycast(transform.position, direction, 1.0f);

        // If something is found
        if (raycastResult.transform != null)
        {
            if (raycastResult.transform.gameObject != gameObject)
            {
                // Check if it is an entity tile
                EntityTile entityTile = raycastResult.transform.gameObject.GetComponent<EntityTile>();
                if (entityTile != null)
                {
                    // Here is where we should do "passable" checks.
                    if (entityTile.isSolid)
                    {
                        return true;
                    }

                    // Collectible objects
                    if (entityTile.isCollectible)
                    {
                        entityTile.CollectObject();
                    }
                }
            }
        }
        return false;
    }

    //Returns true if there is a tile at the tilePos
    public bool checkDirection (Vector2 direction)
    {
        //Gets the objects position in cell space
        Vector3Int objPos = mainGridLayout.WorldToCell(gameObject.transform.position);
        //Adds the direction vector
        Vector3Int blockLocation = new Vector3Int(objPos.x + (int)direction.x, objPos.y + (int)direction.y, 0);
        if (mainTilemap.HasTile(blockLocation))
        {
            return true;
        }
        else if (CheckEntityPosition(direction))
        {
            return true;
        }
        return false;
    }

}
