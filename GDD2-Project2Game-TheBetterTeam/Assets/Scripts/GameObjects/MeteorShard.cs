using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorShard : EntityTile
{

    public override void CollectObject()
    {
        //TODO: Any neat visual effects or other shard-collection logic

        GameObject.Find("GameManager").GetComponent<GameManager>().OnShardCollected();
        Destroy(gameObject);
    }
}
