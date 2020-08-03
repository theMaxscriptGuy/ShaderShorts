using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Vignette : ImageEffectsShaderBase
{
	public Vector2 offset;
	public float exp;
	public Color vignetteColor;

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		m_effectMaterial.SetFloat("_XOffset", offset.x);
		m_effectMaterial.SetFloat("_YOffset", offset.y);
		m_effectMaterial.SetFloat("_Exp", exp);
		m_effectMaterial.SetColor("_VignetteColor", vignetteColor);

		Graphics.Blit(source, destination, m_effectMaterial);
	}
}
