using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class PlayerAction : MonoBehaviour
{
    //Current shape rigidbody
    private Rigidbody playerRB;
    
    //Script References
    private SwitchShape switchShapeScript;
    private ChangeColor changeColorScript;
    private GameController gameController;
    private UIController uiController;
    private PlayerExplosion playerExplosion;
    private SpawnObstaclePatterns spawnObstaclePatterns;
    
    //Background color changer scripts
    private BackgroundColorChanger backgroundBlue;
    private BackgroundColorChanger backgroundPurple;
    private BackgroundColorChanger backgroundRed;

    //Tutorial Scripts
    private TutorialController tutorialController;
    private TutorialFinished tutorialFinished;
    
    //Events
    public UnityEvent playerDeath;
    public UnityEvent levelComplete;
    
    private bool wallDashActivated = false;
    private readonly int wallDashDuration = 3;
    public int numJumps = 0;
    private readonly int maxJumps = 2;
    private bool isOnGround;
    private readonly float jumpForce = 10;
    
    //Parent Transform properties for level reset
    private Transform parentStartTransform;
    private Vector3 startPos;
    private Quaternion startRotation;

    //Animator for current shape
    [SerializeField] private Animator animator;
    
    //Get components from hierarchy and add event listeners for playerDeath and levelComplete
    void Start()
    {
        isOnGround = true;
        playerRB = GetComponent<Rigidbody>();
        switchShapeScript = GetComponentInParent<SwitchShape>();
        changeColorScript = GetComponentInParent<ChangeColor>();
        parentStartTransform = GetComponentInParent<Transform>();
        playerExplosion = GetComponent<PlayerExplosion>();
        
        startPos = parentStartTransform.position;
        startRotation = parentStartTransform.rotation;
        
        playerDeath.AddListener(ResetPlayer);
        playerDeath.AddListener(playerExplosion.ExplodePlayer);
        
        //Used specifically for the tutorial scene
        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            tutorialController = GameObject.FindGameObjectWithTag("TutorialController").GetComponent<TutorialController>();
            tutorialFinished = GameObject.FindGameObjectWithTag("UIController").GetComponent<TutorialFinished>();
            playerDeath.AddListener(tutorialController.IncrementAttempts);
            playerDeath.AddListener(tutorialFinished.UpdateTutorialAttempts);
            levelComplete.AddListener(tutorialFinished.ShowTutorialComplete);
        }
        //Used specifically in the first level
        else if (SceneManager.GetActiveScene().name == "Level_1")
        {
            gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
            uiController = GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>();
            backgroundBlue = GameObject.FindGameObjectWithTag("Far_Background_Blue").GetComponent<BackgroundColorChanger>();
            backgroundPurple = GameObject.FindGameObjectWithTag("Far_Background_Purple").GetComponent<BackgroundColorChanger>();
            //backgroundRed = GameObject.FindGameObjectWithTag("Far_Background_Red").GetComponent<BackgroundColorChanger>();
            backgroundBlue.FadeColor(true);
            playerDeath.AddListener(gameController.IncrementAttempts);
            playerDeath.AddListener(uiController.UpdateAttempts);
            
            levelComplete.AddListener(uiController.ShowLevelComplete);
            levelComplete.AddListener(uiController.ShowMedal);
        }
    }
    
    void Update()
    {   
        //Only call the correct ability when the correct object is active
        //Can dash midair but cannot jump in midair, unless double jump activated
        if (Input.GetMouseButtonDown(0) && isOnGround && switchShapeScript.isSquare)
        {
            SingleJump();
        }
        else if (Input.GetMouseButtonDown(0) && switchShapeScript.isTrapezoid)
        {
            StartCoroutine(WallDash());
        }
        else if (Input.GetMouseButtonDown(0) && (isOnGround || numJumps < maxJumps) && switchShapeScript.isTriangle)
        {
            DoubleJump();
        }
    }
    
    //Jump method using rigidbody AddForce, rotation animation for square
    void SingleJump()
    {
        playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        animator.SetTrigger("Jump");
        isOnGround = false;
    }
    
    //Coroutine for dashing through breakable walls with the trapezoid object
    IEnumerator WallDash()
    {
        wallDashActivated = true;
        yield return new WaitForSeconds(wallDashDuration);
        wallDashActivated = false;
    }
    
    //Jump once, and then only once again until numJumps reaches 0 again.
    void DoubleJump()
    {
        playerRB.velocity = new Vector3(playerRB.velocity.x, 0f, playerRB.velocity.z);
            
        playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        if (numJumps == 0)
        {
            animator.SetTrigger("Jump");
        }
        else
        {
            animator.ResetTrigger("Jump");
            animator.SetTrigger("Jump");
        }
        numJumps++;
        isOnGround = false;
        
    }
    
    //Indicates if the player has returned to the ground to prohibit double jumping during single jump, and resets numJumps for double jump
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            numJumps = 0;
        }
    }
    
    //If the collision is between the player and obstacle then reset the scene
    private void OnTriggerEnter(Collider collision)
    {   
        //Reload scene if dash is activated and player collides with a non-breakable object. Also reload if dash is
        //not activated and collides with breakable object.
        if ((collision.gameObject.layer == 9) || (collision.gameObject.layer == 10 && !wallDashActivated))
        {
            playerDeath.Invoke();
            //SceneManager.LoadScene("Level_1");
        }
        //Otherwise, destroy breakable wall
        else if (collision.gameObject.layer == 10 && wallDashActivated)
        {
            collision.gameObject.GetComponentInParent<BreakableWall>()?.Break();
        }
        //If the collision is with a green color gate and the player is green, play the color gate animation and destroy the color gate
        else if (collision.gameObject.layer == 11 && changeColorScript.colorIndex == 0)
        {
            var mesh = collision.gameObject.GetComponentInChildren<MeshRenderer>();
            if (mesh != null)
                mesh.enabled = false;

            Transform fireball = collision.gameObject.transform.Find("Green Fireball");
            if (fireball != null)
            {
                fireball.gameObject.SetActive(true);

                var anim = fireball.GetComponent<PlayFireballAnimation>();
                if (anim != null)
                {
                    anim.PlayAnimation(); // No coroutine if PlayAnimation() isn’t one
                }
                else
                {
                    Debug.LogWarning("PlayFireballAnimation script missing on: " + fireball.name);
                }
            }
            else
            {
                Debug.LogError("Could not find child 'Green Fireball' under " + collision.gameObject.name);
            }

            Destroy(collision.gameObject, 1f);
        }
        //Player is not green, invoke playerDeath listeners
        else if (collision.gameObject.layer == 11 && changeColorScript.colorIndex != 0)
        {
            playerDeath.Invoke();
        }
        //If the collision is with a blue color gate and the player is blue, play the color gate animation and destroy the color gate
        else if (collision.gameObject.layer == 12 && changeColorScript.colorIndex == 1)
        {
            Destroy(collision.gameObject);
        }
        //Player is not blue, invoke playerDeath listeners
        else if (collision.gameObject.layer == 12 && changeColorScript.colorIndex != 1)
        {
            playerDeath.Invoke();
        }
        //If the collision is with an orange color gate and the player is orange, play the color gate animation and destroy the color gate
        else if (collision.gameObject.layer == 13 && changeColorScript.colorIndex == 2)
        {
            Destroy(collision.gameObject);
        }
        //Player is not orange, invoke playerDeath listeners
        else if (collision.gameObject.layer == 13 && changeColorScript.colorIndex != 3)
        {
            playerDeath.Invoke();
        }
        //If player collides with the finish line at the end of the level, invoke level complete listeners
        else if (collision.gameObject.layer == 14)
        {
            //TODO play an animation for level complete
            levelComplete.Invoke();
            Destroy(collision.gameObject);
            gameObject.SetActive(false);
        }
        //Trigger to gradually change background color
        else if (collision.gameObject.layer == 16)
        {
            print("change color");
            backgroundPurple.FadeColor(true);
            backgroundBlue.FadeColor(false);
        }
    }

    //Resets the player back to the start position
    private void ResetPlayer()
    {
        parentStartTransform.position = startPos;
        parentStartTransform.rotation = startRotation;
        animator.SetTrigger("Death");
    }
}
