using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{

    [SerializeField] private float speed;

    private bool isObstaclePattern = false;
    
    //Check if object is an obstacle pattern at instantiation
    void Awake()
    {
        if (gameObject.CompareTag("Obstacle Layer"))
        {
            isObstaclePattern = true;
        }
    }

    
    void FixedUpdate()
    {
        //Move gameObject left
        transform.Translate(Vector3.left * (Time.deltaTime * speed));
        //If the object is an obstacle pattern it will be destroyed past the threshold of x = -14
        if (isObstaclePattern && transform.position.x < -14)
        {
            Destroy(gameObject);
        }
    }
    
}
