using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
// ReSharper disable InconsistentNaming

public class PlayerAction : MonoBehaviour
{
    private Rigidbody playerRB;
    private bool isOnGround;
    private float jumpForce = 10;
    private SwitchShape switchShapeScript;
    private ChangeColor changeColorScript;
    private GameController _gameController;
    private UIController _uiController;
    private SpawnObstaclePatterns _spawnObstaclePatterns;
    private bool wallDashActivated = false;
    private int wallDashDuration = 3;
    public int numJumps = 0;
    private int maxJumps = 2;
    public UnityEvent playerDeath;
    public UnityEvent levelComplete;
    private Transform parentStartTransform;
    private Vector3 startPos;
    private Quaternion startRotation;

    [SerializeField] private Animator animator;
    
    //Get rigidbody and set isOnGround to true
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        isOnGround = true;
        switchShapeScript = GetComponentInParent<SwitchShape>();
        changeColorScript = GetComponentInParent<ChangeColor>();
        parentStartTransform = GetComponentInParent<Transform>();
        startPos = parentStartTransform.position;
        startRotation = parentStartTransform.rotation;
        
        _gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        _uiController = GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>();
        _spawnObstaclePatterns = GameObject.FindGameObjectWithTag("ObstacleSpawner").GetComponent<SpawnObstaclePatterns>();
        
        playerDeath.AddListener(_gameController.IncrementAttempts);
        playerDeath.AddListener(_uiController.UpdateAttempts);
        playerDeath.AddListener(_spawnObstaclePatterns.ResetSpawner);
        playerDeath.AddListener(ResetPlayer);
        
        levelComplete.AddListener(_uiController.ShowLevelComplete);
        levelComplete.AddListener(_uiController.ShowMedal);
    }

    //Single jump when left mouse is clicked and player is on the ground
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
    
    //Jump method using rigidbody AddForce
    void SingleJump()
    {
        playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        animator.SetTrigger("Jump");
        isOnGround = false;
    }
    
    //Coroutine for dashing through breakable walls with the trapezoid object
    IEnumerator WallDash()
    {
        //Debug.Log("wall dash activated");
        wallDashActivated = true;
        yield return new WaitForSeconds(wallDashDuration);
        wallDashActivated = false;
    }
    
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
    
    //Indicates if the player has returned to the ground to prohibit double jumping
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            numJumps = 0;
        }
    }
    
    //If the collision is between the player and obstacle then reset the scene
    //TODO add number of attempts
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
        //TODO play animation here
        else if (collision.gameObject.layer == 10 && wallDashActivated)
        {
            Destroy(collision.gameObject);
        }
        //TODO play animations here for color gates
        else if (collision.gameObject.layer == 11 && changeColorScript.colorIndex == 0)
        {
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.layer == 11 && changeColorScript.colorIndex != 0)
        {
            playerDeath.Invoke();
        }
        else if (collision.gameObject.layer == 12 && changeColorScript.colorIndex == 1)
        {
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.layer == 12 && changeColorScript.colorIndex != 1)
        {
            playerDeath.Invoke();
        }
        else if (collision.gameObject.layer == 13 && changeColorScript.colorIndex == 2)
        {
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.layer == 13 && changeColorScript.colorIndex != 3)
        {
            playerDeath.Invoke();
        }
        else if (collision.gameObject.layer == 14)
        {
            //TODO play an animation for level complete
            levelComplete.Invoke();
            Destroy(collision.gameObject);
            gameObject.SetActive(false);
        }
    }

    private void ResetPlayer()
    {
        parentStartTransform.position = startPos;
        parentStartTransform.rotation = startRotation;
        animator.SetTrigger("Death");
    }
}
