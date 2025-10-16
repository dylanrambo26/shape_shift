using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    [SerializeField] private Material[] colors;

    public int colorIndex = -1;

    // Start is called before the first frame update
    private void Start()
    {
        ChangeActiveColor();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ChangeActiveColor();
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void ChangeActiveColor()
    {
        int nextIndex = (colorIndex + 1) % transform.childCount;
        
        //Include inactive children shape objects when recoloring
        Renderer[] renderers = GetComponentsInChildren<Renderer>(true);
        foreach (Renderer r in renderers)
        {
            r.material = colors[nextIndex];
        }

        colorIndex = nextIndex;
    }
}
