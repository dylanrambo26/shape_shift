using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private Rigidbody playerRB;
    private bool isOnGround;
    private float jumpForce = 7;
    
    //Get rigidbody and set isOnGround to true
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        isOnGround = true;
    }

    //Single jump when left mouse is clicked and player is on the ground
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isOnGround)
        {
            SingleJump();
        }
    }
    
    //Jump method using rigidbody AddForce
    void SingleJump()
    {
        playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isOnGround = false;
    }
    
    //Indicates if the player has returned to the ground to prohibit double jumping
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }
}
