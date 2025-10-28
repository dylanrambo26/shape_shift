using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    //Preset colors to match with color gates
    [SerializeField] private Material[] colors;

    public int colorIndex = -1;
    
    private void Start()
    {
        ChangeActiveColor();
    }

    //If right-click is clicked, change the player's color
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ChangeActiveColor();
        }
    }

    private void ChangeActiveColor()
    {
        //Change next index to the next index in the list, if it reaches index 2, wrap back to 0
        int nextIndex = (colorIndex + 1) % transform.childCount;
        
        //Include inactive children shape objects when recoloring
        Renderer[] renderers = GetComponentsInChildren<Renderer>(true);
        
        //Change all player shapes to the current selected color
        foreach (Renderer r in renderers)
        {
            r.material = colors[nextIndex];
        }

        colorIndex = nextIndex;
    }
}
