using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorCollection : MonoBehaviour
{
    public Canvas canvas;
    public GameObject meteorPrefab;

    public List<GameObject> shards = new List<GameObject>();
    public int shardsCollected;
    public int totalShards;

    public void CreateShards(int amount)
    {
        Debug.Log("CreateShards()");
        //Clear previous shards
        foreach(GameObject obj in shards)
        {
            Destroy(obj);
            Debug.Log("Destroy shards");
        }

        shards.Clear();
        shardsCollected = 0;
        totalShards = amount;

        //Display shards onto player canvas
        for (int i = 0; i < totalShards; i++)
        {
            Debug.Log("Create shard");
            shards.Add(Instantiate(meteorPrefab, transform, canvas.transform));
            shards[i].GetComponent<RectTransform>().localPosition = new Vector3(-362 + i * 45, 127, 0);
        }
        if(transform.Find("LevelFadeIn") != null)
        {
            transform.Find("LevelFadeIn").SetAsLastSibling();
        }
    }

    public void CollectShard()
    {
        shards[shardsCollected].GetComponent<CanvasMeteor>().SetCollectionStatus(true);
        shardsCollected++;
    }

    public void ResetShards()
    {
        foreach(GameObject shard in shards)
        {
            shard.GetComponent<CanvasMeteor>().SetCollectionStatus(false);
        }

        shardsCollected = 0;
    }
}
