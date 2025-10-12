using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchShape : MonoBehaviour
{
    private int shapesIndex = 0;
    private Transform[] shapes;
    private Rigidbody[] rigidbodies;

    public bool isTriangle;
    
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
        //TODO add check for triangle index for double jump
        Transform currentChild = transform.GetChild(shapesIndex);
        Transform nextChild = transform.GetChild(nextIndex);
        
        //Set new rigidbody
        Rigidbody currentRB = rigidbodies[shapesIndex];
        Rigidbody nextRB = rigidbodies[nextIndex];
        
        //Assign current object and rigidbody properties
        Vector3 currentPosition = currentChild.position;
        Quaternion rotation = currentChild.rotation;
        Vector3 velocity = currentRB.velocity;
        Vector3 angularVelocity = currentRB.angularVelocity;
        
        //Switch to next shape
        currentChild.gameObject.SetActive(false);
        nextChild.gameObject.SetActive(true);
    
        //Assign previous object's values to the next
        nextChild.position = currentPosition;
        nextChild.rotation = rotation;
        nextRB.velocity = velocity;
        nextRB.angularVelocity = angularVelocity;
        shapesIndex = nextIndex;
    }
}
