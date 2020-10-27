using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameObject FocusedObject;
    private bool focusedJumped;

    private AudioManager audioManager;

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

    //Animation 
    [SerializeField]
    private int animationFrame;
    [SerializeField]
    private Sprite[] walkingSprites;
    [SerializeField]
    private Sprite[] jumpingSprites;
    [SerializeField]
    private Sprite idleSprite;


    ContactFilter2D contactFilter;
    private BoxCollider2D playerCollider;

    private float playerSpriteHeight;

    // Start is called before the first frame update
    void Start()
    {
        mObject = gameObject.GetComponent<MoveableObject>();
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioManager = GameObject.Find("PlayerCanvas").GetComponent<AudioManager>();
        playerSpriteHeight = gameObject.GetComponent<SpriteRenderer>().bounds.extents.y;
        playerCollider = gameObject.GetComponent<BoxCollider2D>();

        contactFilter = new ContactFilter2D();
        contactFilter.useTriggers = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Input
        if (!gameObject.GetComponent<MoveableObject>().inMovement)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (FocusedObject == null)
                {
                    if (jumped == false)
                    {
                        GM.ForwardTime();
                        mObject.ApplyTime(Vector2.up);
                        audioManager.PlaySFX(jumpAudio);
                        animationFrame = 0;
                        gameObject.GetComponent<SpriteRenderer>().sprite = jumpingSprites[0];
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
                    audioManager.PlaySFX(moveAudio);
                    if (jumped == true)
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = jumpingSprites[1];
                        jumped = false;
                    }
                    else
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = walkingSprites[animationFrame++];
                    }
                    gameObject.GetComponent<SpriteRenderer>().flipX = true;
                    if (animationFrame >= walkingSprites.Length)
                        animationFrame = 0;
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
                    audioManager.PlaySFX(moveAudio);
                    if (jumped == true)
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = jumpingSprites[1];
                        jumped = false;
                    }
                    else
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = walkingSprites[animationFrame++];
                    }
                    gameObject.GetComponent<SpriteRenderer>().flipX = false;
                    if (animationFrame >= walkingSprites.Length)
                        animationFrame = 0;
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
                    gameObject.GetComponent<SpriteRenderer>().sprite = idleSprite;
                    jumped = false;
                }
                else
                {
                    //move focused object
                    FocusedObject.GetComponent<MoveableObject>().ApplyTime(Vector2.down);
                    focusedJumped = false;
                }
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
            GM.ResetShardsOnDeath();
            audioManager.PlaySFX(deathAudio);
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

        // Handles enemy collisions
        List<Collider2D> collisions = new List<Collider2D>();
        if (playerCollider.OverlapCollider(contactFilter, collisions) > 0)
        {
            foreach (Collider2D colliderResult in collisions)
            {
                // Collide with enemy
                if (colliderResult.GetComponent<Enemy>() != null)
                {
                    return true;
                }
                // Collide with phase block
                else if (colliderResult.GetComponent<PhaseBlock>())
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

    // Re-posess the player
    public void OnMouseDown()
    {
        if (FocusedObject != null)
        {
            FocusedObject.GetComponent<Interactable>().isFocused = false;
            FocusedObject = null;
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
