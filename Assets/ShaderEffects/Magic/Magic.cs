using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Magic : ImageEffectsShaderBase
{
	public int step;
	public float angle;

	public float pingPongValueSpeed;
	public float pingPongValue;
	private float m_pingPong;

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		m_pingPong = Mathf.PingPong(Time.time, pingPongValue) * pingPongValueSpeed;
		m_effectMaterial.SetFloat("_PingPong", m_pingPong);
		m_effectMaterial.SetFloat("_Angle", angle);
		m_effectMaterial.SetInt("_Step", step);
		Graphics.Blit(source, destination, m_effectMaterial);
	}
}
