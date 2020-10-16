using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public GameObject FocusedObject;
    private bool focusedJumped;

    private MoveableObject mObject;
    private GameManager GM;
    private bool jumped;

    //Audio
    [SerializeField]
    private AudioClip moveAudio;
    [SerializeField]
    private AudioClip jumpAudio;
    [SerializeField]
    private AudioClip deathAudio;

    ContactFilter2D contactFilter;
    private BoxCollider2D playerCollider;

    private float playerSpriteHeight;

    // Start is called before the first frame update
    void Start()
    {
        mObject = gameObject.GetComponent<MoveableObject>();
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerSpriteHeight = gameObject.GetComponent<SpriteRenderer>().bounds.extents.y;
        playerCollider = gameObject.GetComponent<BoxCollider2D>();

        contactFilter = new ContactFilter2D();
        contactFilter.useTriggers = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Input
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (FocusedObject == null)
            {
                if (jumped == false)
                {
                    GM.ForwardTime();
                    mObject.ApplyTime(Vector2.up);
                    gameObject.GetComponent<AudioSource>().PlayOneShot(jumpAudio);
                    jumped = true;
                }
            }
            else
            {
                if (focusedJumped == false)
                {
                    GM.ForwardTime();
                    //move focused object
                    FocusedObject.GetComponent<MoveableObject>().ApplyTime(Vector2.up);
                    focusedJumped = true;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            GM.ForwardTime();
            if (FocusedObject == null)
            {
                mObject.ApplyTime(Vector2.left);
                gameObject.GetComponent<AudioSource>().PlayOneShot(moveAudio);
                jumped = false;
            }
            else
            {
                //move focused object
                FocusedObject.GetComponent<MoveableObject>().ApplyTime(Vector2.left);
                focusedJumped = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            GM.ForwardTime();
            if (FocusedObject == null)
            {
                mObject.ApplyTime(Vector2.right);
                gameObject.GetComponent<AudioSource>().PlayOneShot(moveAudio);
                jumped = false;
            }
            else
            {
                //move focused object
                FocusedObject.GetComponent<MoveableObject>().ApplyTime(Vector2.right);
                focusedJumped = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            GM.ForwardTime();
            if (FocusedObject == null)
            {
                mObject.ApplyTime(Vector2.down);
                jumped = false;
            }
            else
            {
                //move focused object
                FocusedObject.GetComponent<MoveableObject>().ApplyTime(Vector2.down);
                focusedJumped = false;
            }
        }

        if (FocusedObject == null)
        {
            //Player Gravity
            if (!jumped)
            {
                PlayerGravity();
            }
        }
        else
        {
            //Focused Object Gravity
            if (!focusedJumped)
            {
                FocusedObjectGravity();
            }
        }

        // Handles player death if applicable
        if (CheckForDeath())
        {
            Debug.Log("Player has died!");
            gameObject.GetComponent<AudioSource>().PlayOneShot(deathAudio);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    // Checks if the player did something to die
    private bool CheckForDeath()
    {
        // Fell off screen - uses sprite height so the death happens after the player is off-screen
        if (gameObject.transform.position.y < 0 - playerSpriteHeight)
        {
            return true;
        }

        // Handles enemy collisions I think?
        List<Collider2D> collisions = new List<Collider2D>();
        if (playerCollider.OverlapCollider(contactFilter, collisions) > 0)
        {
            foreach (Collider2D colliderResult in collisions)
            {
                if (colliderResult.GetComponent<Enemy>() != null)
                {
                    return true;
                }
            }
        }

        return false;
    }

    void PlayerGravity()
    {
        if (!mObject.checkDirection(Vector2.down))
        {
            // Removing this line fixes the phasing blocks
            //GM.ForwardTime();
            mObject.ApplyTime(Vector2.down);
        }
    }

    void FocusedObjectGravity()
    {
        if (!FocusedObject.GetComponent<MoveableObject>().checkDirection(Vector2.down))
        {
            // Removing this line fixes the phasing blocks
            //GM.ForwardTime();
            FocusedObject.GetComponent<MoveableObject>().ApplyTime(Vector2.down);
        }
    }

    public void ResetJumps()
    {
        if(FocusedObject == null)
        {
            focusedJumped = false;
        }
        else
        {
            jumped = false;
        }
    }
}
