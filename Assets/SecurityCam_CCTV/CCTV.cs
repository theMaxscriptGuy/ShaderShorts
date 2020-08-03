using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CCTV : ImageEffectsShaderBase
{
	public Texture2D lines;
	public float clampValue;
	public float speed;
	public Color footageColor;

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		m_effectMaterial.SetTexture("_Lines", lines);
		m_effectMaterial.SetFloat("_ClampValue", clampValue);
		m_effectMaterial.SetFloat("_AnimatedValue", Time.time * speed);
		m_effectMaterial.SetColor("_FootageColor", footageColor);
		Graphics.Blit(source, destination, m_effectMaterial);
	}
}
