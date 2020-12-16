using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class SecureCam : MonoBehaviour
{
	public Shader camShader;
	public float clamp;
	public float uvOffset;
	public float timeOffset;
	public float speed;
	public Color camLinesColor;
	public Texture2D camTextureA;

	private Material m_camMaterial;

	private void OnEnable()
	{
		if (camShader == null)
		{
			enabled = false;
			return;
		}
		m_camMaterial = new Material(camShader);
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		timeOffset += Time.deltaTime * speed;
		m_camMaterial.SetFloat("_UVOffset", uvOffset);
		m_camMaterial.SetFloat("_TimeOffset",  timeOffset);
		m_camMaterial.SetFloat("_Clamp", clamp);
		m_camMaterial.SetColor("_CamLinesColor", camLinesColor);
		m_camMaterial.SetTexture("_CamTextureA", camTextureA);
		Graphics.Blit(source, destination, m_camMaterial);
	}
}
