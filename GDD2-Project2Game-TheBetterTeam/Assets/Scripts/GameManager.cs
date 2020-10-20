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

    private AudioManager audioManager;

    void Start()
    {
        moveableObjects = new List<GameObject>(); // Otherwise there's a warning -_-
        GameObject[] allObj = FindObjectsOfType<GameObject>();
        foreach(GameObject moveObj in allObj)
        {
            if(moveObj.GetComponent<MoveableObject>() != null)
            {
                moveableObjects.Add(moveObj);
            }
        }

        audioManager = GameObject.Find("PlayerCanvas").GetComponent<AudioManager>();
        audioManager.PlayMusic(levelMusic);
    }

    // Update is called once per frame
    void Update()
    {
        //Debugging
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ForwardTime();
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
        foreach (GameObject obj in moveableObjects)
        {
            if(obj.tag != "Player" || (obj.tag == "Player" && playerFocus != null)) 
            {
                if (playerFocus != obj)
                {
                    //Gravity
                    obj.GetComponent<MoveableObject>().ApplyTime(Vector2.down);
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
        audioManager.PlaySFX(shardPickupSound);
        //Prob move this to when a player picks up a shard so we dont have to call it every frame
        if (numberOfShards >= shardsPerLevel[SceneManager.GetActiveScene().buildIndex])
        {
            StartCoroutine("GoingToNextLevel");
        }
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
        yield return new WaitForSeconds(4f);
        GameObject.FindWithTag("Settings").GetComponent<Settings>().NextLevel();

    }
}
