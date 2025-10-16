using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
// ReSharper disable InconsistentNaming

public class PlayerAction : MonoBehaviour
{
    private Rigidbody playerRB;
    private bool isOnGround;
    private float jumpForce = 7;
    private SwitchShape switchShapeScript;
    private ChangeColor changeColorScript;
    private bool wallDashActivated = false;
    private int wallDashDuration = 3;
    public int numJumps = 0;
    private int maxJumps = 2;
    
    //Get rigidbody and set isOnGround to true
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        isOnGround = true;
        switchShapeScript = GetComponentInParent<SwitchShape>();
        changeColorScript = GetComponentInParent<ChangeColor>();
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
            SceneManager.LoadScene("Level_1");
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
            SceneManager.LoadScene("Level_1");
        }
        else if (collision.gameObject.layer == 12 && changeColorScript.colorIndex == 1)
        {
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.layer == 12 && changeColorScript.colorIndex != 1)
        {
            SceneManager.LoadScene("Level_1");
        }
        else if (collision.gameObject.layer == 13 && changeColorScript.colorIndex == 2)
        {
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.layer == 13 && changeColorScript.colorIndex != 3)
        {
            SceneManager.LoadScene("Level_1");
        }
    }
}
