using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DipToBlack : MonoBehaviour
{
    public Shader dipToBlackShader;
    public float fadeValue;
    public float fadeValueMultiplier;

    private Material m_dipToBlackMaterial;
    private float fadePingPong;
    private void OnEnable()
    {
        if(dipToBlackShader == null)
        {
            enabled = false;
            return;
        }
        //Create a new material here:
        m_dipToBlackMaterial = new Material(dipToBlackShader);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        fadePingPong = Mathf.PingPong(Time.time, fadeValue) * fadeValueMultiplier;
        m_dipToBlackMaterial.SetFloat("_fadeValue", fadePingPong);
        Graphics.Blit(source, destination, m_dipToBlackMaterial);
    }
}
