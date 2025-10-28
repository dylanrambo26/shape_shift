using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BackgroundColorChanger : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private Color targetColor;

    [SerializeField] private float colorChangeSpeed = 2f;
    
    //Get the spriteRenderer in the background sprite and set its alpha to 0
    void Awake()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();

        targetColor = new Color(1, 1, 1, 0);
    }

    //Gradually change transparency over time
    void Update()
    {
        spriteRenderer.color = Color.Lerp(spriteRenderer.color, targetColor, Time.deltaTime * colorChangeSpeed);
    }

    //Will fade in the next background if fadeIn true, otherwise it will fade it out
    public void FadeColor(bool fadeIn)
    {
        
        var color = spriteRenderer.color;
        if (fadeIn)
        {
            targetColor = new Color(color.r, color.g, color.b, 1);
            print("change to " + targetColor);
        }
        else
        {
            targetColor = new Color(color.r, color.g, color.b, 0);
        }
        
    }
}
