using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityTile : MonoBehaviour
{
    public bool isSolid = true;
    public bool isCollectible = false;

    public abstract void CollectObject();
}
