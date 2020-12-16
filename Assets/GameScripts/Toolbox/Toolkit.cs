using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toolkit : MonoBehaviour
{
    public MeasureTool currentTool;
    void Start()
    {
        currentTool.OnExecuteTool += CurrentTool_OnExecuteTool;
    }

    private void CurrentTool_OnExecuteTool(object sender, int e)
    {
        Debug.LogError(e);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
