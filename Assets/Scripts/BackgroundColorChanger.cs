using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BackgroundColorChanger : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private Color targetColor;

    [SerializeField] private float colorChangeSpeed = 2f;
    // Start is called before the first frame update
    void Awake()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();

        targetColor = new Color(1, 1, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.color = Color.Lerp(spriteRenderer.color, targetColor, Time.deltaTime * colorChangeSpeed);
    }

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
