using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchShape : MonoBehaviour
{
    private int shapesIndex = 0;
    private Transform[] shapes;
    private Rigidbody[] rigidbodies;

    public bool isTriangle = false;
    public bool isTrapezoid = false;
    public bool isSquare = true;
    
    //Get the children objects and their respective rigidbodies to make smooth transitioning between shapes
    void Awake()
    {
        int count = transform.childCount;
        shapes = new Transform[count];
        rigidbodies = new Rigidbody[count];

        for (int i = 0; i < count; i++)
        {
            shapes[i] = transform.GetChild(i);
            rigidbodies[i] = shapes[i].GetComponent<Rigidbody>();
            rigidbodies[i].constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
            shapes[i].gameObject.SetActive(i == shapesIndex);
        }
    }

    //Switch shapes when space is pressed
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetActiveShape();
        }
    }

    //Change Shapes
    void SetActiveShape()
    {   
        //Get the next index for the next shape to switch to and get the current shape along with the next
        int nextIndex = (shapesIndex + 1) % transform.childCount;
        
        Transform currentChild = transform.GetChild(shapesIndex);
        Transform nextChild = transform.GetChild(nextIndex);
        
        //Set new rigidbody
        Rigidbody currentRB = rigidbodies[shapesIndex];
        Rigidbody nextRB = rigidbodies[nextIndex];
        
        //Assign current object and rigidbody properties
        Vector3 currentPosition = currentChild.position;
        Vector3 velocity = currentRB.velocity;
        Vector3 angularVelocity = currentRB.angularVelocity;
        
        //Switch to next shape
        currentChild.gameObject.SetActive(false);
        nextChild.gameObject.SetActive(true);
    
        //Assign previous object's values to the next
        nextChild.position = currentPosition;
        nextRB.velocity = velocity;
        nextRB.angularVelocity = angularVelocity;

        //Set the rotations back to their starter rotations, for triangle it is Euler(0,0,0). For the two others it is Euler(0,270f,0) because they start at a rotation of 90 degrees.
        if (nextChild.CompareTag("Triangle"))
        {
            nextChild.rotation = Quaternion.Euler(0f,0f,0f);
        }
        else if (nextChild.CompareTag("Square") || nextChild.CompareTag("Trapezoid"))
        {
            nextChild.rotation = Quaternion.Euler(0f,270f,0f);
        }
        
        shapesIndex = nextIndex;
        
        //Set the booleans to the correct values based on the active shape. These values are used in PlayerAction to compare collisions.
        if (nextChild.CompareTag("Trapezoid"))
        {
            isTrapezoid = true;
            isSquare = !isTrapezoid;
            isTriangle = !isTrapezoid;
        }
        else if (nextChild.CompareTag("Triangle"))
        {
            isTriangle = true;
            isSquare = !isTriangle;
            isTrapezoid = !isTriangle;
        }
        else if (nextChild.CompareTag("Square"))
        {
            isSquare = true;
            isTrapezoid = !isSquare;
            isTriangle = !isSquare;
        }
    }
}
