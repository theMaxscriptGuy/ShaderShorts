using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : ImageEffectsShaderBase
{
	private float t;

	public float speed;
	public Vector2 wave;
	public Vector2 UVScale;
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		t += Time.deltaTime * speed;
		m_effectMaterial.SetFloat("time", t);
		m_effectMaterial.SetFloat("waveX", wave.x);
		m_effectMaterial.SetFloat("waveY", wave.y);
		m_effectMaterial.SetFloat("uvScaleX", UVScale.x);
		m_effectMaterial.SetFloat("uvScaleY", UVScale.y);
		Graphics.Blit(source, destination, m_effectMaterial);
	}
}
