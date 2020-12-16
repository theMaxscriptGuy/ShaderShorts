using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paint : MonoBehaviour
{
    public float brushSize;

    [SerializeField]
    private Shader m_paintShader;
    private Material m_paintMat;
    private RaycastHit m_hit;
    private RenderTexture m_paintRT;
    private RenderTexture m_paintRTTemp;
    private Material m_objectMaterial;
    void Start()
    {
        if(!m_paintShader)
        {
            enabled = false;
            return;
        }
        
        //this material is only for painting the paint map
        m_paintMat = new Material(m_paintShader);
        m_paintRT = new RenderTexture(1024, 1024, 0, RenderTextureFormat.ARGBFloat);
        m_paintRTTemp = new RenderTexture(1024, 1024, 0, RenderTextureFormat.ARGBFloat);
        
        m_objectMaterial = GetComponent<Renderer>().material;
        //set the paint map to the currentMaterial/Shader
        m_objectMaterial.SetTexture("_Paint", m_paintRT);
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0))
        {
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out m_hit))
            {
                if (m_hit.transform.name != transform.name)
                    return;
                m_paintMat.SetFloat("_BrushSize", brushSize);
                m_paintMat.SetVector("_HitCoordsUV", new Vector4(m_hit.textureCoord.x, m_hit.textureCoord.y, 0, 0));
                //copy paint map to temp:
                Graphics.Blit(m_paintRT, m_paintRTTemp);
                //copy temp to paint map with currentMaterial with new paint added/removed
                Graphics.Blit(m_paintRTTemp, m_paintRT, m_paintMat);
            }
        }
    }
}
