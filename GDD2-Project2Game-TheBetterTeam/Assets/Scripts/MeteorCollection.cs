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

    void Start()
    {
        
    }

    void Update()
    {

    }

    public void CreateShards(int amount)
    {
        //Clear previous shards
        foreach(GameObject obj in shards)
        {
            Destroy(obj);
        }

        shards.Clear();
        shardsCollected = 0;
        totalShards = amount;

        //Display shards onto player canvas
        for (int i = 0; i < totalShards; i++)
        {
            shards.Add(Instantiate(meteorPrefab, transform, canvas.transform));
            shards[i].GetComponent<RectTransform>().localPosition = new Vector3(-362 + i * 45, 127, 0);
        }
    }

    public void CollectShard()
    {
        shards[shardsCollected].GetComponent<CanvasMeteor>().Collect();
        shardsCollected++;
    }
}
