using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageEffectsShaderBase : MonoBehaviour
{
	public Shader effectShader;
	internal Material m_effectMaterial;
	private void OnEnable()
	{
		if(effectShader == null)
		{
			enabled = false;
			return;
		}
		m_effectMaterial = new Material(effectShader);
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		Graphics.Blit(source, destination, m_effectMaterial);
	}
}
