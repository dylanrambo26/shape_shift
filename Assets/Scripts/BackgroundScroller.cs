using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEditor.U2D;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 0.1f;
    
    //Two duplicate sprites
    private Transform sprite1;
    private Transform sprite2;
    private float spriteWidth;
    
    //Used to pause background scrolling globally
    public static bool isPaused = false;
    
    void Start()
    {
        //Get the two sprites and set the width to the x-size of the first sprite
        sprite1 = transform.GetChild(0);
        sprite2 = transform.GetChild(1);
        spriteWidth = sprite1.GetComponent<SpriteRenderer>().bounds.size.x;
    }
    
    void Update()
    {
        //Will pause if another script changes isPaused to true.
        if (isPaused)
        {
            return;
        }
        //Scroll left
        sprite1.position += Vector3.left * (scrollSpeed * Time.deltaTime);
        sprite2.position += Vector3.left * (scrollSpeed * Time.deltaTime);
        
        //Once a sprite reaches the negative value of its width, move it right to line up with the other sprite, to appear seamless
        if (sprite1.position.x <= -spriteWidth)
        {
            sprite1.position += Vector3.right * (spriteWidth * 2);
        }
        
        if (sprite2.position.x <= -spriteWidth)
        {
            sprite2.position += Vector3.right * (spriteWidth * 2);
        }
    }
}
