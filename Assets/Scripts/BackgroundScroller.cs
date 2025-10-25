using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEditor.U2D;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 0.1f;
    private Transform sprite1;
    private Transform sprite2;
    private float spriteWidth;
    
    //private Vector3 startPos;
    //rivate float worldUnitSizeX;
    
    // Start is called before the first frame update
    void Start()
    {
        sprite1 = transform.GetChild(0);
        sprite2 = transform.GetChild(1);
        
        //startPos = transform.position;

        spriteWidth = sprite1.GetComponent<SpriteRenderer>().GetComponent<SpriteRenderer>().bounds.size.x;
        //worldUnitSizeX = spriteRenderer.sprite.bounds.size.x * transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        sprite1.position += Vector3.left * (scrollSpeed * Time.deltaTime);
        sprite2.position += Vector3.left * (scrollSpeed * Time.deltaTime);

        if (sprite1.position.x <= -spriteWidth)
        {
            sprite1.position += Vector3.right * (spriteWidth * 2);
        }
        
        if (sprite2.position.x <= -spriteWidth)
        {
            sprite2.position += Vector3.right * (spriteWidth * 2);
        }
        //float newPosition = Mathf.Repeat(Time.time * scrollSpeed, worldUnitSizeX);
        //transform.position = startPos + Vector3.left * newPosition;
    }
}
