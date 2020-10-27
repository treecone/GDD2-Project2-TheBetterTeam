using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> moveableObjects;
    [SerializeField]
    private int numberOfShards;
    [SerializeField]
    private int[] shardsPerLevel;
    [SerializeField]
    private AudioClip shardPickupSound;
    [SerializeField]
    private AudioClip levelMusic;
    [SerializeField]
    private GameObject fader;

    public int currentLevel;
    private AudioManager audioManager;

    void Start()
    {
        //Use single gameManager throught game
        DontDestroyOnLoad(this.gameObject);
        GetAllMoveableObjects();
    }

    // Update is called once per frame
    void Update()
    {
        //Debugging
        /*
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ForwardTime();
        }
        */
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //skip level
            numberOfShards = 100;
            OnShardCollected();
        }
    }

    private void GetAllMoveableObjects()
    {
        moveableObjects = new List<GameObject>();
        GameObject[] allObj = FindObjectsOfType<GameObject>();
        foreach (GameObject moveObj in allObj)
        {
            if (moveObj.GetComponent<MoveableObject>() != null)
            {
                moveableObjects.Add(moveObj);
            }
        }
    }

    public void RemoveMoveableObject(GameObject removalObject)
    {
        moveableObjects.Remove(removalObject);
    }

    public void ForwardTime ()
    {
        UpdatePhaseBlocks();
        GameObject playerFocus = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().FocusedObject;
        for (int i = 0; i < moveableObjects.Count; i++)
        {
            GameObject current = moveableObjects[i];
            if(current.tag != "Player" || (current.tag == "Player" && playerFocus != null)) 
            {
                if (playerFocus != current)
                {
                    //Gravity
                    current.GetComponent<MoveableObject>().ApplyTime(Vector2.down);
                    //Set jumped to false so that object can jump when switching back
                    GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().ResetJumps();
                }
            }
        }
    }

    public void UpdatePhaseBlocks()
    {
        //update phase blocks
        foreach (GameObject obj in FindObjectsOfType<GameObject>())
        {
            if (obj.GetComponent<PhaseBlock>() != null)
            {
                obj.GetComponent<PhaseBlock>().UpdatePhaseCount();
            }
        }
    }

    public void OnShardCollected()
    {
        numberOfShards++;
        Debug.Log("Shards collected: " + numberOfShards);
        audioManager.PlaySFX(shardPickupSound);
        GameObject.Find("PlayerCanvas").GetComponent<MeteorCollection>().CollectShard();
        //Prob move this to when a player picks up a shard so we dont have to call it every frame
        if (numberOfShards >= shardsPerLevel[currentLevel])
        {
            GameObject fadeObj = Instantiate(fader);
            fadeObj.GetComponentInChildren<FadeScene>().Init(0, 1);
            currentLevel++;
            StartCoroutine("GoingToNextLevel");
        }
    }

    public void ResetShardsOnDeath()
    {
        GameObject.Find("PlayerCanvas").GetComponent<MeteorCollection>().ResetShards();
        numberOfShards = 0;
    }

    public void AddMoveableObject(GameObject newObject)
    {
        if (moveableObjects != null && newObject.GetComponent<MoveableObject>() != null)
        {
            moveableObjects.Add(newObject);
        }
    }

    [SerializeField]
    private AudioClip win;

    IEnumerator GoingToNextLevel ()
    {
        Debug.Log("Next Level, son!()");
        numberOfShards = 0;
        audioManager.PlaySFX(win);
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("Scenes/Levels/Level" +currentLevel);

        GameObject.Find("PlayerCanvas").GetComponent<MeteorCollection>().CreateShards(shardsPerLevel[currentLevel]);

    }

    public void OnLevelWasLoaded(int level)
    {
        //initialize first level
        if (audioManager == null)
        {
            currentLevel = 1;
            GameObject.Find("PlayerCanvas").GetComponent<MeteorCollection>().CreateShards(shardsPerLevel[1]);
            audioManager = GameObject.Find("PlayerCanvas").GetComponent<AudioManager>();
            audioManager.PlayMusic(levelMusic);
        }

        GameObject fadeObj = Instantiate(fader);
        fadeObj.GetComponentInChildren<FadeScene>().Init(1, 0);

        GetAllMoveableObjects();
    }
}
