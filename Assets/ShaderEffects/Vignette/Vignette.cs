using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Vignette : ImageEffectsShaderBase
{
	public Vector2 offset;
	public float exp;
	public float maxExp;
	public Color vignetteColor;

	private float m_playerHealth = 1f;

	private void Start()
	{
		HealthScript.OnUpdatePlayerHealth += HealthScript_OnUpdatePlayerHealth;
	}

	private void HealthScript_OnUpdatePlayerHealth(float val)
	{
		Debug.Log(val);
		if(m_playerHealth <= 0)
		{
			m_playerHealth = 0f;
			return;
		}
		m_playerHealth -= val;
	}

	private void Update()
	{
		if (m_playerHealth >= 1f)
		{
			m_playerHealth = 1f;
			return;
		}
		m_playerHealth += Time.deltaTime * 0.05f;
	}

	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		//exp = Mathf.Lerp(maxExp, 0f, m_playerHealth);
		m_effectMaterial.SetFloat("_XOffset", offset.x);
		m_effectMaterial.SetFloat("_YOffset", offset.y);
		m_effectMaterial.SetFloat("_Exp", exp);
		m_effectMaterial.SetColor("_VignetteColor", vignetteColor);

		Graphics.Blit(source, destination, m_effectMaterial);
	}
}